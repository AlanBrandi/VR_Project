using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public int ID;
    public InventoryManager inventoryManager;
    private double _selectTimer;

    private void Start()
    {
        inventoryManager = GameObject.FindGameObjectWithTag("InventoryManager").GetComponent<InventoryManager>();
    }

    public void SelectDeselect(Sprite selectSprite)
    {
        gameObject.GetComponent<Image>().sprite = selectSprite;
    }

    public void InstantiateMenu(GameObject itemMenu)
    {
        Instantiate(itemMenu);
    }

    public void StartTimer()
    {
        StartCoroutine(Timer());
    }

    public void EndTimer()
    {
        StopCoroutine(Timer());
        _selectTimer = 0;
    }

    IEnumerator Timer()
    {
        while (_selectTimer <= 2)
        {
            Debug.Log(_selectTimer);
            _selectTimer += .1;
            if (_selectTimer > 2)
            {
                //RemoveItem
            }
            yield return new WaitForSecondsRealtime(.1f);
        }
    }

    public void SetID()
    {
        inventoryManager.currentSlot = ID;
        inventoryManager.PickupDropInventory();
    }
}
