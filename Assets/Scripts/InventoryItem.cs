using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour
{
    public Item itemData;
    public TMPro.TMP_Text amountText;
    public bool isStackable;

    public int amount = 1;

    private void Update()
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
    }
}
