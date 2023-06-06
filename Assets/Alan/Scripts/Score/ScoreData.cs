using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New ScoreData", menuName = "ScoreData")]
public class ScoreData : ScriptableObject
{
   public ScoreDataBest scoreDataBest;
   public float timeRemain;
   public int healthRemain;
   public int pointsCollected;
   public int healthItems;
   public bool escaped;
   public int scoreTotal;
   
   private int lastScore;
   private int lastTime;

   public void ResetData()
   {
      timeRemain = 0;
      healthRemain = 100;
      pointsCollected = 0;
      healthItems = 0;
      escaped = false;
      scoreTotal = 0;
   }
   public int TotalScore()
   {
      scoreTotal += Mathf.RoundToInt(timeRemain) * 100;
      scoreTotal += healthRemain * 10;
      scoreTotal += healthItems;
      scoreTotal += pointsCollected * 100;
      if (escaped)
      {
         scoreTotal += 1000;
      }
      SaveBestScore();
      lastScore = scoreTotal;
      lastTime = Mathf.RoundToInt(timeRemain);
      return scoreTotal;
   }
   public void SaveBestScore()
   {
      if (scoreTotal > lastScore)
      {
         scoreDataBest.bestScore = scoreTotal;
      }
      else
      {
         scoreDataBest.bestScore = lastScore;
      }
      if (timeRemain > lastTime)
      {
         scoreDataBest.bestTime = timeRemain;
      }
      else
      {
         scoreDataBest.bestTime = lastTime;
      }
   }
}
