using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlots : MonoBehaviour, IDropHandler
{

    public void OnDrop(PointerEventData eventData)
    {
        if (transform.childCount == 0)
        {
            InventoryItem inventoryItem = eventData.pointerDrag.GetComponent<InventoryItem>();
            inventoryItem.parentAfterDrag = transform;
        }
        else
        {
            InventoryItem itemOnSlot = GetComponentInChildren<InventoryItem>();
            InventoryItem itemOnDrag = eventData.pointerDrag.GetComponent<InventoryItem>();
            if (itemOnDrag != null && itemOnSlot != null && itemOnSlot.item == itemOnDrag.item && itemOnSlot.item.isStackable)
            {
                int total = itemOnDrag.itemAmount + itemOnSlot.itemAmount;
                int maxStackSize = itemOnSlot.item.maxStackSize;
                if (total <= maxStackSize)
                {
                    // stack items
                    itemOnSlot.itemAmount += itemOnDrag.itemAmount;
                    itemOnSlot.RefreshCount();
                    Destroy(itemOnDrag.gameObject);
                }
                else
                {
                    // partially stack items
                    itemOnSlot.itemAmount = maxStackSize;
                    itemOnSlot.RefreshCount();
                    itemOnDrag.itemAmount = total - maxStackSize;
                    itemOnDrag.RefreshCount();
                }
            }
        }
    }

  
}


