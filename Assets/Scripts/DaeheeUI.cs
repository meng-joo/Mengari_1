using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class DaeheeUI : MonoBehaviour
{
    public List<Image> stageImage = new List<Image>();

    public RectTransform cardPanelTransform;

    public Sequence _seq;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.W))
        {
            StageRst();
        }
        if(Input.GetKeyDown(KeyCode.E))
        {
            CardPanelRst();
        }
    }

    void CardPanelRst()
    {
        _seq = DOTween.Sequence();
        _seq.Append(cardPanelTransform.DOMoveY(-475.25f, 0.3f)).SetEase(Ease.OutCubic);
        _seq.AppendCallback(() =>
        {
            _seq.Append(cardPanelTransform.DOMoveY(475.25f, 0.3f)).SetEase(Ease.OutBounce);
        });
        _seq.AppendCallback(() =>
        {
            _seq.Kill();
        });
    }

    void StageRst()
    {
        _seq = DOTween.Sequence();
        foreach(Image image in stageImage)
        {
            _seq.Append(image.DOColor(new Color(255, 255, 255), 0.1f));
            _seq.Join(image.transform.DOScale(1.5f, 0.1f));
            _seq.AppendCallback(() =>
            {
                _seq.Append(image.transform.DOScale(1.0f, 0.1f));
            });
        }
        _seq.AppendCallback(() =>
        {
            _seq.Kill();
        });
    }
}
