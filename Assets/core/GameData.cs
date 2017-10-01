using System.Collections.Generic;

[System.Serializable]
public class GameData
{
    private int[] tileCellValues;
    private int score;
    private int highScore;
    
    public GameData() {
        tileCellValues = new int[16];
        for(int i = 0; i < tileCellValues.Length; i++)
        {
            tileCellValues[i] = 1;
        }
        score = 0;
        highScore = 0;
    }
    
    public void appendTileCellValues(TileCell[] tileCells)
    {
        if(tileCells == null) return;
        tileCellValues = new int[tileCells.Length];
        for(int i = 0; i < tileCells.Length; i++)
        {
            tileCellValues[i] = tileCells[i].getCellNumber();
        }
    }
    
    public void saveScores(Score s)
    {
        if(s == null) return;
        score = s.ScoreNumber;
        highScore = s.MaxScore.MaxValue;
    }
    
    public int[] getCellValues()
    {
        return tileCellValues;
    }
    
    public int getScore()
    {
        return score;
    }
    
    public int getHighScore()
    {
        return highScore;
    }
    
    public void setScore(int score)
    {
        this.score = score;
    }
    
    public void setHighScore(int highScore)
    {
        this.highScore = highScore;
    }
    
    public void setTileCellValues(int[] cellValues)
    {
        this.tileCellValues = cellValues;
    }
    
}