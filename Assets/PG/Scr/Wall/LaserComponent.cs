using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserComponent : MonoBehaviour
{
    private LayerMask _targetLayerMask ;
    [SerializeField]
    private Transform _targetTrans; // 레이캐스트 길이 구할 때 쓸 변수 

    private Ray _ray;
    [SerializeField]
    private float _distance; 
    private void Start()
    {
        _ray = new Ray(transform.position, transform.forward);
        _distance = Vector3.Distance(transform.position, _targetTrans.position);
        _targetLayerMask = 1 << LayerMask.NameToLayer("Wall");
    }
    private void Update()
    {
        if(Physics.Raycast(_ray, out RaycastHit hitInfo,10, _targetLayerMask) == true)
        {
            Debug.Log("AA");
            Debug.DrawRay(_ray.origin, _ray.direction * _distance, Color.red, 1111f);
            Wall target = hitInfo.transform.GetComponent<Wall>();
          //  target.HitBullet(); 
            // 게임 종료 
        }
    }

    
}
