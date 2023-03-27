using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoScript : MonoBehaviour
{
    public InventoryManager inventoryManager;

    public int amountToAdd = 1;

    // To Add and Visualize Item 
    public void PickUpItem(Item item)
    {
        bool result = inventoryManager.AddItem(item, amountToAdd);
        inventoryManager.AddItemToDic(item, amountToAdd);
        if(result){
            Debug.Log("Item added");
        }
        else
        {
            Debug.Log("Item Not added");
        }
    }

    public void GetItem(Item item)
    {
        inventoryManager.GetItemFromDic(item);
    }

    public void RemoveItem(Item item)
    {
        inventoryManager.RemoveItemFromDic(item,1);
    }
}
