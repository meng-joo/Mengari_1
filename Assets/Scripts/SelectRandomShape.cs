using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class SelectRandomShape : MonoBehaviour
{
    [SerializeField]
    private List<Shape> currentShapeList = new List<Shape>(); //현재 나와잇는 모양들

    public List<Shape> CurrentShapeList
    {
        get
        {
            return currentShapeList;
        }
    }
    
    [SerializeField]
    private List<Shape> randomShapeList = new List<Shape>(); //리스트 안에 있는 모양들
    [SerializeField]
    private List<Shape> wholeShapeList = new List<Shape>(); //전체의 리스트


    public int shapeCnt = 4; //화면에 뜨는 쉐이프에 개수
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
        //전체 리스트에 모든 쉐이프를 넣어둠

        for(int i = 0; i < 10; i++)
        {
            randomShapeList.Add(wholeShapeList[i]);
        }
        //처음 필요한 개수만큼 랜덤 리스트에 넣어둠

        ShuffleList(randomShapeList);
        //셔플
        
        for(int i = 0; i < shapeCnt; i++)
        {
            currentShapeList.Add(randomShapeList[0]); //랜덤리스트에서 커렉트리스트로 shapecnt만큼 옮김
            randomShapeList.RemoveAt(0); //화면의 보이는 쉐이프의 개수
        }
        //기본으로 완성

    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            SelectShape();
        }
    }

    void SelectShape() //랜덤으로 2개를 클릭시키고 바꿔줌
    {
        int answer1 = Random.Range(0, shapeCnt) + 1;
        int answer2 = Random.Range(0, shapeCnt) + 1;

        //if(맞으면)
        ChangeShape();
    }

    void ChangeShape()
    {
        for(int i = 0; i < shapeCnt; i++)
        {
            randomShapeList.Add(currentShapeList[i]); // 커렉트를 비움
        }
        currentShapeList.RemoveRange(0, shapeCnt);

        //난이도 조절코드 shapecnt가 바뀐다던가, shapelist의 수가 증가한다던가
        //shapeCnt++;
        //RandomShapeList.Add();

        ShuffleList(randomShapeList); //셔플

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
