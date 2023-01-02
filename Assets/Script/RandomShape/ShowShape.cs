using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowShape : MonoBehaviour
{
    private Wall_RandomShape _randomShape;

    private GameObject[] shapes = new GameObject[6];
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

        for (int i = 0; i < transform.childCount; i++)
        {
            shapes[i] = transform.GetChild(i).gameObject;
        }
    }
    //¾Ã¤© ¿Ö¾ÈµÅ
    public void ShowShapeMaterial()
    {
        SetStage(); 

        for (int i = 0; i < transform.childCount; i++)
        {
            for (int j = 0; j < transform.GetChild(i).childCount; j++)
            {
                transform.GetChild(i).GetChild(j).GetComponent<MeshRenderer>().material = RandomShape.shapeData[j];
            }
        }
    }


    private void Start()
    {
        
    }


    private void SetStage()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            shapes[i].SetActive(false);
        }

        shapes[WallManager.stageLevel / 6].SetActive(true);
    }
}
