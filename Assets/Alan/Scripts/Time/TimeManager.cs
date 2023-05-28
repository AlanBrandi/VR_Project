using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimeManager : MonoBehaviour
{
    [Header("Timer configs")]
    [SerializeField] private float maxTime = 60f; //preciso salvar isso em algum data
    [SerializeField] private float damagePerTime = 5;
    [SerializeField] private float interval = 1f;
    
    [Header("Player health")]
    [SerializeField] private HealthSystem healthSystem;

    [Header("Player health")] 
    [SerializeField] private string sceneName;

    [Header("Canvas")] 
    [SerializeField] private TMP_Text timerDisplay;
    
    private float _currentTime = 0f; 

    private void Update()
    {
        _currentTime += Time.deltaTime;

        while (_currentTime >= interval)
        {
            _currentTime -= interval;
            timerDisplay.color = Color.red;
            Invoke("ChangeColorToNormal",0.5f);
            healthSystem.Damage(damagePerTime);
        }

        maxTime -= Time.deltaTime;

        if (maxTime <= 0f)
        {
            SceneManager.LoadScene(sceneName);
            enabled = false;
        }
        int minutes = (int)(maxTime / 60);
        int seconds = (int)(maxTime % 60);
        string formattedTime = string.Format("{0:00}:{1:00}", minutes, seconds);
        
        timerDisplay.text = formattedTime;
    }

    private void ChangeColorToNormal()
    {
        timerDisplay.color = Color.white;
    }
}
