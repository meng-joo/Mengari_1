using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;
using System;
using UnityEngine.Events;
using Cinemachine;

public class CardPanelManager : MonoBehaviour
{
    public int appearCardCount = 2;

    public int maxinumCardNumber = 1;

    public Frame card;

    public List<Frame> cards = new List<Frame>();

    public List<Frame> cardButtons = new List<Frame>();

    public List<Button> clickedCards = new List<Button>();

    public List<Shape> shapeList = new List<Shape>();

    public GridLayoutGroup cardGridLayoutGroup;

    public RectTransform cardPanel;

    public RectTransform finalCardLocation;

    public Image wrongEffect;

    private Sequence _seq;

    private Wall_RandomShape randomShape;

    public CinemachineVirtualCamera cinemachineVirtualCamera;

    private void Awake()
    {
        randomShape = GetComponent<Wall_RandomShape>();
    }

    private void Start()
    {
        int count = randomShape.spriteData.Count;

        for (int i = 0; i < count; i++)
        {
            Frame newCard = Instantiate(card, cardPanel);

            cardButtons.Add(newCard);

            newCard.GetComponent<Button>().enabled = false;
            newCard.GetComponent<Image>().enabled = false;

            newCard.shape.enumShape = randomShape.enumData[i];
            newCard.shape.sprite = randomShape.spriteData[i];
            newCard.button.image.sprite = newCard.shape.sprite;

            //newCard.button.onClick.AddListener(ClickCard);

            EventTrigger eventTrigger = newCard.gameObject.AddComponent<EventTrigger>();

            EventTrigger.Entry entry_PointerDown = new EventTrigger.Entry();
            entry_PointerDown.eventID = EventTriggerType.PointerDown;
            entry_PointerDown.callback.AddListener((data) => { OnPointerDown((PointerEventData)data); });
            eventTrigger.triggers.Add(entry_PointerDown);

            EventTrigger.Entry entry_PointerUp = new EventTrigger.Entry();
            entry_PointerUp.eventID = EventTriggerType.PointerUp;
            entry_PointerUp.callback.AddListener((data) => { OnPointerUp((PointerEventData)data); });
            eventTrigger.triggers.Add(entry_PointerUp);
        }
        AddCard();
    }

    IEnumerator ShakeCamera(float intensity, float time)
    {
        CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin =
            cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = intensity;
        yield return new WaitForSeconds(time);
        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 0;
    }

    //public void ShuffleList(List<Sprite> list1, List<EnumShape> list2)
    //{
    //    int random1;
    //    int random2;

    //    for (int index = 0; index < 1; ++index)
    //    {
    //        random1 = UnityEngine.Random.Range(0, appearCardCount);
    //        random2 = UnityEngine.Random.Range(0, appearCardCount);
    //        Debug.Log(list1[random1]);
    //        Debug.Log(list1[random2]);
    //        var tmp1 = list1[random1];
    //        list1[random1] = list1[random2];
    //        list1[random2] = tmp1;

    //        var tmp2 = list2[random1];
    //        list2[random1] = list2[random2];
    //        list2[random2] = tmp2;
    //    }
    //}

    void CardReset()
    {
        for (int i = 0; i < appearCardCount; i++)
        {
            cards[i].shape.enumShape = randomShape.cardEnumData[i];
            cards[i].shape.sprite = randomShape.cardSpriteData[i];
            cards[i].button.image.sprite = randomShape.cardSpriteData[i];
        }
    }

    void Update()
    {
        LevelUp();
        CardReSize();
    }

    private void LevelUp()
    {
        int frontStageNumber = WallManager.stageLevel / 6 + 1;
        int backStageNumber = WallManager.stageLevel % 6 + 1;

        if(frontStageNumber==2)
        {
            appearCardCount = 4;
            maxinumCardNumber = 2;
        }
        else if(frontStageNumber == 3)
        {
            appearCardCount = 6;
            maxinumCardNumber = 3;
        }
        else if (frontStageNumber == 4)
        {
            appearCardCount = 8;
            maxinumCardNumber = 4;
        }
        else if (frontStageNumber == 5)
        {
            maxinumCardNumber = 5;
        }
        else if (frontStageNumber == 6)
        {
            maxinumCardNumber = 6;
        }
        AddCard();
    }

    public void AddCard()
    {
        cards.Clear();
        for (int i = 0; i < appearCardCount; i++)
            cards.Add(cardButtons[i]);
    }

