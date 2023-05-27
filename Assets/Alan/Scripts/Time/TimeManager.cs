using UnityEngine;
using UnityEngine.SceneManagement;

public class TimeManager : MonoBehaviour
{
    [Header("Timer configs")]
    [SerializeField] private float maxTime = 60f;
    [SerializeField] private float interval = 1f;
    
    [Header("Player health")]
    [SerializeField] private HealthSystem healthSystem;

    [Header("Player health")] 
    [SerializeField] private string sceneName;
    
    private float _currentTime = 0f; //preciso salvar isso em algum data

    private void Update()
    {
        _currentTime += Time.deltaTime;

        while (_currentTime >= interval)
        {
            _currentTime -= interval;
            healthSystem.Damage(30);
        }

        maxTime -= Time.deltaTime;

        if (maxTime <= 0f)
        {
            SceneManager.LoadScene(sceneName);
            enabled = false;
        }
    }
}
