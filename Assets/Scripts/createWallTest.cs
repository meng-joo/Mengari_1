using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class createWallTest : MonoBehaviour
{
    public GameObject path;
    public GameObject wall;

    private float createTime;

    private bool isCanCreate;

    private void Start()
    {
        Instantiate(path);
        isCanCreate = true;
        createTime = 2f;
    }

    private void Update()
    {

        if(isCanCreate)
        {
            StartCoroutine(SpawnWall());
            isCanCreate = false;
            Instantiate(wall);
        }
    }


    IEnumerator SpawnWall()
    {
        yield return new WaitForSeconds(createTime);
        isCanCreate = true;

    }

}
