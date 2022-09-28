using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazerCtrl : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Wall"))
        {
            Debug.Log("collision");
            Destroy(collision.gameObject);
        }
    }
}
