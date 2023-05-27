using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class CollectableItem : MonoBehaviour, IGvrPointerHoverHandler
{
    [SerializeField] GameObject _item;
    public void OnGvrPointerHover(PointerEventData eventData)
    {
        InventoryManager.Instance.AddItem(_item, 5);
        Destroy(gameObject);
    }
}
