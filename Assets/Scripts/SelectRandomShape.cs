using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectRandomShape : MonoBehaviour
{
    //UIManager uiManager;

    //[SerializeField]
    //private List<Shape> currentShapeList = new List<Shape>(); //현재 나와잇는 모양들

    //public List<Shape> CurrentShapeList
    //{
    //    get
    //    {
    //        return currentShapeList;
    //    }
    //}

    //[SerializeField]
    //private List<Shape> wholeShapeList = new List<Shape>(); //전체의 리스트 16개 

    //private Shape GoldShapeList; // 보스전 모양

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

    //    전체 리스트에 모든 쉐이프를 넣어둠

    //    처음 필요한 개수만큼 랜덤 리스트에 넣어둠

    //    ShuffleList(wholeShapeList);
    //    셔플

    //    start말고 함수로 따로 옮겨
    //    Debug.Log(uiManager.CardCount);
    //    for (int i = 0; i < uiManager.CardCount; i++)
    //    {
    //        currentShapeList.Add(wholeShapeList[0]); //랜덤리스트에서 커렉트리스트로 shapecnt만큼 옮김
    //        wholeShapeList.RemoveAt(0); //화면의 보이는 쉐이프의 개수
    //    }
    //    기본으로 완성
    //}

    //public void ClearList()
    //{
    //    Debug.Log(uiManager.CardCount);
    //    for (int i = 0; i < currentShapeList.Count; i++)
    //    {
    //        wholeShapeList.Add(currentShapeList[i]); // 커렉트를 비움
    //    }
    //    currentShapeList.RemoveRange(0, currentShapeList.Count);
    //}

    //public void IncreaseDifficult()
    //{
    //    처음 난이도는 생성개수 2개 리스트개수 4개
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
