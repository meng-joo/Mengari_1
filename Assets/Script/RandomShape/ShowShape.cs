using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowShape : MonoBehaviour
{
    private Wall_RandomShape _randomShape;
    public Wall_RandomShape RandomShape
    {
        get
        {
            _randomShape ??= FindObjectOfType<Wall_RandomShape>();
            return _randomShape;
        }
    }

    void Awake()
    {
        _randomShape ??= FindObjectOfType<Wall_RandomShape>();
    }
    //¾Ã¤© ¿Ö¾ÈµÅ
    public void ShowShapeMaterial()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            for (int j = 0; j < transform.GetChild(i).childCount; j++)
            {
                transform.GetChild(i).GetChild(j).GetComponent<MeshRenderer>().material = RandomShape.shapeData[j];
            }
        }
    }
}
