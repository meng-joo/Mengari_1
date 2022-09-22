using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class UIManager : MonoBehaviour
{
    public int cardCount = 2;

    public Button startButton;

    public RectTransform cardPanel;

    public List<Frame> buttons = new List<Frame>();

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
            cardPanel.GetComponent<GridLayoutGroup>().cellSize = new Vector2(600, 700);
            cardPanel.GetComponent<GridLayoutGroup>().spacing = new Vector2(100, 50);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            cardCount = 4;
            cardPanel.GetComponent<GridLayoutGroup>().cellSize = new Vector2(350, 400);
            cardPanel.GetComponent<GridLayoutGroup>().spacing = new Vector2(200, 50);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            cardCount = 6;
            cardPanel.GetComponent<GridLayoutGroup>().cellSize = new Vector2(300, 400);
            cardPanel.GetComponent<GridLayoutGroup>().spacing = new Vector2(150, 50);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            cardCount = 8;
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

    [ContextMenu("카드 생성")]
    public void CreateCard()
    {
        cardPanel.GetComponent<GridLayoutGroup>().enabled = false;
        for (int i=0;i<cardCount;i++)
        {
            _seq = DOTween.Sequence();
            buttons[i].GetComponent<RectTransform>().localScale = new Vector3(2f, 2f, 0);
            buttons[i].GetComponent<Image>().enabled = true;
            _seq.Append(buttons[i].transform.DOScale(1f, 0.5f));
            buttons[i].GetComponent<Button>().enabled = true;
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
