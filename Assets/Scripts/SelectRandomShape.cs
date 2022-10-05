using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectRandomShape : MonoBehaviour
{
    //UIManager uiManager;

    //[SerializeField]
    //private List<Shape> currentShapeList = new List<Shape>(); //���� �����մ� ������

    //public List<Shape> CurrentShapeList
    //{
    //    get
    //    {
    //        return currentShapeList;
    //    }
    //}

    //[SerializeField]
    //private List<Shape> wholeShapeList = new List<Shape>(); //��ü�� ����Ʈ 16��

    //private Shape GoldShapeList; // ������ ����

    //void Start()
    //{
    //    if (uiManager == null)
    //        uiManager = GetComponent<UIManager>();

    //    for (int i = 0; i < System.Enum.GetValues(typeof(EnumShape)).Length; i++)
    //    {
    //        Shape shape = new Shape();
    //        shape.enumShape = (EnumShape)i;
    //        string shapeString = System.Enum.GetName(typeof(EnumShape), i);
    //        shape.sprite = Resources.Load<Sprite>($"Shapes/{shapeString}");
    //        wholeShapeList.Add(shape);
    //    }

    //}

    //void Update()
    //{

    //}

    //public void GameStart()
    //{

    //    ��ü ����Ʈ�� ���� �������� �־���

    //    ó�� �ʿ��� ������ŭ ���� ����Ʈ�� �־���

    //    ShuffleList(wholeShapeList);
    //    ����

    //    start���� �Լ��� ���� �Ű�
    //    Debug.Log(uiManager.CardCount);
    //    for (int i = 0; i < uiManager.CardCount; i++)
    //    {
    //        currentShapeList.Add(wholeShapeList[0]); //��������Ʈ���� Ŀ��Ʈ����Ʈ�� shapecnt��ŭ �ű�
    //        wholeShapeList.RemoveAt(0); //ȭ���� ���̴� �������� ����
    //    }
    //    �⺻���� �ϼ�
    //}

    //public void ClearList()
    //{
    //    Debug.Log(uiManager.CardCount);
    //    for (int i = 0; i < currentShapeList.Count; i++)
    //    {
    //        wholeShapeList.Add(currentShapeList[i]); // Ŀ��Ʈ�� ����
    //    }
    //    currentShapeList.RemoveRange(0, currentShapeList.Count);
    //}

    //public void IncreaseDifficult()
    //{
    //    ó�� ���̵��� �������� 2�� ����Ʈ���� 4��
    //}

    //public void ShuffleList<T>(List<T> list)
    //{
    //    int random1;
    //    int random2;

    //    T tmp;

    //    for (int index = 0; index < list.Count; ++index)
    //    {
    //        random1 = UnityEngine.Random.Range(0, list.Count);
    //        random2 = UnityEngine.Random.Range(0, list.Count);

    //        tmp = list[random1];
    //        list[random1] = list[random2];
    //        list[random2] = tmp;
    //    }
    //}

}
