using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class SelectRandomShape : MonoBehaviour
{
    [SerializeField]
    private List<Shape> currentShapeList = new List<Shape>(); //���� �����մ� ����

    public List<Shape> CurrentShapeList
    {
        get
        {
            return currentShapeList;
        }
    }
    
    [SerializeField]
    private List<Shape> randomShapeList = new List<Shape>(); //����Ʈ �ȿ� �ִ� ����
    [SerializeField]
    private List<Shape> wholeShapeList = new List<Shape>(); //��ü�� ����Ʈ


    public int shapeCnt = 4; //ȭ�鿡 �ߴ� �������� ����
    void Start()
    {
        for (int i = 0; i < System.Enum.GetValues(typeof(EnumShape)).Length; i++)
        {
            Shape shape = new Shape();
            shape.enumShape = (EnumShape)i;
            string shapeString = System.Enum.GetName(typeof(EnumShape), i);
            shape.sprite = Resources.Load<Sprite>($"Shapes/{shapeString}");
            wholeShapeList.Add(shape);
        }
        //��ü ����Ʈ�� ��� �������� �־��

        for(int i = 0; i < 10; i++)
        {
            randomShapeList.Add(wholeShapeList[i]);
        }
        //ó�� �ʿ��� ������ŭ ���� ����Ʈ�� �־��

        ShuffleList(randomShapeList);
        //����
        
        for(int i = 0; i < shapeCnt; i++)
        {
            currentShapeList.Add(randomShapeList[0]); //��������Ʈ���� Ŀ��Ʈ����Ʈ�� shapecnt��ŭ �ű�
            randomShapeList.RemoveAt(0); //ȭ���� ���̴� �������� ����
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

        //if(������)
        ChangeShape();
    }

    void ChangeShape()
    {
        for(int i = 0; i < shapeCnt; i++)
        {
            randomShapeList.Add(currentShapeList[i]); // Ŀ��Ʈ�� ���
        }
        currentShapeList.RemoveRange(0, shapeCnt);

        //���̵� �����ڵ� shapecnt�� �ٲ�ٴ���, shapelist�� ���� �����Ѵٴ���
        //shapeCnt++;
        //RandomShapeList.Add();

        ShuffleList(randomShapeList); //����

        for (int i = 0; i < shapeCnt; i++)
        {
            currentShapeList.Add(randomShapeList[i]);
        }
        randomShapeList.RemoveRange(0, shapeCnt);
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
