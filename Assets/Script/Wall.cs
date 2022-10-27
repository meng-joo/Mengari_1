using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : PoolableMono
{
    private Vector3 _originPos; 
    private Followers _followers; 
    private GameObject _brokenWall, _originWall;


    public GameObject BrokenWall => _brokenWall ??= transform.GetChild(0).GetChild(0).gameObject;
    public GameObject OriginWall => _originWall ??= transform.GetChild(0).GetChild(1).gameObject;
    public void Awake()
    {
        _followers = GetComponentInChildren<Followers>(); 
        SetWall(); 
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            Debug.Log("�浹");
            HitBullet();
            other.GetComponent<Bullet>().DestroyBullet();
            // ���� 
        }
        else if (other.CompareTag("Laser"))
        {
            Debug.Log("�浹");
            HitBullet();
        }
    }

    private void SetWall()
    {
        _originWall ??= transform.GetChild(0).GetChild(0).gameObject;
        _brokenWall ??= transform.GetChild(0).GetChild(1).gameObject;
    }

    public void HitBullet()
    {
        _followers.enabled = false; 
        _originWall.SetActive(false);
        _brokenWall.SetActive(true);
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
        // ������ ���� 
        transform.position = _originPos;
        _followers.enabled = true;  
        OriginWall.SetActive(true);
        BrokenWall.SetActive(false);
    }
}
