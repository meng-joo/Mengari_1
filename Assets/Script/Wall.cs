using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : PoolableMono
{
    private Vector3 _originPos; 
    private Followers _followers; 
    [SerializeField]
    private GameObject _brokenWall, _originWall;

    private bool _isCollision = false; 
    private ShowShape _showShape;
    //private GameObject _randomShape;

    public GameObject BrokenWall => _brokenWall ??= transform.GetChild(0).GetChild(0).gameObject;
    public GameObject OriginWall => _originWall ??= transform.GetChild(0).GetChild(1).gameObject;

    public Action brokenEvent = null;

    private void Awake()
    {
        _followers = GetComponentInChildren<Followers>(); 
        SetWall(); 
    }

    private void Start()
    {
        _showShape = gameObject.GetComponentInChildren<ShowShape>();
     //   _randomShape = transform.GetChild(3).GetComponent<GameObject>();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (_isCollision == false) return; 
        if (other.CompareTag("Bullet"))
        {
            Debug.Log("충돌");
            _isCollision = false; 
            HitBullet();
            other.GetComponent<Bullet>().DestroyBullet();
            // 진동 
        }
        else if (other.CompareTag("Laser"))
        {
            Debug.Log("충돌");
            _isCollision = false; 
            HitBullet();
        }
    }

    private void SetWall()
    {
        _originWall ??= transform.GetChild(0).GetChild(0).gameObject; 
        _brokenWall ??= transform.GetChild(0).GetChild(1).gameObject;
    }

    public void SetShape()
    {
        _showShape.ShowShapeMaterial(); 
    }
    public void HitBullet()
    {
        _followers.enabled = false; 
        _originWall.SetActive(false);
    //    _randomShape.SetActive(false);
        _brokenWall.SetActive(true);
        _showShape.randomShape.ShapeShake();

    }

    public void Reseting()
    {
        _originWall.SetActive(true);
        _brokenWall.SetActive(false);
    }

    public override void SetPosAndRot(Vector3 pos, Vector3 rot)
    {
        _originPos = pos; 
        base.SetPosAndRot(pos, rot);
    }
    public override void Reset()
    {
        // 포지션 지정 
        transform.position = _originPos;
        _followers.enabled = true;
        OriginWall.SetActive(true);
     //   _randomShape.SetActive(true);
        BrokenWall.SetActive(false);

        _isCollision = true;

    }
}
