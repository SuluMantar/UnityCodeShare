using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    // Character's inventory slots
    public InventorySlots[] inventorySlots;
    //Item to spawn 
    public GameObject inventoryItemPrefab;

    private int maxStackSize = 6;


    // How to Additem in Slots 
    // It checks that is there any item in that slot or not 
    public bool AddItem(Item item)
    {
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlots slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot != null && itemInSlot.item == item && itemInSlot.item.isStackable && itemInSlot.itemAmount < maxStackSize)
            {
                itemInSlot.itemAmount++;
                itemInSlot.RefreshCount();
                return true;
            }
        }




        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlots slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if(itemInSlot == null)
            {
                SpawnNewItem(item, slot);
                return true;
            }
        }
        return false;
    }


    // To Add item to slot we have to spawn item and place it in that slot 
    void SpawnNewItem(Item item, InventorySlots slot)
    {
        GameObject newItemGo = Instantiate(inventoryItemPrefab, slot.transform);
        // Get the Inventoryitem Script to Do InitialiseItem Function
        //This Function just change our SpawnedItem's image and features according to our item
        InventoryItem inventoryItem = newItemGo.GetComponent<InventoryItem>();
        inventoryItem.InitialiseItem(item);
    }
}
