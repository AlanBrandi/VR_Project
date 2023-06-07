using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class CollectableItem : MonoBehaviour, IGvrPointerHoverHandler
{
    [SerializeField] GameObject _item;
    [SerializeField] int _amount;
    public void OnGvrPointerHover(PointerEventData eventData)
    {
        InventoryManager.Instance.AddItem(_item, _amount);
        Destroy(gameObject);
    }
}
