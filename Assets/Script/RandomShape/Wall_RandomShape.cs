using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall_RandomShape : MonoBehaviour
{
    public ShapeDataSO _shapeDataSO;
    private List<Material> shapeData = new List<Material>();
    private MeshRenderer[] _meshRenderer;
    //private Dictionary<GameObject, Material> wall_Shape = new Dictionary<GameObject, Material>();
    public MeshRenderer[] wall_Shape = new MeshRenderer[20];

    private void Awake()
    {
        for (int i = 0; i < _shapeDataSO._allShape.Length; i++)
        {
            shapeData.Add(_shapeDataSO._allShape[i]);
        }

        ShapeShake();
    }

    void ShapeShake()
    {
        for (int i = 0; i < 50; i++)
        {
            int randA = Random.Range(0, shapeData.Count);
            int randB = Random.Range(0, shapeData.Count);

            Material _mat = shapeData[randA];
            shapeData[randA] = shapeData[randB];
            shapeData[randB] = _mat;
        }
    }

    void ShowShape()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            for (int j = 0; j < transform.GetChild(i).childCount; j++)
            {
                //wall_Shape[] = 
                //wall_Shape.Add(transform.GetChild(i).gameObject, transform.GetChild(i).GetChild(j).GetComponent<MeshRenderer>().material);
            }
        }
    }


}
