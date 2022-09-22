using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class SelectRandomShape : MonoBehaviour
{
    public List<int> CurrentShapeList = new List<int>(); //���� �����մ� ����
    
    public List<int> RandomShapeList = new List<int>(); //����Ʈ �ȿ� �ִ� ����

    public List<int> TempShapeList = new List<int>(); //������ ���� �� �Ҽ������� ���� �Ⱦ��鼭?


    public int shapeCnt = 4; //ȭ�鿡 �ߴ� �������� ����
    void Start()
    {
        int num1 = 1;
        int num2 = 2;
        int num3 = 3;
        int num4 = 4;
        int num5 = 5;
        int num6 = 6;
        int num7 = 7;
        int num8 = 8;
        int num9 = 9;
        int num10 = 10;



        RandomShapeList.Add(num1);
        RandomShapeList.Add(num2);
        RandomShapeList.Add(num3);
        RandomShapeList.Add(num4);
        RandomShapeList.Add(num5);
        RandomShapeList.Add(num6);
        RandomShapeList.Add(num7);
        RandomShapeList.Add(num8);
        RandomShapeList.Add(num9);
        RandomShapeList.Add(num10);

        ShuffleList(RandomShapeList);
        
        for(int i = 0; i < shapeCnt; i++)
        {
            CurrentShapeList.Add(RandomShapeList[0]); //�������� Ŀ��Ʈ�� shapecnt��ŭ �ű�
            RandomShapeList.RemoveAt(0);
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
        shapeCnt++;
        RandomShapeList.Add(0);

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
