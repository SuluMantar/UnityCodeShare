using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Tiles/Custom Rule Tile")]

public class RuleTileWithData : RuleTile
{
    //This field necessary because this item represent that which item rule tile should drop
    public Item item;
}
