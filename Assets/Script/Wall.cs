using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    public GameObject _brokenWall, _originWall;

    public void Start()
    {
        _originWall = transform.GetChild(0).gameObject;
        _brokenWall = transform.GetChild(1).gameObject;
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Bullet"))
        {
            HitBullet();
        }
    }

    public void HitBullet()
    {
        _originWall.SetActive(false);
        _brokenWall.SetActive(true);
    }
}
