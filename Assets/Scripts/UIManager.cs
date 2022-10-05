using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;
using System;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private int cardCount = 2;
    public int CardCount
    {
        get => cardCount;
        set => cardCount = value;
    }

    public int finalCardsClick = 1;
    public int curCardsClick = 0;

    public bool isBoss;

    public RectTransform lastCardLocation;

    public Button startButton;

    public RectTransform cardPanel;

    public RectTransform stagePanel;

    public int stageLevel = 1;

    public List<Image> stageImage = new List<Image>();

    public List<Frame> buttons = new List<Frame>();

    public List<Frame> clickedCards = new List<Frame>();

    public Text stageText;

    public int frontStageNumber = 1;

    public Frame BossCard;

    public Frame card;

    private Sequence _seq;

    private SelectRandomShape selectRandomShape; // ���߿� ���ľ��ҵ�

    public List<Shape> ShapeList = new List<Shape>();

    //public Shape shape;

    public List<int> randomList = new List<int>();

    void Awake()
    {
        selectRandomShape = GetComponent<SelectRandomShape>();
    }

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < System.Enum.GetValues(typeof(EnumShape)).Length; i++)
        {
            Shape shape = new Shape();
            shape.enumShape = (EnumShape)i;
            string shapeString = System.Enum.GetName(typeof(EnumShape), i);
            shape.sprite = Resources.Load<Sprite>($"Shapes/{shapeString}");
            ShapeList.Add(shape);
            //randomList.Add(i);
        }

        //ShuffleList(randomList);

        ShuffleList(ShapeList);

        int count = System.Enum.GetValues(typeof(EnumShape)).Length;

        for (int i=0;i< System.Enum.GetValues(typeof(EnumShape)).Length; i++)
        {
            Frame newCard = Instantiate(card, cardPanel);
            buttons.Add(newCard);
            newCard.GetComponent<Button>().enabled = false;
            newCard.GetComponent<Image>().enabled = false;
            newCard.shape = ShapeList[i];
            newCard.button.image.sprite = newCard.shape.sprite;

            EventTrigger eventTrigger = newCard.gameObject.AddComponent<EventTrigger>();

            EventTrigger.Entry entry_PointerDown = new EventTrigger.Entry();
            entry_PointerDown.eventID = EventTriggerType.PointerDown;
            entry_PointerDown.callback.AddListener((data) => { OnPointerDown((PointerEventData)data); });
            eventTrigger.triggers.Add(entry_PointerDown);

            EventTrigger.Entry entry_PointerUp = new EventTrigger.Entry();
            entry_PointerUp.eventID = EventTriggerType.PointerUp;
            entry_PointerUp.callback.AddListener((data) => { OnPointerUp((PointerEventData)data); });
            eventTrigger.triggers.Add(entry_PointerUp);

            newCard.GetComponent<Button>().onClick.AddListener(delegate {
                ClickCard();

            });
            //newCard.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //selectRandomShape.ClearList();
            //cardCount += 2;
            CreateCard();
        }
        //StageUp();
    }

    public void IncreaseDifficult()
    {
        //ó�� ���̵��� �������� 2�� ����Ʈ���� 4��
    }

    [ContextMenu("색칠")]
    public void StageLevelImage()
    {
        if(stageLevel>=7)
        {
            stageLevel = 1;
            frontStageNumber++;
            for(int i=0;i<stageImage.Count;i++)
            {
                stageImage[i].color = new Color(255, 255, 255);
            }
        }
        int lvIdx = stageLevel - 1;
        _seq = DOTween.Sequence();
        _seq.Append(stageText.DOFade(0f, 0.8f));
        _seq.AppendCallback(() =>
        {
            stageText.GetComponent<RectTransform>().localScale = new Vector3(0.5f, 0.5f, 0);
            stageText.text = frontStageNumber + "-" + stageLevel;
            stageText.DOFade(1f, 0.5f).SetEase(Ease.OutQuad);
            stageText.GetComponent<RectTransform>().DOScale(1f, 0.6f);
        });
        if (lvIdx != 5)
        {
            _seq.Join(stageImage[lvIdx].DOColor(new Color(255, 22, 0), 0.1f));
            _seq.Append(stageImage[lvIdx].GetComponent<RectTransform>().DOScale(2.5f, 0.3f));
            _seq.Append(stageImage[lvIdx].GetComponent<RectTransform>().DOScale(1f, 0.4f));
        }
        else
        {
            _seq.Join(stageImage[lvIdx].DOColor(new Color(0, 206, 255), 0.1f));
            _seq.Append(stageImage[lvIdx].GetComponent<RectTransform>().DOScale(3f, 0.3f));
            _seq.Append(stageImage[lvIdx].GetComponent<RectTransform>().DOScale(1.5f, 0.4f));
        }
        _seq.AppendCallback(() => { _seq.Kill(); });
    }

    public void StageUp()
    {
        if (cardCount == 2)
        {
            finalCardsClick = 1;
            cardPanel.GetComponent<GridLayoutGroup>().cellSize = new Vector2(600, 700);
            cardPanel.GetComponent<GridLayoutGroup>().spacing = new Vector2(100, 50);
        }
        else if (cardCount == 4)
        {
            finalCardsClick = 2;
            cardPanel.GetComponent<GridLayoutGroup>().cellSize = new Vector2(350, 400);
            cardPanel.GetComponent<GridLayoutGroup>().spacing = new Vector2(200, 50);
        }
        else if (cardCount == 6)
        {
            finalCardsClick = 3;
            cardPanel.GetComponent<GridLayoutGroup>().cellSize = new Vector2(300, 400);
            cardPanel.GetComponent<GridLayoutGroup>().spacing = new Vector2(150, 50);
        }
        else if (cardCount == 8)
        {
            finalCardsClick = 4;
            cardPanel.GetComponent<GridLayoutGroup>().cellSize = new Vector2(250, 350);
            cardPanel.GetComponent<GridLayoutGroup>().spacing = new Vector2(100, 100);
        }
    }

    public void OnClickStartButton()
    {
        _seq = DOTween.Sequence();
        _seq.Append(startButton.transform.DOScale(0f, 0.15f));
        _seq.Append(cardPanel.DOAnchorPosY(-cardPanel.anchoredPosition.y, 0.5f));
        _seq.Join(stagePanel.DOAnchorPosY(stagePanel.anchoredPosition.y - 270f, 0.5f));
        _seq.AppendCallback(() => { _seq.Kill(); });
        startButton.gameObject.SetActive(false);
    }

    public void ClickCard()
    {
        var clickObject = EventSystem.current.currentSelectedGameObject;

        clickObject.GetComponent<Image>().DOColor(Color.gray, 0.1f);
        //��ư �������� interactable ����
        clickObject.GetComponent<Button>().enabled = false;
        clickedCards.Add(clickObject.GetComponent<Frame>());
        curCardsClick++;
        if (curCardsClick == finalCardsClick)
        {
            _seq = DOTween.Sequence();
            for (int i = 0; i < clickedCards.Count; i++)
            {
                _seq.Join(clickedCards[i].GetComponent<RectTransform>().DOMove(lastCardLocation.transform.position, 0.6f));
            }
            _seq.AppendCallback(() => {
                _seq.Kill();
                curCardsClick = 0;
                cardPanel.GetComponent<GridLayoutGroup>().enabled = false;
                cardPanel.GetComponent<GridLayoutGroup>().enabled = true;

                for (int i = 0; i < clickedCards.Count; i++)
                {
                    clickedCards[i].GetComponent<Image>().enabled = false;
                    clickedCards[i].GetComponent<Image>().color = Color.black;


                    int random = UnityEngine.Random.Range(cardCount + 1, System.Enum.GetValues(typeof(EnumShape)).Length);
                    Shape shape= new Shape();
                    shape.enumShape = clickedCards[i].shape.enumShape;
                    shape.sprite = clickedCards[i].shape.sprite;
                    Debug.Log(random);
                    clickedCards[i].shape.enumShape = ShapeList[random].enumShape;
                    clickedCards[i].shape.sprite = ShapeList[random].sprite;
                    ShapeList[random].enumShape = shape.enumShape;
                    ShapeList[random].sprite = shape.sprite;
                }
                //생성코드
                fillCard();
            });
        }
    }

    public void fillCard() //클릭되서 사용된 카드를 채운다.
    {
        _seq = DOTween.Sequence();
        for (int i = 0; i < clickedCards.Count; i++)
        {
            clickedCards[i].GetComponent<Image>().sprite = clickedCards[i].shape.sprite;
            clickedCards[i].GetComponent<RectTransform>().localScale = new Vector3(1.5f, 1.5f, 0);
            clickedCards[i].GetComponent<Image>().enabled = true;
            clickedCards[i].GetComponent<Button>().enabled = true;
            _seq.Join(clickedCards[i].transform.DOScale(1f, 0.3f));
        }
        _seq.AppendCallback(() => {
            _seq.Kill();
            cardPanel.GetComponent<GridLayoutGroup>().enabled = true;
        });
        clickedCards.Clear();
    }

    public void OnPointerDown(PointerEventData data)
    {
        var clickObject = EventSystem.current.currentSelectedGameObject;
        _seq = DOTween.Sequence();
        _seq.Append(clickObject.transform.DOScale(0.95f, 0.15f));
    }

    public void OnPointerUp(PointerEventData data)
    {
        var clickObject = EventSystem.current.currentSelectedGameObject;
        _seq = DOTween.Sequence();
        _seq.Append(clickObject.transform.DOScale(1f, 0.15f));
    }



    [ContextMenu("ī�� ����")]
    public void CreateCard() //처음생성
    {

        StageUp();

        //selectRandomShape.GameStart();
        if(!isBoss)
        {
            _seq = DOTween.Sequence();
            cardPanel.GetComponent<GridLayoutGroup>().enabled = false;
            for (int i = 0; i < cardCount; i++)
            {
                if (buttons[i].GetComponent<Image>().enabled)
                    continue;
                buttons[i].GetComponent<RectTransform>().localScale = new Vector3(1.5f, 1.5f, 0);
                Debug.Log(buttons[i].GetComponent<RectTransform>().localScale);
                buttons[i].GetComponent<Image>().enabled = true;
                buttons[i].GetComponent<Button>().enabled = true;
                _seq.Join(buttons[i].transform.DOScale(1f, 0.3f));
            }
            _seq.AppendCallback(() => {
                _seq.Kill();
                cardPanel.GetComponent<GridLayoutGroup>().enabled = true;
            });
        }

    }

    public void ShuffleList<T>(List<T> list)
    {
        int random1;
        int random2;

        T tmp;

        for (int index = 0; index < 250; ++index)
        {
            random1 = UnityEngine.Random.Range(0, list.Count);
            random2 = UnityEngine.Random.Range(0, list.Count);

            tmp = list[random1];
            list[random1] = list[random2];
            list[random2] = tmp;
        }
    }
}
