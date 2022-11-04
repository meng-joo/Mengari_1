using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolParentObj : PoolableMono
{
    public Action ResetEvent = null; // 자식 오브젝트 이벤트 
    public override void Reset()
    {
        ResetEvent?.Invoke(); 
    }
}
