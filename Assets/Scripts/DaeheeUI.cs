using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using Cinemachine;

public class DaeheeUI : MonoBehaviour
{
    public List<Image> stageImage = new List<Image>();

    public RectTransform cardPanelTransform;

    public Image cardPanelImage;

    public Transform camera;

    public Sequence _seq;

    public RectTransform stageTextTransform;

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
        if(Input.GetKeyDown(KeyCode.R))
        {
            Wrong();
        }
        if(Input.GetKeyDown(KeyCode.T))
        {
            GameOver();
        }
    }
    #region Wrong
    void Wrong()
    {
        _seq = DOTween.Sequence();

        _seq.Append(camera.DOShakePosition(3f, 0.2f));
        _seq.Join(cardPanelImage.DOColor(Color.red, 0.3f)
            .OnComplete(() =>
                _seq.Append(cardPanelImage.DOColor(Color.white, 3f))
                ));
        _seq.AppendCallback(() =>
        {
            _seq.Kill();
        });
    }

    #endregion

    #region CardPanelRst
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
    #endregion

    #region StageRst
    void StageRst()
    {
        _seq = DOTween.Sequence();
        foreach (Image image in stageImage)
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
    #endregion

    #region GameOver
    void GameOver()
    {
        _seq = DOTween.Sequence();

        
        foreach (Image image in stageImage)
        {
            _seq.Join(image.DOColor(new Color(0, 0, 0), 0.1f));
            _seq.Join(image.transform.DOScale(1.5f, 0.1f));
            _seq.Append(image.transform.DOScale(1.0f, 0.1f));
        }
        _seq.Append(stageTextTransform.DORotate((new Vector3(0, 0, 360)), 2.5f, RotateMode.LocalAxisAdd).SetEase(Ease.Linear));
        _seq.Join(stageTextTransform.DOScale((new Vector3(0, 0, 0)), 2.5f));

        foreach (Image image in stageImage)
        {
            _seq.Append(image.transform.DOScale(0.0f, 0.1f));
        }
        _seq.Join(cardPanelTransform.DOMoveY(-475.25f, 3f)).SetEase(Ease.OutBounce);
        _seq.AppendCallback(() =>
        {
            _seq.Kill();
        });     
    }
    #endregion

}
