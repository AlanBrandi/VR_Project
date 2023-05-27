using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance = null;
    public GameObject inventory;
    AudioSource audioSource;
    [SerializeField] AudioClip pickUpClip;
    [SerializeField] AudioClip pickDownClip;
    [SerializeField] AudioClip pickBothClip;
    [SerializeField] Grab playerGrab;

    public Transform inventorySlotHolder;

    public Transform cursor;
    public Vector3 offset;

    public List<bool> isFull;
    public List<Transform> slots;

    public int currentSlot;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        initializeInventory();
        SetSlotsIDs();
        CheckSlots();
    }
    private void Update()
    {
        if (inventory.activeSelf == true)
        {
            cursor.position = Input.mousePosition + offset;
        }
        if (cursor.childCount > 0)
        {
            cursor.gameObject.SetActive(true);
        }
        else
        {
            cursor.gameObject.SetActive(false);
        }
    }
    void initializeInventory() //Sets slots.
    {
        for (int i = 0; i < inventorySlotHolder.childCount; i++)
        {
            slots.Add(inventorySlotHolder.GetChild(i));
            isFull.Add(false);
        }
    }

    void SetSlotsIDs()
    {
        for (int i = 0; i < slots.Count; i++)
        {
            if (slots[i].GetComponent<Slot>() != null)
            {
                slots[i].GetComponent<Slot>().ID = i;
            }
        }
    }

    void CheckSlots() //Check if slots are full.
    {
        for (int i = 0; i < slots.Count; i++)
        {
            if (slots[i].childCount > 0)
            {
                isFull[i] = true;
            }
            else
            {
                isFull[i] = false;
            }
        }
    }

    public void AddItem(GameObject item, int amount)
    {
        int remainingAmount = amount;
        for (int i = 0; i < slots.Count; i++)
        {
            InventoryItem slotItem = slots[i].GetComponentInChildren<InventoryItem>();
            if (slotItem != null && slotItem.itemData.ID == item.GetComponent<InventoryItem>().itemData.ID && slotItem.amount < slotItem.itemData.maxStack)
            {
                int spaceAvailable = slotItem.itemData.maxStack - slotItem.amount;
                int itemsToAdd = Mathf.Min(remainingAmount, spaceAvailable);
                slotItem.amount += itemsToAdd;
                remainingAmount -= itemsToAdd;
                if (remainingAmount <= 0)
                {
                    break; // No more items to add
                }
            }
        }

        for (int i = 0; i < slots.Count; i++)
        {
            InventoryItem slotItem = slots[i].GetComponentInChildren<InventoryItem>();
            if (remainingAmount <= 0)
            {
                break; // No more items to add
            }
            if (slotItem == null)
            {
                int itemsToAdd = Mathf.Min(remainingAmount, item.GetComponent<InventoryItem>().itemData.maxStack);

                GameObject newItem = Instantiate(item, slots[i]);
                newItem.GetComponent<InventoryItem>().amount = itemsToAdd;

                remainingAmount -= itemsToAdd;
            }
        }
        CheckSlots();
    }
    #region UnusedCode
    /*public void RemoveItem(int ID, int amount)
    {
        for (int i = 0; i < slots.Count; i++)
        {
            if (isFull[i] == true)
            {
                if (slots[i].GetChild(0).GetComponent<InventoryItem>().itemData.ID == ID && slots[i].GetChild(0).GetComponent<InventoryItem>().amount >= amount)
                {
                    slots[i].GetChild(0).GetComponent<InventoryItem>().amount -= amount;
                }
                else
                {
                    Debug.Log("Not enough items to complete action.");
                }
            }
        }
    }*/
    #endregion 
    public void UseItem(int ID, int amount)
    {
        bool itemUsed = false;
        for (int i = 0; i < slots.Count; i++)
        {
            Slot slot = slots[i].GetComponent<Slot>();
            if (slot != null && slot.GetItemID() == ID)
            {
                InventoryItem selectedSlotItem = slots[i].GetComponentInChildren<InventoryItem>();
                if (selectedSlotItem.amount >= amount)
                {
                    if (selectedSlotItem.itemData.holdable)
                    {
                    }
                    else
                    {
                        selectedSlotItem.amount -= amount;
                    }
                    selectedSlotItem.UpdateUI();

                    if (selectedSlotItem.amount <= 0)
                    {
                        Destroy(selectedSlotItem.gameObject);
                        isFull[i] = false;
                    }

                    itemUsed = true;
                    break;
                }
            }
        }
        if (!itemUsed)
        {
            Debug.Log("Wrong item or not enough items to complete action.");
        }
    }


    public void PickupDropInventory()
    {
        if (slots[currentSlot].childCount > 0 && cursor.childCount < 1)
        {
            //Put inside cursor.
            audioSource.clip = pickUpClip;
            audioSource.Play();
            Instantiate(slots[currentSlot].GetChild(0).gameObject, cursor);
            Destroy(slots[currentSlot].GetChild(0).gameObject);
        }
        else if (slots[currentSlot].childCount < 1 && cursor.childCount > 0)
        {
            audioSource.clip = pickDownClip;
            audioSource.Play();
            Instantiate(cursor.GetChild(0).gameObject, slots[currentSlot]);
            Destroy(cursor.GetChild(0).gameObject);
        }
        else if (slots[currentSlot].childCount > 0 && cursor.childCount > 0)
        {
            audioSource.clip = pickBothClip;
            audioSource.Play();
            InventoryItem slotItem = slots[currentSlot].GetChild(0).GetComponent<InventoryItem>();
            InventoryItem cursorItem = cursor.GetChild(0).GetComponent<InventoryItem>();
            if (slotItem.itemData.ID == cursorItem.itemData.ID)
            {
                if (slotItem.amount + cursorItem.amount <= slotItem.itemData.maxStack && cursorItem.isStackable)
                {
                    slotItem.amount += cursorItem.amount;
                    Destroy(cursor.GetChild(0).gameObject);
                }
                if (slotItem.amount + cursorItem.amount > slotItem.itemData.maxStack)
                {
                    int amountForMax = slotItem.itemData.maxStack - slotItem.amount;
                    slotItem.amount += amountForMax;
                    cursorItem.amount -= amountForMax;
                }
            }
        }
        CheckSlots();
    }

    public void ToggleInventory()
    {
        inventory.SetActive(!inventory.activeSelf);
    }
}