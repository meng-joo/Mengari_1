using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;
using System;

public class UIManagerStage : MonoBehaviour
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

    public RectTransform lastCardLocation;

    public Button startButton;

    public RectTransform cardPanel;

    public List<Frame> buttons = new List<Frame>();

    public List<Button> clickedCards = new List<Button>();

    public Frame card;

    private Sequence _seq;

    private SelectRandomShape1 selectRandomShape; // ���߿� ���ľ��ҵ�

    void Awake()
    {

        selectRandomShape = GetComponent<SelectRandomShape1>();
    }

    // Start is called before the first frame update
    void Start()
    {
        for(int i=0;i<8;i++)
        {
            var newCard = Instantiate(card, cardPanel);
            buttons.Add(newCard);
            newCard.GetComponent<Button>().enabled = false;
            newCard.GetComponent<Image>().enabled = false;

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
            selectRandomShape.ClearList();
            if(cardCount != 8)
            {
                cardCount += 2;
            }

            CreateCard();
        }
        //StageUp();
    }

    public void IncreaseDifficult()
    {
        //ó�� ���̵��� �������� 2�� ����Ʈ���� 4��
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
            finalCardsClick = 4;
            cardPanel.GetComponent<GridLayoutGroup>().cellSize = new Vector2(300, 400);
            cardPanel.GetComponent<GridLayoutGroup>().spacing = new Vector2(150, 50);
        }
        else if (cardCount == 8)
        {
            finalCardsClick = 5;
            cardPanel.GetComponent<GridLayoutGroup>().cellSize = new Vector2(250, 350);
            cardPanel.GetComponent<GridLayoutGroup>().spacing = new Vector2(100, 100);
        }
    }

    public void OnClickStartButton()
    {
        _seq = DOTween.Sequence();
        _seq.Append(startButton.transform.DOScale(0f, 0.15f));
        _seq.Append(cardPanel.DOAnchorPosY(-cardPanel.anchoredPosition.y, 0.5f));
        _seq.AppendCallback(() => { _seq.Kill(); });
        startButton.gameObject.SetActive(false);
    }

    public void ClickCard()
    {
        var clickObject = EventSystem.current.currentSelectedGameObject;
        
        clickObject.GetComponent<Image>().DOColor(Color.gray, 0.1f);
        //��ư �������� interactable ����
        clickObject.GetComponent<Button>().enabled = false;
        clickedCards.Add(clickObject.GetComponent<Button>());
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
                }
                clickedCards.Clear();
            });
        }
    }

    public void OnPointerDown(PointerEventData data)
    {
        var clickObject = EventSystem.current.currentSelectedGameObject;
        _seq = DOTween.Sequence();
        _seq.Append(clickObject.transform.DOScale(0.95f, 0.15f));
        Debug.Log("Pointer Down");

    }

    public void OnPointerUp(PointerEventData data)
    {
        var clickObject = EventSystem.current.currentSelectedGameObject;
        _seq = DOTween.Sequence();
        _seq.Append(clickObject.transform.DOScale(1f, 0.15f));
        Debug.Log("Pointer Up");
    }

    [ContextMenu("ī�� ����")]
    public void CreateCard()
    {

        StageUp();

        selectRandomShape.GameStart();

        _seq = DOTween.Sequence();
        cardPanel.GetComponent<GridLayoutGroup>().enabled = false;
        for (int i=0;i<cardCount;i++)
        {
            if (buttons[i].GetComponent<Image>().enabled)
                continue;
            buttons[i].GetComponent<RectTransform>().localScale = new Vector3(1.5f, 1.5f, 0);
            Debug.Log(buttons[i].GetComponent<RectTransform>().localScale);
            buttons[i].GetComponent<Image>().enabled = true;
            buttons[i].GetComponent<Button>().enabled = true;
            _seq.Join(buttons[i].transform.DOScale(1f, 0.3f));
        }
        _seq.AppendCallback(() => { _seq.Kill();
            cardPanel.GetComponent<GridLayoutGroup>().enabled = true;
        });

        SetCardComponent();
    }

    private void SetCardComponent()
    {
        for(int i = 0; i < cardCount; i++)
        {
            buttons[i].button.image.sprite = selectRandomShape.CurrentShapeList[i].sprite;
            buttons[i].shape.enumShape = selectRandomShape.CurrentShapeList[i].enumShape;
        }
    }
}