    public void CardReSize()
    {
        if (appearCardCount == 2)
        {
            maxinumCardNumber = 1;
            cardPanel.GetComponent<GridLayoutGroup>().cellSize = new Vector2(600, 700);
            cardPanel.GetComponent<GridLayoutGroup>().spacing = new Vector2(100, 50);
        }
        else if (appearCardCount == 4)
        {
            maxinumCardNumber = 2;
            cardPanel.GetComponent<GridLayoutGroup>().cellSize = new Vector2(350, 400);
            cardPanel.GetComponent<GridLayoutGroup>().spacing = new Vector2(200, 50);
        }
        else if (appearCardCount == 6)
        {
            maxinumCardNumber = 4;
            cardPanel.GetComponent<GridLayoutGroup>().cellSize = new Vector2(300, 400);
            cardPanel.GetComponent<GridLayoutGroup>().spacing = new Vector2(150, 50);
        }
        else if (appearCardCount == 8)
        {
            maxinumCardNumber = 5;
            cardPanel.GetComponent<GridLayoutGroup>().cellSize = new Vector2(250, 350);
            cardPanel.GetComponent<GridLayoutGroup>().spacing = new Vector2(100, 100);
        }
    }

    [ContextMenu("카드 만들기")]
    public void CreateCard()
    {
        //ShuffleList(randomShape.spriteData, randomShape.enumData);
        CardReset();
        CardReSize();

        _seq = DOTween.Sequence();
        for (int i = 0; i < cards.Count; i++)
        {
            cardButtons[i].GetComponent<RectTransform>().localScale = new Vector3(1.5f, 1.5f, 0);
            cardButtons[i].GetComponent<Image>().enabled = true;
            cardButtons[i].GetComponent<Button>().enabled = true;
            _seq.Join(cardButtons[i].transform.DOScale(1f, 0.3f));
        }
        _seq.AppendCallback(() => {
            _seq.Kill();
            cardPanel.GetComponent<GridLayoutGroup>().enabled = true;
        });
        clickedCards.Clear();
    }

    public void OnPointerDown(PointerEventData data)
    {
        if (clickedCards.Count >= maxinumCardNumber)
            return;
        Debug.Log("PointerDown");

        _seq = DOTween.Sequence();

        var clickObject = EventSystem.current.currentSelectedGameObject;

        int number = randomShape.enumData.IndexOf(clickObject.GetComponent<Frame>().shape.enumShape);
        Debug.Log(number + " " + maxinumCardNumber);
        if (number < maxinumCardNumber)
        {
            clickObject.transform.DOScale(0.75f, 0.2f).SetEase(Ease.OutBack);
            clickedCards.Add(clickObject.GetComponent<Button>());
            clickObject.GetComponent<Image>().DOColor(Color.gray, 0.1f);
        }
        else
        {
            //StartCoroutine(ShakeCamera(5f, 1f));
            Handheld.Vibrate();
            wrongEffect.gameObject.SetActive(true);
            for (int i=0;i<clickedCards.Count;i++)
            {
                clickedCards[i].GetComponent<Image>().DOColor(Color.black, 0.1f);
            }
            _seq.Append(wrongEffect.DOFade(1.0f, 0.6f));
            _seq.Append(wrongEffect.DOFade(0f, 0.6f));
            _seq.AppendCallback(() => {
                wrongEffect.gameObject.SetActive(false);
            });
            for (int i = 0; i < cards.Count; i++)
            {
                Debug.Log(cards[i]);

                //cards[i].transform.DOScale(0.8f, 0.3f).SetLoops(2,LoopType.Yoyo);
                cards[i].transform.DOShakePosition(0.4f, 30f, 80, 80);
            }

            clickedCards.Clear();
        }
        //if(clickObject.GetComponent<Frame>().shape.enumShape)

        //if (clickObject.GetComponent<Image>().color != Color.gray)
        //{
        //    clickedCards.Add(clickObject.GetComponent<Button>());
        //    clickObject.GetComponent<Image>().DOColor(Color.gray, 0.1f);
        //}
        //else
        //{
        //    clickedCards.Remove(clickObject.GetComponent<Button>());
        //    clickObject.GetComponent<Image>().DOColor(Color.black, 0.1f);
        //}

        
    }



    public void OnPointerUp(PointerEventData data)
    {
        if (clickedCards.Count > maxinumCardNumber)
            return;
        Debug.Log("PointerUp");
        var clickObject = EventSystem.current.currentSelectedGameObject;
        clickObject.transform.DOScale(1f, 0.2f);
        _seq = DOTween.Sequence();
        if (clickedCards.Count >= maxinumCardNumber)
        {

            for (int i = 0; i < clickedCards.Count; i++)
            {
                _seq.Join(clickedCards[i].GetComponent<RectTransform>().DOMove(finalCardLocation.transform.position, 0.6f));
            }

            _seq.AppendCallback(() => {
                for (int i = 0; i < clickedCards.Count; i++)
                {
                    clickedCards[i].GetComponent<Image>().enabled = false;
                    clickedCards[i].GetComponent<Image>().color = Color.black;
                }
                //Invoke("CreateCard", 1f);
            });
        }
    }
}
