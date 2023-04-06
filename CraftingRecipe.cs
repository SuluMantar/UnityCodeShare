
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CraftingRecipe", menuName = "CraftingRecipe/Recipe", order = 0)]
public class CraftingRecipe : ScriptableObject
{
    public Item[] itemsForRecipes;
    public Item craftedItem;

    public bool CanCraft(InventoryManager manager)
    {
        for (int i = 0; i < itemsForRecipes.Length; i++)
        {
            if (!manager.Contains(itemsForRecipes[i], itemsForRecipes[i].quantityToCraft) && manager.IsThereItemOnOutput())
                return false;

        }
        return true;

    }

    public void Craft(InventoryManager manager)
    {
        for (int i = 0; i < itemsForRecipes.Length; i++)
        {

            if (manager.IsThereItemOnOutput())
            {
               manager.RemoveItemFromDicInCraft(itemsForRecipes[i], itemsForRecipes[i].quantityToCraft); 
            }

        }

        if (manager.IsThereItemOnOutput())
        {
            manager.AddItemToOutput(craftedItem, 1);
            manager.AddItemToDic(craftedItem, 1);  
        }


    }


}
