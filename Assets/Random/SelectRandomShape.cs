using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class SelectRandomShape : MonoBehaviour
{
    public List<int> CurrentShapeList = new List<int>(); //현재 나와잇는 모양들
    
    public List<int> RandomShapeList = new List<int>(); //리스트 안에 있는 모양들

    public List<int> TempShapeList = new List<int>(); //보관함 좀더 잘 할수잇을듯 굳이 안쓰면서?


    public int shapeCnt = 4; //화면에 뜨는 쉐이프에 개수
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
            CurrentShapeList.Add(RandomShapeList[0]); //랜덤에서 커렉트로 shapecnt만큼 옮김
            RandomShapeList.RemoveAt(0);
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

        Debug.Log("answer1 의 값은 : " + answer1);
        Debug.Log("answer2 의 값은 : " + answer2);

        //if(맞으면)
        ChangeShape();
    }

    void ChangeShape()
    {
        for(int i = 0; i < shapeCnt; i++)
        {
            RandomShapeList.Add(CurrentShapeList[i]); // 커렉트를 비움
        }
        CurrentShapeList.RemoveRange(0, shapeCnt);

        //난이도 조절코드 shapecnt가 바뀐다던가, shapelist의 수가 증가한다던가
        shapeCnt++;
        RandomShapeList.Add(0);

        ShuffleList(RandomShapeList); //셔플

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
