using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New ItemData", menuName = "SO/Items/ItemData", order = 1)]
public class ItemData : ScriptableObject
{
    public  string  itemName;
    // public  string  itemInform;
    public  int     itemCost;
    public  Sprite  itemSprite;

    public  bool    itemUse;        // 사용 시 true
    public  bool    itemBuy;        // 구매하면 true, 구매 하지 않았으면 false 
    public  bool    itemUnlock;     // 잠기면 true, 열리면 false
}
