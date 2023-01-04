using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall_RandomShape : MonoBehaviour
{
    public ShapeDataSO _shapeDataSO;
    public List<Material> shapeData = new List<Material>();
    public List<Sprite> spriteData = new List<Sprite>();
    public List<EnumShape> enumData = new List<EnumShape>();
    public List<Sprite> cardSpriteData = new List<Sprite>();
    public List<EnumShape> cardEnumData = new List<EnumShape>();

    private StagePanelUI stagePanelUI;

    private WallManager wallManager;

    private CardPanelManager cardPanelManager;

    private void Awake()
    {
        for (int i = 0; i < _shapeDataSO._allCardShape.Length; i++)
        {
            shapeData.Add(_shapeDataSO._allCardShape[i]._allShape);
            spriteData.Add(_shapeDataSO._allCardShape[i].sprite);
            enumData.Add(_shapeDataSO._allCardShape[i].enumShape);
        }

        //ShapeShake();
    }

    private void Start()
    {
        stagePanelUI = GetComponent<StagePanelUI>();
        wallManager = GetComponent<WallManager>();
        cardPanelManager = GetComponent<CardPanelManager>();
    }

    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.Space))
    //        ShapeShake();
    //}

    public void ShapeShake()
    {
        for (int i = 0; i < 50; i++)
        {
            int randA = Random.Range(0, shapeData.Count);
            int randB = Random.Range(0, shapeData.Count);

            Material _mat = shapeData[randA];
            shapeData[randA] = shapeData[randB];
            shapeData[randB] = _mat;

            Sprite _sprite = spriteData[randA];
            spriteData[randA] = spriteData[randB];
            spriteData[randB] = _sprite;

            EnumShape _enum = enumData[randA];
            enumData[randA] = enumData[randB];
            enumData[randB] = _enum;
        }
        cardSpriteData.Clear();
        cardEnumData.Clear();
        for(int i=0;i< _shapeDataSO._allCardShape.Length; i++)
        {
            cardSpriteData.Add(spriteData[i]);
            cardEnumData.Add(enumData[i]);
        }
        for (int i = 0; i < 50; i++)
        {
            int randA = Random.Range(0, cardPanelManager.appearCardCount);
            int randB = Random.Range(0, cardPanelManager.appearCardCount);

            Sprite _sprite = cardSpriteData[randA];
            cardSpriteData[randA] = cardSpriteData[randB];
            cardSpriteData[randB] = _sprite;

            EnumShape _enum = cardEnumData[randA];
            cardEnumData[randA] = cardEnumData[randB];
            cardEnumData[randB] = _enum;
        }
        //stagePanelUI.Renewal();

        // 여기 

        StartCoroutine(WallCreate());
        //Invoke(wallManager.CreateWall(), 1);
        //ShowShape();
    }

    IEnumerator WallCreate()
    {
        yield return new WaitForSecondsRealtime(1);
        Debug.Log("Wall생성@@");
        cardPanelManager.CreateCard();
        wallManager.CreateWall();

    }

    public void ResetWall()
    {
        StopAllCoroutines(); 

    }

    //void ShowShape()
    //{
    //    for (int i = 0; i < transform.childCount; i++)
    //    {
    //        for (int j = 0; j < transform.GetChild(i).childCount; j++)
    //        {
    //            transform.GetChild(i).GetChild(j).GetComponent<MeshRenderer>().material = shapeData[j];
    //        }
    //    }
    //}
}
