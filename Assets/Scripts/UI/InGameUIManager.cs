using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;
using System;

public class InGameUIManager : MonoBehaviour
{
    public Button startButton;

    public GameObject cardPanel;

    public Image[] levelImages;

    public Text levelText;

    private Sequence _seq;

    public void OnClickStartButton()
    {
        _seq = DOTween.Sequence();
        _seq.Append(startButton.transform.DOScale(0f, 0.15f));
        _seq.Join(levelText.transform.DOMoveY(2430f, 0.3f));
        _seq.Join(cardPanel.transform.DOMoveY(500f, 0.3f));
        for (int i = 0; i < levelImages.Length; i++)
        {
            _seq.Append(levelImages[i].transform.DOMoveY(2320f, 0.15f).SetEase(Ease.OutBack));
        }
        startButton.gameObject.SetActive(false);
    }
}
