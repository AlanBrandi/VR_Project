using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
  [SerializeField] private ScoreData scoreData;

  private void OnTriggerEnter(Collider other)
  {
      if (other.CompareTag("Player"))
      {
          scoreData.escaped = true;
          SceneManager.LoadScene("GameOver");
      }
  }
}
