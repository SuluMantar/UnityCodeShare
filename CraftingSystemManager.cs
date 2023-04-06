using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingSystemManager : MonoBehaviour
{
    [SerializeField]
    private InventoryManager inventory;

    [SerializeField]
    private CraftingRecipe[] recipes;

    [SerializeField]
    private TimeScript timer;

    public int Days;
    public int Hours;
    public int Minutes;
    public int Seconds;

    public void Craft()
    {
        foreach (CraftingRecipe recipe in recipes)
        {
            if (recipe.CanCraft(inventory))
            {
                inventory.CraftingInProgress(false);
                timer.StartTimer(Days, Hours, Minutes, Seconds);
                StartCoroutine(WaitForCrafting(recipe));
            }
            else
            {
                Debug.Log(" You dont have enough material");
            }
        }
    }

    IEnumerator WaitForCrafting(CraftingRecipe recipe)
    {
        while (timer.inProgress)
        {
            yield return null;
        }
        inventory.CraftingInProgress(true);
        recipe.Craft(inventory);
    }

}
