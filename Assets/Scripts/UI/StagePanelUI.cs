using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class StagePanelUI : MonoBehaviour
{
    public int frontStageNumber = 1;
    //1-1의 2
    public int backStageNumber = 1;

    private Sequence _seq;

    InGameUIManager _inGameUIManager;

    private void Start()
    {
        _inGameUIManager = GetComponent<InGameUIManager>();
        
    }
    //레벨업시 UI갱신
    [ContextMenu("레벨업 UI갱신")]
    public void Renewal()
    {
        frontStageNumber = WallManager.stageLevel / 6 + 1;
       
        backStageNumber = WallManager.stageLevel2 % 6 + 1;   
        //색깔 하양색으로 Reset
        if (backStageNumber == 1)
        {
            for (int i = 0; i < _inGameUIManager.levelImages.Length; i++)
            {
                _inGameUIManager.levelImages[i].color = new Color(255, 255, 255);
            }
        }
        _seq = DOTween.Sequence();
        _seq.Append(_inGameUIManager.levelText.DOFade(0f, 0.8f));

        _seq.AppendCallback(() =>
        {
            _inGameUIManager.levelText.GetComponent<RectTransform>().localScale = new Vector3(0.5f, 0.5f, 0);
            _inGameUIManager.levelText.text = frontStageNumber + "-" + backStageNumber;
            _inGameUIManager.levelText.DOFade(1f, 0.5f).SetEase(Ease.OutQuad);
            _inGameUIManager.levelText.GetComponent<RectTransform>().DOScale(1f, 0.6f);
        });

        int index = backStageNumber - 1;

        if (index != 5)
        {
            _seq.Join(_inGameUIManager.levelImages[index].DOColor(new Color(255, 22, 0), 0.1f));
            _seq.Append(_inGameUIManager.levelImages[index].GetComponent<RectTransform>().DOScale(2.5f, 0.3f));
            _seq.Append(_inGameUIManager.levelImages[index].GetComponent<RectTransform>().DOScale(1f, 0.4f));
        }
        else
        {
            _seq.Join(_inGameUIManager.levelImages[index].DOColor(new Color(0, 206, 255), 0.1f));
            _seq.Append(_inGameUIManager.levelImages[index].GetComponent<RectTransform>().DOScale(3f, 0.3f));
            _seq.Append(_inGameUIManager.levelImages[index].GetComponent<RectTransform>().DOScale(1.5f, 0.4f));
        }

        _seq.AppendCallback(() => { _seq.Kill(); });
    }
}
