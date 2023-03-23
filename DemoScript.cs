using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoScript : MonoBehaviour
{
    public InventoryManager inventoryManager;



    // To Add and Visualize Item 
    public void PickUpItem(Item item)
    {
        bool result = inventoryManager.AddItem(item, 1);
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
