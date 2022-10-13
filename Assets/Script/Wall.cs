using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : PoolableMono
{
    public GameObject _brokenWall, _originWall;

    public void Start()
    {
        _originWall = transform.GetChild(0).gameObject;
        _brokenWall = transform.GetChild(1).gameObject;

        
    }

    public void OnTriggerEnter(Collider other)
    {
        //if(other.CompareTag("Bullet"))
        //{
        //    HitBullet();
        //    other.GetComponent<Bullet>().DestroyBullet(); 
        //}
        //else if(other.CompareTag("Laser"))
        //{
        //    //HitBullet();
        //}
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
        _originWall.SetActive(true);
        _brokenWall.SetActive(false);
    }
}
