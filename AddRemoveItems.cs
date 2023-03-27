using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public class AddRemoveItems : MonoBehaviour
{
    //This item is what item is selected on inventory, Item that equipped
    [SerializeField]
    private Item item;

    // This field show that which area that we are going to put the item (Preview) 
    [SerializeField]
    private TileBase highlightTile;

    [SerializeField]
    private Tilemap mainTilemap;
    [SerializeField]
    private Tilemap tempTilemap;

    private Vector3Int highlightedTilePos;
    private Vector3Int playerPos;
    private bool highlighted;

    [SerializeField]
    private GameObject lootPrefab;
    [SerializeField]
    InventoryManager inventory;


    private void Start()
    {
        inventory.AddItem(item, 10);
        inventory.AddItemToDic(item,10);
    }


    private void Update()
    {
        playerPos = mainTilemap.WorldToCell(transform.position);

        if (item != null)
        {
            HiglightedTile(item);
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (highlighted)
            {
                if (item.type == ItemType.BuildingBlock)
                {
                    Build(highlightedTilePos, item);
                }
                else if (item.type == ItemType.Tool)
                {
                    Destroy(highlightedTilePos);
                }
            }
        }



    }
    





    private Vector3Int GetMouseOnGridPos()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int mouseCellPos = mainTilemap.WorldToCell(mousePos);
        mouseCellPos.z = 0;

        return mouseCellPos;

    }

    private void HiglightedTile(Item currentItem) 
    {

        Vector3Int mouseGridPos = GetMouseOnGridPos();

        if (highlightedTilePos != mouseGridPos)
        {
            tempTilemap.SetTile(highlightedTilePos, null);


            if (InRange(playerPos, mouseGridPos, (Vector3Int)item.range))
            {
                if (CheckCondition(mainTilemap.GetTile<RuleTileWithData>(mouseGridPos), currentItem))
                {
                    tempTilemap.SetTile(mouseGridPos, highlightTile);
                    highlightedTilePos = mouseGridPos;
                    highlighted = true;
                }
                else
                {
                    highlighted = false;
                }
            }
            else
            {
                highlighted = false;
            }
        }
    
    
    }

    private bool InRange(Vector3Int positionA, Vector3Int positionB, Vector3Int range)
    {
        Vector3Int distance = positionA - positionB;

        if (Math.Abs(distance.x) >= range.x ||
            Math.Abs(distance.y) >= range.y)
        {
            return false;
        }

        return true;


    }



    // Check currentItem that we equipped is the type we needed then check if its building block then check for is there any tile 
    private bool CheckCondition(RuleTileWithData tile, Item equippedItem)
    {
        if (equippedItem.type == ItemType.BuildingBlock)
        {
            if (!tile)
            {
                return true;
            }
        }
        else if (equippedItem.type == ItemType.Tool)
        {
            if (tile)
            {
                if (tile.item.actionType == equippedItem.actionType)
                {
                    return true;
                }
            }
        }


        return false;


    }


    private void Build(Vector3Int position, Item itemToBuild)
    {
        if (inventory.GetItemFromDic(item))
        {
            int amountToRemove = 1;
            inventory.RemoveItemFromDic(item, amountToRemove);

            tempTilemap.SetTile(position, null);
            highlighted = false;

            mainTilemap.SetTile(position, itemToBuild.tile);
        }

        
    }


    private void Destroy(Vector3Int position)
    {
        tempTilemap.SetTile(position, null);
        highlighted = false;

        RuleTileWithData tile = mainTilemap.GetTile<RuleTileWithData>(position);
        mainTilemap.SetTile(position, null);

        Vector3 pos = mainTilemap.GetCellCenterWorld(position);
        GameObject loot = Instantiate(lootPrefab, pos, Quaternion.identity);
        loot.GetComponent<Loot>().Initialize(tile.item, inventory);

    }

}
