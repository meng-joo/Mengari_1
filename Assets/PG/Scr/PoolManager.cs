using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoSingleton<PoolManager>
{
    [SerializeField]
    private PoolingListSO _poolingListSO; 

    public Dictionary<PoolType, Queue<PoolableMono>> poolDict = new Dictionary<PoolType, Queue<PoolableMono>>();
    public List<PoolableMono> poolList = new List<PoolableMono>();

    private void Awake()
    {
        foreach(var pool in _poolingListSO.poolingList)
        {
            CreatePool(pool.prefab, pool.count); 
        }
    }

    public void CreatePool(PoolableMono prefab, int count = 10)
    {
        if (poolDict.ContainsKey(prefab.poolType) == false)
        {
            poolDict.Add(prefab.poolType, new Queue<PoolableMono>());
            poolList.Add(prefab);
        }

        Debug.Log(prefab.name); 
        for (int i =0; i<count; i++)
        {
            CreateItem(prefab);
            Push(prefab);
            //poolDict[prefab.poolType].Enqueue(prefab); 
        }
     }

    private void CreateItem(PoolableMono item)
    {
        GameObject obj = Instantiate(item,transform).gameObject;
        obj.SetActive(false); 
    }

    public PoolableMono Pop(PoolType poolType)
    {
        if (!poolDict.ContainsKey(poolType))
        {
            Debug.LogError("Prefab doesnt exist on pool");
            return null;
        }

        PoolableMono item = null;
        if (poolDict[poolType].Count <= 0)
        {
            Debug.Log(poolType ); 
            foreach(var pool in poolList)
            {
                if (pool.poolType == poolType)
                    item = pool; 
            }
            CreateItem(item);
        }

        item = poolDict[poolType].Dequeue();
        item.Reset();
        return item;
    }

    public void Push(PoolableMono obj)
    {
        poolDict[obj.poolType].Enqueue(obj);
        obj.gameObject.SetActive(false);
    }

}

public enum PoolType
{
    Bullet,
    BulletCreateEffect
}
