using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingSystemManager : MonoBehaviour
{
    [SerializeField]
    private InventoryManager inventory;

    [SerializeField]
    private CraftingRecipe[] recipes;


    public void Craft(CraftingRecipe recipe)
    {
        if (recipe.CanCraft(inventory))
        {
            recipe.Craft(inventory);
        }
        else
        {
            Debug.Log(" You dont have enough material");
        }
    }

}
