using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public int ID;
    private int itemID;
    public InventoryManager inventoryManager;
    private double _selectTimer;
    private bool _stoppedTimer;
    private Coroutine timerCoroutine;
    [HideInInspector] public float timer;

    private Image progressBar;

    private void Start()
    {
        progressBar = GameObject.Find("ProgressBar").GetComponent<Image>();
        inventoryManager = GameObject.FindGameObjectWithTag("InventoryManager").GetComponent<InventoryManager>();
    }

    /*private void Update()
    {
        if (transform.childCount > 0)
        {
            if (transform.GetChild(0).GetComponent<InventoryItem>().amount <= 0)
            {
                Destroy(transform.GetChild(0).gameObject);
            }
        }
    }*/ //Old destroy item method. 

    public void SelectDeselect(Sprite selectSprite)
    {
        gameObject.GetComponent<Image>().sprite = selectSprite;
    }

    public void InstantiateMenu(GameObject itemMenu)
    {
        Instantiate(itemMenu);
    }

    #region VRSelectionTimer
    public void StartTimer()
    {
        if (transform.childCount > 0)
        {
            
            itemID = transform.GetChild(0).GetComponent<InventoryItem>().itemData.ID;
            if (timerCoroutine == null)
            {
                //Cursor animation starts here.
                progressBar.gameObject.SetActive(true);
                timerCoroutine = StartCoroutine(Timer(itemID));
            }
        }
    }

    public void EndTimer()
    {
        _stoppedTimer = true;
        progressBar.gameObject.SetActive(false);
        if (timerCoroutine != null)
        {
            //Cursor animation gets interrupted here.
            StopCoroutine(timerCoroutine);
            timerCoroutine = null;
        }
        _selectTimer = 0f;
        _stoppedTimer = false;
    }

    IEnumerator Timer(int id)
    {
        float timer = 0f;
        float duration = 1.5f;
        float fillAmountMax = 100f;

        while (!_stoppedTimer)
        {
            timer += Time.deltaTime;
            float normalizedTimer = timer / duration;
            float fillAmount = Mathf.Clamp(normalizedTimer * fillAmountMax, 0f, fillAmountMax);

            progressBar.fillAmount = fillAmount / fillAmountMax;

            if (timer > duration)
            {
                // Cursor animation ends here.
                inventoryManager.UseItem(id, 1);
                timer = 0f;
                StopCoroutine(Timer(1));
            }
            
            yield return null;
        }

        timer = 0f;
        _stoppedTimer = false;
        timerCoroutine = null;
    }
    #endregion

    public void SetID()
    {
        inventoryManager.currentSlot = ID;
        //inventoryManager.PickupDropInventory();
    }

    public int GetItemID()
    {
        if (transform.childCount > 0)
        {
            InventoryItem item = transform.GetChild(0).GetComponent<InventoryItem>();
            if (item != null)
            {
                return item.itemData.ID;
            }
        }
        return -1;
    }
}
