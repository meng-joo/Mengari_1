using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Text_Bullet : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            transform.position = new Vector3(0, 10, 0);
    }
}
