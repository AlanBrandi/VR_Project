using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        scoreDataBest.bestScore = PlayerPrefs.GetFloat("BestScore");
        scoreDataBest.bestTime = PlayerPrefs.GetFloat("BestTime");
        bestScore.text = scoreDataBest.bestScore.ToString();
        int minutes = (int)(scoreDataBest.bestTime / 60);
        int seconds = (int)(scoreDataBest.bestTime % 60);
        string formattedTime = string.Format("{0:00}:{1:00}", minutes, seconds);
        bestTime.text = formattedTime;
    }
}