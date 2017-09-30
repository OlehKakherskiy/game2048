using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    private int score;
    
    public Text ScoreText;
    
    private MaxScore maxScore;
    
    public void Start()
    {
        maxScore =  GameObject.FindObjectOfType<MaxScore>();
        score = 0;
        Debug.Log(ScoreText.text);
        Reset();
    }
    
    public void Increment(int value)
    {
        score += value;
        maxScore.CompareAndUpdate(score);
        UpdateText();
    }
    
    public void Reset(){
        score = 0;
        UpdateText();
    }
    
    public int GetScore()
    {
        return score;
    }
    
    private void UpdateText()
    {
        ScoreText.text = score+"";
        ScoreText.enabled = true;
    }
    
}