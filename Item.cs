using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public enum ItemType
{
    BuildingBlock,
    Tool
}

[CreateAssetMenu(menuName = "Scriptable object/Item")]


public class Item : ScriptableObject
{
    public TileBase tile;
    public Sprite image;
    public ItemType type;
    public bool isStackable;
    public string nameOfItem;
    public Vector2Int range = new Vector2Int(5, 4);
    public ActionType actionType;
    public int quantityToCraft;
    public int maxStackSize;

    


    public enum ActionType
	{
	         Dig,
            Mine
	}



}
