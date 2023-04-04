using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    // Character's inventory slots
    public InventorySlots[] inventorySlots;
    // Crafting Slots
    public InventorySlots[] craftingSlots;
    // Crafted Item Slot
    public InventorySlots craftedItemSlot;
    //Item to spawn 
    public GameObject inventoryItemPrefab;

    private int maxStackSize = 24;

    Dictionary<Item, int> itemDic = new Dictionary<Item, int>();


    // How to Additem in Slots 
    // It checks that is there any item in that slot or not 
    public bool AddItem(Item item, int amountToAdd)
    {

        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlots slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot != null && itemInSlot.item == item && itemInSlot.item.isStackable && itemInSlot.itemAmount < maxStackSize)
            {
                itemInSlot.itemAmount += amountToAdd;
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
                SpawnNewItem(item, slot, amountToAdd);
                return true;
            }
            
        }
        return false;
    }

    public bool IsThereItemOnOutput() 
    {
        InventoryItem outputItemInSlot = craftedItemSlot.GetComponentInChildren<InventoryItem>();
        if (outputItemInSlot == null)
        {
            Debug.Log("Its empty");
            return true;
        }
        Debug.Log("Output is full");
        return false;

    }




    public void AddItemToOutput(Item item, int amountToAdd)
    {
        InventoryItem outputItemInSlot = craftedItemSlot.GetComponentInChildren<InventoryItem>();

        if (outputItemInSlot == null)
        {
            SpawnNewItem(item, craftedItemSlot, amountToAdd);
            Debug.Log("Added");
        }
    }

    // To Add item to slot we have to spawn item and place it in that slot 
    void SpawnNewItem(Item item, InventorySlots slot, int amount)
    {
        GameObject newItemGo = Instantiate(inventoryItemPrefab, slot.transform);
        // Get the Inventoryitem Script to Do InitialiseItem Function
        //This Function just change our SpawnedItem's image and features according to our item
        InventoryItem inventoryItem = newItemGo.GetComponent<InventoryItem>();
        inventoryItem.InitialiseItem(item,amount);
    }


    // To Get certain item from our inventory
    public Item GetItemFromDic(Item item)
    {
        int itemAmount;
        if (itemDic.TryGetValue(item, out itemAmount))
        {
            Debug.Log("Found item " + item.name + " with amount " + itemAmount);
            return item;
        }
        else
        {
            Debug.Log("Item " + item.name + " not found");
            return null;
        }

    }


    // To Add new item to our inventory 
    // AddItem function is for visualizition this function add item to our inventory
    public void AddItemToDic(Item item, int Amount)
    {
        if (itemDic.ContainsKey(item))
        {
            Debug.Log("Item Added To Dic");
            // Item already exists in dictionary, increment its amount
            itemDic[item] += Amount;
        }
        else
        {
            Debug.Log("Item Added To Dic");
            // Item does not exist in dictionary, add it
            itemDic.Add(item, Amount);
        }
    }

    private InventoryItem GetInventoryItemByItem(Item item)
    {
        foreach (InventorySlots slot in inventorySlots)
        {
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot != null && itemInSlot.item == item)
            {
                return itemInSlot;
            }
        }
        return null;
    }

    public void RemoveItemFromDic(Item item, int amountToRemove)
    {
        InventoryItem itemInSlot = GetInventoryItemByItem(item);
        if (item != null && itemDic.ContainsKey(item))
        {
            // Remove From Dictionary
            int currentAmount = itemDic[item];
            if (currentAmount > amountToRemove)
            {
                itemDic[item] -= amountToRemove;
            }
            else
            {
                itemDic.Remove(item);
                amountToRemove = currentAmount;
            }

            // If the item has a stackable GameObject, decrease its stack size
            // To Update Visualization
            if (item.isStackable)
            {
                if (itemInSlot != null)
                {
                    itemInSlot.itemAmount -= amountToRemove;
                    if (itemInSlot.itemAmount <= 0)
                    {
                        Destroy(itemInSlot.gameObject);
                    }
                    else
                    {
                        itemInSlot.RefreshCount();
                    }
                }
            }
            else
            {
                Destroy(itemInSlot.gameObject);

            }
        }
        
    }


    private InventoryItem GetCraftingItemByItem(Item item)
    {
        foreach (InventorySlots slot in craftingSlots)
        {
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot != null && itemInSlot.item == item)
            {
                return itemInSlot;
            }
        }
        return null;
    }

    public void RemoveItemFromDicInCraft(Item item, int amountToRemove)
    {
        InventoryItem itemInSlot = GetCraftingItemByItem(item);

        if (item != null && itemInSlot.itemAmount > amountToRemove)
        {
            itemInSlot.itemAmount -= amountToRemove;
            itemInSlot.RefreshCount();
        }
        else
        {
            itemDic.Remove(item);
            Destroy(itemInSlot.gameObject);
        }
    }



    public bool Contains(Item item, int necessaryAmountToCraft)
    {
        foreach (InventorySlots slot in craftingSlots)
        {
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot != null && itemInSlot.item == item && itemInSlot.GetQuantity() >= necessaryAmountToCraft)
            {
                return true;
            }
        }
        return false;
    }


        
}
