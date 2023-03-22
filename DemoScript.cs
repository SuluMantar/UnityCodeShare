using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoScript : MonoBehaviour
{
    public InventoryManager inventoryManager;



    // To Add and Visualize Item 
    public void PickUpItem(Item item)
    {
        bool result = inventoryManager.AddItem(item);
        if(result){
            Debug.Log("Item added");
        }
        else
        {
            Debug.Log("Item Not added");
        }
    }
   
}
