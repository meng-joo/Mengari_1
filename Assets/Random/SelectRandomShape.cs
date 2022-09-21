using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class SelectRandomShape : MonoBehaviour
{
    public List<Shape> CurrentShapeList = new List<Shape>(); //���� �����մ� ����
    
    public List<Shape> RandomShapeList = new List<Shape>(); //����Ʈ �ȿ� �ִ� ����

    public List<Shape> WholeShapeList = new List<Shape>(); //��ü�� ����Ʈ


    public int shapeCnt = 4; //ȭ�鿡 �ߴ� �������� ����
    void Start()
    {
        for (int i = 0; i < System.Enum.GetValues(typeof(EnumShape)).Length; i++)
        {
            Shape shape = new Shape();
            shape.enumShape = (EnumShape)i;
            string shapeString = System.Enum.GetName(typeof(EnumShape), i);
            shape.sprite = Resources.Load<Sprite>($"Shapes/{shapeString}");
            WholeShapeList.Add(shape);
        }
        //��ü ����Ʈ�� ��� �������� �־��

        for(int i = 0; i < 10; i++)
        {
            RandomShapeList.Add(WholeShapeList[i]);
        }
        //ó�� �ʿ��� ������ŭ ���� ����Ʈ�� �־��

        ShuffleList(RandomShapeList);
        //����
        
        for(int i = 0; i < shapeCnt; i++)
        {
            CurrentShapeList.Add(RandomShapeList[0]); //��������Ʈ���� Ŀ��Ʈ����Ʈ�� shapecnt��ŭ �ű�
            RandomShapeList.RemoveAt(0); //ȭ���� ���̴� �������� ����
        }
        //�⺻���� �ϼ�

    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            SelectShape();
        }
    }

    void SelectShape() //�������� 2���� Ŭ����Ű�� �ٲ���
    {
        int answer1 = Random.Range(0, shapeCnt) + 1;
        int answer2 = Random.Range(0, shapeCnt) + 1;

        Debug.Log("answer1 �� ���� : " + answer1);
        Debug.Log("answer2 �� ���� : " + answer2);

        //if(������)
        ChangeShape();
    }

    void ChangeShape()
    {
        for(int i = 0; i < shapeCnt; i++)
        {
            RandomShapeList.Add(CurrentShapeList[i]); // Ŀ��Ʈ�� ���
        }
        CurrentShapeList.RemoveRange(0, shapeCnt);

        //���̵� �����ڵ� shapecnt�� �ٲ�ٴ���, shapelist�� ���� �����Ѵٴ���
        //shapeCnt++;
        //RandomShapeList.Add();

        ShuffleList(RandomShapeList); //����

        for (int i = 0; i < shapeCnt; i++)
        {
            CurrentShapeList.Add(RandomShapeList[i]);
        }
        RandomShapeList.RemoveRange(0, shapeCnt);
    }

    public void ShuffleList<T>(List<T> list)
    {
        int random1;
        int random2;

        T tmp;

        for (int index = 0; index < list.Count; ++index)
        {
            random1 = UnityEngine.Random.Range(0, list.Count);
            random2 = UnityEngine.Random.Range(0, list.Count);

            tmp = list[random1];
            list[random1] = list[random2];
            list[random2] = tmp;
        }
    }

}
