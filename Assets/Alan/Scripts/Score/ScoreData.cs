using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New ScoreData", menuName = "ScoreData")]
public class ScoreData : ScriptableObject
{
   public float timeRemain;
   public int healthRemain;
   public int pointsCollected;
   public int healthItens;
   public bool escaped;
}
