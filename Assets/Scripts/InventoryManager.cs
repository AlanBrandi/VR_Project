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
        for (int i = 0; i < amount; i++)
        {
            for (int x = 0; x < slots.Count; x++)
            {
                if (isFull[x] == false)
                {
                    Instantiate(item, slots[x]);
                    CheckSlots();
                    return;
                }
                else
                {
                    Debug.Log("Slot is full.");
                }
            }
        }
        Debug.Log("No inventory space.");
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