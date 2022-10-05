using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PoolPair
{
    public PoolType poolType;
    public PoolableMono prefab;
    public int count; 
}


[CreateAssetMenu(menuName = "SO/PoolingLIstSO")]
public class PoolingListSO : ScriptableObject
{
    public List<PoolPair> poolingList = new List<PoolPair>(); 
}
