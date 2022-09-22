using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class createWallTest : MonoBehaviour
{
    public GameObject path;
    public GameObject wall;
    private void Start()
    {
        Instantiate(path);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(wall);
        }
    }
}
