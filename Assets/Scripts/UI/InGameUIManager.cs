using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;
using Cinemachine;
using System;

public class InGameUIManager : MonoBehaviour
{
    public Button startButton;

    public GameObject cardPanel;

    public Image[] levelImages;

    public Text levelText;

    private Sequence _seq;
    public CinemachineVirtualCamera inGameVCam;
    public void OnClickStartButton()
    {
        _seq = DOTween.Sequence();

        _seq.Append(startButton.transform.DOScale(0f, 0.15f));
        _seq.AppendCallback(() => startButton.gameObject.SetActive(false));

        _seq.AppendCallback(() => inGameVCam.Priority = 20);
        _seq.AppendInterval(1.4f);

        _seq.Append(levelText.transform.DOMoveY(2430f, 0.8f));
        _seq.Join(cardPanel.transform.DOMoveY(460f, 1.2f));
        for (int i = 0; i < levelImages.Length; i++)
        {
            _seq.Append(levelImages[i].GetComponent<RectTransform>().DOAnchorPosY(-450f, 0.25f).SetEase(Ease.OutBack));
        }
    }

    [ContextMenu("�ʱ�ȭ")]
    public void EndCardPanelUI()
    {
        _seq.Append(cardPanel.transform.DOMoveY(-460f, -1.2f));
        for (int i = 0; i < levelImages.Length; i++)
        {
            _seq.Join(levelImages[i].GetComponent<RectTransform>().DOAnchorPosY(450f, 0.25f).SetEase(Ease.OutBack));
            _seq.Join(levelImages[i].DOColor(new Color(255,255, 255), 0.1f));
        }
        
    }
}
