using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewGame : MonoBehaviour
{
    private GameManager gm;
    private Score score;
    
    public void Start()
    {
        gm = GameObject.FindObjectOfType<GameManager>();
        score = GameObject.FindObjectOfType<Score>();
        createNewGame();
    }
        
    public void createNewGame()
    {
        Debug.Log("New game started");
        gm.CreateNewGame();
        score.Reset();
        gm.gameOverPanel.SetActive(false);
        gm.winPanel.SetActive(false);
    }
}