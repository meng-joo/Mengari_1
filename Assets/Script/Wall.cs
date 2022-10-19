using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : PoolableMono
{
    private GameObject _brokenWall, _originWall;

    public GameObject BrokenWall => _brokenWall ??= transform.GetChild(0).GetChild(0).gameObject;
    public GameObject OriginWall => _originWall ??= transform.GetChild(0).GetChild(1).gameObject;
    public void Awake()
    {
        SetWall(); 
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            HitBullet();
            other.GetComponent<Bullet>().DestroyBullet();
        }
        else if (other.CompareTag("Laser"))
        {
            //HitBullet();
        }
    }

    private void SetWall()
    {
        _originWall ??= transform.GetChild(0).GetChild(0).gameObject;
        _brokenWall ??= transform.GetChild(0).GetChild(1).gameObject;
    }

    public void HitBullet()
    {
        _originWall.SetActive(false);
        _brokenWall.SetActive(true);
    }

    public void Reseting()
    {
        _originWall.SetActive(true);
        _brokenWall.SetActive(false);
    }

    public override void Reset()
    {
        OriginWall.SetActive(true);
        BrokenWall.SetActive(false);
    }
}
