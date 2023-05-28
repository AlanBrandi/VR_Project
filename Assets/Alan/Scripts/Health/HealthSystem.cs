﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using Random = UnityEngine.Random;

public class HealthSystem : MonoBehaviour
{
    [Header("Set life")] public float maxHealth;

    [Header("Data")] [SerializeField] private ScoreData scoreData;
    [SerializeField] private float currentHealth;

    [Header("Audio")] [SerializeField] private AudioSource hitSound;

    [Header("Canvas")] 
    [SerializeField] private GameObject hitPanel;
    [SerializeField] private GameObject heartIcon;
    [SerializeField] private TMP_Text healthText;
    [SerializeField] private float smoothDecreaseDuration = 0.5f;


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
        StartCoroutine(nameof(SmoothDecreaseHealth), value);
        hitPanel.SetActive(true);
        hitSound.pitch = Random.Range(0.3f, 0.6f);
        hitSound.Play();
        Invoke(nameof(DisableHitPanel), 0.5f);
        if (currentHealth <= 0)
        {
            died = true;
            SceneManager.LoadScene("GameOver");
        }
    }

    private IEnumerator SmoothDecreaseHealth(float damage)
    {
        float damagePerTick = damage / smoothDecreaseDuration;
        float elapsedTime = 0f;
        while (elapsedTime < smoothDecreaseDuration)
        {
            heartIcon.GetComponent<Image>().color = Color.red;
            healthText.color = Color.red;
            float currentDamage = damagePerTick * Time.deltaTime;
            currentHealth -= currentDamage;
            elapsedTime += Time.deltaTime;
            UpdateHealthText();
            if (currentHealth <= 0)
            {
                currentHealth = 0;
                break;
            }

            yield return null;
        }
        heartIcon.GetComponent<Image>().color = Color.white;
        healthText.color = Color.white;
    }
    private IEnumerator SmoothIncreaseHealth(float inscrease)
    {
        float damagePerTick = inscrease / smoothDecreaseDuration;
        float elapsedTime = 0f;
        while (elapsedTime < smoothDecreaseDuration)
        {
            heartIcon.GetComponent<Image>().color = Color.green;
            healthText.color = Color.green;
            float currentDamage = damagePerTick * Time.deltaTime;
            currentHealth += currentDamage;
            elapsedTime += Time.deltaTime;
            UpdateHealthText();
            yield return null;
        }
        heartIcon.GetComponent<Image>().color = Color.white;
        healthText.color = Color.white;
    }

    private void UpdateHealthText()
    {
        healthText.text = currentHealth.ToString("0");
    }
    public void UsePowerUp(float value)
    {
        if (value + currentHealth > maxHealth)
        {
            value = (((value + currentHealth) - maxHealth) - value) * -1;
            StartCoroutine(nameof(SmoothIncreaseHealth), value);
        }
        else
        {
            StartCoroutine(nameof(SmoothIncreaseHealth), value);
        }
       
    }

    private void DisableHitPanel()
    {
        hitPanel.SetActive(false);
    }
}
