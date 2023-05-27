using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryDebug : MonoBehaviour
{
    public GameObject gobject;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            InventoryManager.Instance.AddItem(gobject, 5);
        }
    }
}
