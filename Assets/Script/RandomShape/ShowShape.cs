using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowShape : MonoBehaviour
{
    public Wall_RandomShape randomShape;

    void Start()
    {
        randomShape = FindObjectOfType<Wall_RandomShape>();
    }

    public void ShowShapeMaterial()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            for (int j = 0; j < transform.GetChild(i).childCount; j++)
            {
                transform.GetChild(i).GetChild(j).GetComponent<MeshRenderer>().material = randomShape.shapeData[j];
            }
        }
    }
}
