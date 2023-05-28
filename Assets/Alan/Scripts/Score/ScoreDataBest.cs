using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "bestScore", menuName = "New ScoreDataBest")]
public class ScoreDataBest : ScriptableObject
{
   public float bestTime;
   public float bestScore;
}
