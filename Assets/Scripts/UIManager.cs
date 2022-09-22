using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;
using System;

public class UIManager : MonoBehaviour
{
    public int cardCount = 2;

    public int finalCardsClick = 1;
    public int curCardsClick = 0;

    public RectTransform lastCardLocation;

    public Button startButton;

    public RectTransform cardPanel;

    public List<Frame> buttons = new List<Frame>();

    public List<Button> clickedCards = new List<Button>();

    public Frame card;

    private Sequence _seq;

    public SelectRandomShape selectRandomShape; // 나중에 고쳐야할듯

    void Awake()
    {
        //selectRandomShape = GetComponent<SelectRandomShape>();
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
            newCard.GetComponent<Button>().onClick.AddListener(delegate { ClickCard(); });
            //newCard.gameObject.SetActive(false);
        }
        //cardPanel.GetComponent<GridLayoutGroup>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        StageUp();
    }

    public void StageUp()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            cardCount = 2;
            finalCardsClick = 1;
            cardPanel.GetComponent<GridLayoutGroup>().cellSize = new Vector2(600, 700);
            cardPanel.GetComponent<GridLayoutGroup>().spacing = new Vector2(100, 50);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            cardCount = 4;
            finalCardsClick = 2;
            cardPanel.GetComponent<GridLayoutGroup>().cellSize = new Vector2(350, 400);
            cardPanel.GetComponent<GridLayoutGroup>().spacing = new Vector2(200, 50);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            cardCount = 6;
            finalCardsClick = 4;
            cardPanel.GetComponent<GridLayoutGroup>().cellSize = new Vector2(300, 400);
            cardPanel.GetComponent<GridLayoutGroup>().spacing = new Vector2(150, 50);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            cardCount = 8;
            finalCardsClick = 5;
            cardPanel.GetComponent<GridLayoutGroup>().cellSize = new Vector2(250, 350);
            cardPanel.GetComponent<GridLayoutGroup>().spacing = new Vector2(100, 100);
        }
        else if(Input.GetKeyDown(KeyCode.Space))
        {
            CreateCard();
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
        clickObject.GetComponent<Image>().color = Color.gray;
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
        Debug.Log(clickObject);

    }

    [ContextMenu("카드 생성")]
    public void CreateCard()
    {
        _seq = DOTween.Sequence();
        cardPanel.GetComponent<GridLayoutGroup>().enabled = false;
        for (int i=0;i<cardCount;i++)
        {
            if (buttons[i].GetComponent<Image>().enabled)
                continue;
            buttons[i].GetComponent<RectTransform>().localScale = new Vector3(2f, 2f, 0);
            buttons[i].GetComponent<Image>().enabled = true;
            buttons[i].GetComponent<Button>().enabled = true;
            _seq.Join(buttons[i].transform.DOScale(1f, 0.5f));
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
            buttons[i].enumShape = selectRandomShape.CurrentShapeList[i].enumShape;
        }

        
    }
}
