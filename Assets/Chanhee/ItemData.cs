using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New ItemData", menuName = "SO/Items/ItemData", order = 1)]
public class ItemData : ScriptableObject
{
    public  string  itemName;
    public  string  itemInform;
    public  int     itemCost;
    public  Sprite  itemSprite;

    public  bool    itemBuy;
    public  bool    itemUnlock;
}
