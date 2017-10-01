using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MaxScore : MonoBehaviour
{
    public int MaxValue {get;set;}
    
    private Text ScoreText;
    
    public void Start()
    {
        ScoreText = transform.Find("HighScoreValue").gameObject.GetComponent<Text>();
        MaxValue = GameSaveLoad.GAME_DATA.getHighScore();
        UpdateText();
    }
    
    public void CompareAndUpdate(int Score)
    {
        Debug.Log("Comparing score with max score...");
        if(MaxValue < Score)
        {
            Debug.Log("Update and persist max score");
            MaxValue = Score;
            GameSaveLoad.GAME_DATA.setHighScore(Score);
            UpdateText();
            return;
        }
        Debug.Log("Max score wasn't updated");
    }
    
    private void UpdateText()
    {
        ScoreText.text = MaxValue+"";
        ScoreText.enabled = true;
    }
}