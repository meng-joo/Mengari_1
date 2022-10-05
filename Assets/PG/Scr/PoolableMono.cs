using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PoolableMono : MonoBehaviour
{
    public PoolType poolType; 
    public abstract void Reset();

}
