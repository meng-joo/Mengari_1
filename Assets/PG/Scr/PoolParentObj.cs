using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolParentObj : PoolableMono
{
    public Action ResetEvent = null; // �ڽ� ������Ʈ �̺�Ʈ 
    public override void Reset()
    {
        ResetEvent?.Invoke(); 
    }
}
