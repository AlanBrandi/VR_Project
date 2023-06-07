using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour
{
    public Item itemData;
    public TMPro.TMP_Text amountText, healthRecoveredText;
    public bool isStackable;
    private Image itemIcon;

    public int amount = 1, healthUp = 0;

    #region Old Text Update
    /*private void Update()
    {
        if (isStackable)
        {
            if (amountText != null)
            {
                amountText.text = amount.ToString();
            }
            else
            {
                Debug.LogWarning("InventoryItem amount text not set.");
            }
        }
    }*/
    #endregion

    private void Start()
    {
        UpdateUI();
    }
    public void UpdateUI()
    {
        if (isStackable)
        {
            if (amountText != null && healthRecoveredText != null)
            {
                amountText.text = amount.ToString();
                healthRecoveredText.text = healthUp.ToString();
            }
            else
            {
                Debug.LogWarning("InventoryItem amount or healthRecovered text not set.");
            }
        }
    }

    private void OnValidate()
    {
        if (itemData.icon != null)
        {
            itemIcon = GetComponent<Image>();
            itemIcon.sprite = itemData.icon;
        }
        else
        {
            Debug.Log("No itemIcon set.");
        }
    }
}
