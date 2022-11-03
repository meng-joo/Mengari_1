using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ShopInfo
{
    public int skinId;
    public string skinName;
    public int skinCost;
    public Sprite sprite;
}

[CreateAssetMenu(fileName ="New Item", menuName ="ScriptableObject/Items")]
public class ItemInfo
{
    public int skinId;
    public string skinName;
    public int skinCost;
    public Sprite sprite;
}