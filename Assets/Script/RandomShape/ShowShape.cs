using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowShape : MonoBehaviour
{
    private Wall_RandomShape randomShape;
    // Start is called before the first frame update
    void Start()
    {
        randomShape = GameObject.Find("Manager").GetComponent<Wall_RandomShape>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Invoke("ShowShapeMaterial", 1f);
        }
    }

    void ShowShapeMaterial()
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
