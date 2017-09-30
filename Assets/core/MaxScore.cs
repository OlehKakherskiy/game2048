using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MaxScore : MonoBehaviour
{
    private int MaxValue;
    
    private Text ScoreText;
    
    public void Start()
    {
        ScoreText = transform.Find("HighScoreValue").gameObject.GetComponent<Text>();
    }
    
    public void CompareAndUpdate(int Score)
    {
        if(MaxValue < Score)
        {
            MaxValue = Score;
            UpdateText();
        }
    }
    
    private void UpdateText()
    {
        ScoreText.text = MaxValue+"";
        ScoreText.enabled = true;
    }
}