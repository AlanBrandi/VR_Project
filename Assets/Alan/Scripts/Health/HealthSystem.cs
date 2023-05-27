using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    [Header("Set life")]
    [SerializeField] private float maxHealth;

    [Header("Data")]
    [SerializeField] private ScoreData scoreData;
    
    [SerializeField] private float currentHealth;

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
        if (currentHealth <= 0)
        {
            //morrer
        }
    }
    public void UsePowerUp(float value)
    {
        currentHealth += value;
        scoreData.healthRemain = Mathf.RoundToInt(currentHealth);
    }
}
