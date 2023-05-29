using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverScore : MonoBehaviour
{
    [SerializeField] private ScoreData scoreData;
    [SerializeField] private TMP_Text finalScore;
    [SerializeField] private TMP_Text timeScore;
    [SerializeField] private TMP_Text result;


    private void Awake()
    {
       finalScore.text = scoreData.TotalScore().ToString();
       int minutes = (int)(scoreData.timeRemain / 60);
       int seconds = (int)(scoreData.timeRemain % 60);
       string formattedTime = string.Format("{0:00}:{1:00}", minutes, seconds);
       timeScore.text = formattedTime;

       if (scoreData.escaped)
       {
           result.text = "YOU ESCAPED!";
       }
       if (scoreData.timeRemain <= 0)
       {
           result.text = "YOU SURVIVED!";
       }
       else if(scoreData.healthRemain < 0)
       {
           result.text = "YOU LOSE!";
       }
       
    }
}
