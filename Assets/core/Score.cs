using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public int ScoreNumber {get;set;}
    
    public Text ScoreText;
    
    public MaxScore MaxScore {get;set;}
    
    public void Start()
    {
        MaxScore =  GameObject.FindObjectOfType<MaxScore>();
        ScoreNumber = GameSaveLoad.GAME_DATA.getScore();
        UpdateText();
    }
    
    public void Increment(int value)
    {
        Debug.Log("add to score number = "+value);
        ScoreNumber += value;
        MaxScore.CompareAndUpdate(ScoreNumber);
        UpdateText();
    }
    
    public void Reset() {
        ScoreNumber = 0;
        UpdateText();
    }
    
    private void UpdateText()
    {
        GameSaveLoad.GAME_DATA.setScore(ScoreNumber);
        ScoreText.text = ScoreNumber+"";
        ScoreText.enabled = true;
    }
    
}