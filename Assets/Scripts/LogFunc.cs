using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogFunc : MonoBehaviour
{
    [SerializeField]
    private float _quotient;

    void Start()
    {
        for(int i=0;i<72;i++)
        {
            Debug.Log(Mathf.Pow(_quotient, 1 - i * 0.015f) + " " + (i%6+1));
        }
    }
}
