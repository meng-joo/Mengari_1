using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WallManager : MonoBehaviour
{
    public static int stageLevel = 1;
    public UnityEvent levelUpEvent;

    [SerializeField]
    private Transform _createdTrans;
    
    // 벽 생성 , 벽 삭제, 벽 체크 레이캐스트 

    /// <summary>
    ///  벽 생성 
    /// </summary>
    [ContextMenu("벽 생성")]
    public void CreateWall()
    {
        PoolType poolType = SelectRandomWall();
        Wall wall = PoolManager.Instance.Pop(poolType) as Wall;
        wall.SetPosAndRot(_createdTrans.position, Vector3.zero);
    }

    /// <summary>
    ///  벽 랜덤 선택 
    /// </summary>
    private PoolType SelectRandomWall()
    {
        int randomIdx = Random.Range(0, 3);
        PoolType poolType = PoolType.None; 
        switch (randomIdx)
        {
            case 0:
                poolType = PoolType.Wall_1;
                break;
            case 1:
                poolType = PoolType.Wall_2;
                break;
            case 2:
                poolType = PoolType.Wall_3;
                break;
        }
        return poolType; 
    }
}
