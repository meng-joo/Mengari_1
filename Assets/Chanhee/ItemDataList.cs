using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="ItemDataList", menuName ="SO/Items/ItemDataList", order = 0)]
public class ItemDataList : ScriptableObject
{
    public List<ItemData> itemDataLists;

    public ItemData usingItemData;
}
