using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private ScoreDataBest scoreDataBest;
    [SerializeField] private TMP_Text bestScore;
    [SerializeField] private TMP_Text bestTime;


    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void CloseGame()
    {
        Application.Quit();
    }
    
    private void Awake()
    {
        bestScore.text = scoreDataBest.bestScore.ToString();
       int minutes = (int)(scoreDataBest.bestTime / 60);
       int seconds = (int)(scoreDataBest.bestTime % 60);
       string formattedTime = string.Format("{0:00}:{1:00}", minutes, seconds);
       bestTime.text = formattedTime;
    }
}