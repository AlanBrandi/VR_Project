using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class HealthSystem : MonoBehaviour
{
    [Header("Set life")]
    public float maxHealth;

    [Header("Data")]
    [SerializeField] private ScoreData scoreData;
    [SerializeField] private float currentHealth;
    
    [Header("Audio")]
    [SerializeField] private AudioSource hitSound;
    
    [Header("Canvas")]
    [SerializeField] private GameObject hitPanel;

    private bool died = false;
    private void Awake()
    {
        currentHealth = maxHealth;
    }
    private void Start()
    {
        scoreData.healthRemain = Mathf.RoundToInt(currentHealth);
    }

    
    public float GetCurrentHealth()
    {
        return currentHealth;
    }
    public void Damage(float value)
    {
        currentHealth -= value;
        scoreData.healthRemain = Mathf.RoundToInt(currentHealth);
        hitPanel.SetActive(true);
        hitSound.pitch = Random.Range(0.3f, 0.6f);
        hitSound.Play();
        Invoke(nameof(DisableHitPanel),2);
        if (currentHealth <= 0)
        {
            died = true;
            SceneManager.LoadScene("GameOver");
        }
    }
    public void UsePowerUp(float value)
    {
        currentHealth += value;
        scoreData.healthRemain = Mathf.RoundToInt(currentHealth);
    }

    private void DisableHitPanel()
    {
        hitPanel.SetActive(false);
    }
}
