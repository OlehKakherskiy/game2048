using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using UnityEngine.UI;

public class TileCellComparer: IComparer<TileCell>
{
    public int Compare(TileCell x, TileCell y)
    {
        int xNum = Int32.Parse(x.transform.name.Replace("TileCell",""));
        int yNum = Int32.Parse(y.transform.name.Replace("TileCell",""));
        return xNum - yNum;
    }
}

public class GameManager:MonoBehaviour
{
    private static readonly int TILES_COUNT = 16;
    
    private TileCell[] tiles;
    private HashSet<int> emptyTiles;
    private System.Random rnd;
    
    private Score scoreController;
    
    public GameObject gameOverPanel;
    public GameObject winPanel;
    
    public Text ScoreText;
    
    public void Awake()
    {
        System.Environment.SetEnvironmentVariable("MONO_REFLECTION_SERIALIZER", "yes");
        GameSaveLoad.LoadGame();
        rnd = new System.Random();
        tiles = GameObject.FindObjectsOfType<TileCell>();
        emptyTiles = new HashSet<int>();
        Array.Sort(tiles, new TileCellComparer());
        scoreController = GameObject.FindObjectOfType<Score>();
    }
    
    public void Start()
    {
        loadTiles();
        if(emptyTiles.Count() == TILES_COUNT)
        {
            Debug.Log("All tiles are empty - new game and generating first two numbers");
            generateNumber();
            generateNumber();
        }
    }
    
    private void loadTiles()
    {
        Debug.Log("Updating persisted tiles numbers...");
        int[] loadedTileIndexes = GameSaveLoad.GAME_DATA.getCellValues();
        for(int i = 0; i < loadedTileIndexes.Length; i++)
        {
            tiles[i].UpdateNumber(loadedTileIndexes[i]);
            if(loadedTileIndexes[i] == 1)
            {
                emptyTiles.Add(i);
            }
        }
    }
    
    public void CreateNewGame()
    {
        Debug.Log("Creating new game ...");
        for(int i = 0; i < TILES_COUNT; i++)
        {
            ResetTile(i);
        }
        generateNumber();
        generateNumber();
    }

    public void doMoving(Direction d)
    {
//        Debug.Log("Direction value is "+ d);
        
        for(int i = 0; i < (int) Math.Sqrt(tiles.Length); i++)
        {
            int[] mergeIndexes = SliceTileArray(d, i);
            MoveTiles(mergeIndexes);
        }

        generateNumber();
    }
    
    private int[] SliceTileArray(Direction d, int multiplicator)
    {
        int shift = (int) d;
        int scale = (Direction.Left == d || Direction.Right == d) ? (int) Direction.Down * multiplicator : (int) Direction.Right * multiplicator;
        
        int lowBoundary = 0;
        int highBoundary = (int) Math.Sqrt(TILES_COUNT);
        
        int from = -1;
        int to = -1;
        if(Math.Sign(shift) < 0)
        {
            from = lowBoundary + scale;
            to = highBoundary * Math.Abs(shift) + scale;
            shift *= -1;
        
        int[] result = new int[(int) Math.Sqrt(TILES_COUNT)];
        int j = 0;
        for(int i = from; i < to; i = i + shift)
        {
            result[j] = i;
            j++;
        }
        return result;
        }else
        {
            from = highBoundary * Math.Abs(shift) + scale;
            to = lowBoundary - 1 + scale;
            shift *= -1;
        
        int[] result = new int[(int) Math.Sqrt(TILES_COUNT)];
        int j = 0;
        for(int i = from + shift; i > to; i = i + shift)
        {
            result[j] = i;
            j++;
        }
        return result;
        }
        
    }
    
    private void MoveTiles(int[] mergeIndexes)
    {
        for(int i = 1; i < mergeIndexes.Length; i++)
        {
            int fromIndex = mergeIndexes[i];
//            Debug.Log("Check what to do with tile num = "+fromIndex+" and index = "+i);
            if(emptyTiles.Contains(fromIndex)){
//                Debug.Log("from index is empty");
                continue;
            }
            bool allEmpty = true;
            for(int j = i - 1; j >= 0; j--)    
            {
                int toIndex = mergeIndexes[j];
//                Debug.Log("Comparing with tile num = "+toIndex);
                if(emptyTiles.Contains(toIndex))
                {
//                    Debug.Log("Tile is empty with num = "+toIndex);
                    continue;
                }else{
                    allEmpty = false;
                    if (TryMerge(fromIndex, toIndex) == true)
                    {
//                        Debug.Log("Merged successfully");
                        ResetTile(fromIndex);    
                    }else{
                        doMove(fromIndex, mergeIndexes[j+1]);
                    }
                    break;
                }
            }
            if(allEmpty == true)
            {
                doMove(fromIndex, mergeIndexes[0]);
            }
        }
    }
    
    private void doMove(int from, int to)
    {   
        if(from == to){
            return;
        }
        Debug.Log("Moving tile from index = "+from+" to index = "+to);
        emptyTiles.Remove(to);
        tiles[to].UpdateNumber(tiles[from].Number);
        
        ResetTile(from);
    }


    private bool TryMerge(int currentIndex, int mergeToIndex)
    {
        TileCell mergeTo = tiles[mergeToIndex];
        if (mergeTo.Number == tiles[currentIndex].Number)
        {
            int newValue = mergeTo.Number * 2;
            scoreController.Increment(newValue);
            mergeTo.UpdateNumber(newValue);
            CheckWin();
            return true;
        }
        return false;
    }

    private void ResetTile(int tileIndex)
    {
        Debug.Log("Reset tile with index = "+tileIndex);
        tiles[tileIndex].UpdateNumber(1);
        emptyTiles.Add(tileIndex);
    }
    
    private void generateNumber()
    {
        Debug.Log("Generating new number ...");
        if(emptyTiles.Count() == 0)
        {
            Debug.Log("No empty cells - checking game over ...");
            checkGameOver();
            return;
        }
        List<int> tempList = new List<int>(emptyTiles);
        int elementIndex = tempList.ElementAt(rnd.Next(0, tempList.Count()));
        Debug.Log("Was generated on pos = "+elementIndex);
        tiles[elementIndex].UpdateNumber(2);
        emptyTiles.Remove(elementIndex);
    }
    
    private bool lastChanceMissed = false;
    
    private void checkGameOver() {
        if(lastChanceMissed){
            Debug.Log("User didn't merge any tiles and no free tiles - game over.");
            gameOverPanel.SetActive(true);
            ScoreText.text = scoreController.ScoreNumber+"";
        }else{
            Debug.Log("User has last chanse to merge any tile...");
            lastChanceMissed = true;
        }
    }
    
        
    private void CheckWin()
    {
        for(int i = 0; i < TILES_COUNT; i++)
        {
            if(tiles[i].getCellNumber() == 2048)
            {
                Debug.Log("Found tile with number 2048 - game is won");
                winPanel.SetActive(true);
            }
        }
    }
    
    public void ContinueAfterWin()
    {
        Debug.Log("User wants to continue the game...");
        winPanel.SetActive(false);
    }
    
    
    void OnApplicationQuit() 
    {
        Debug.Log("Persisting tiles numbers before quit game");
        int[] tileNumbers = new int[TILES_COUNT];
        for(int i = 0; i < TILES_COUNT; i++)
        {
            tileNumbers[i] = tiles[i].getCellNumber();
        }
        GameSaveLoad.GAME_DATA.setTileCellValues(tileNumbers);
        GameSaveLoad.SaveGame();
    }

}
