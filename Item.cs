using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Scriptable object/Item")]


public class Item : ScriptableObject
{
    public Sprite image;
    public ItemType type;
    public bool isStackable;
    public string name;


    public enum ItemType
    {
        BuildingBlock,
        Tool
    }


}
