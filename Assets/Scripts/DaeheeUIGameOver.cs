using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class DaeheeUIGameOver : MonoBehaviour
{

    private Sequence _seq;
    private Sequence _restartTextSeq;
    public List<Image> btnImageList;
    public TextMeshProUGUI titleText;
    public RectTransform restartText;


    public RectTransform settingPanel;
    public RectTransform quitBtnTransform;
    public RectTransform volumeBtnTransform;

    void Awake()
    {
        _seq = DOTween.Sequence();
        foreach (Image image in btnImageList)
        {
            _seq.Join(image.DOFade(0, 0f));
        }
        _seq.Join(titleText.DOFade(0, 0f));
        _seq.Join(settingPanel.DOAnchorPosX(-2000f, 2f));
        _seq.OnComplete(() =>
        {
            _seq.Kill();
        });

    }

    void Start()
    {
        StartUI();
    }

    void StartUI()
    {
        _seq = DOTween.Sequence();
        _seq.Append(titleText.DOFade(1, 2f));
        foreach (Image image in btnImageList)
        {
            _seq.Join(image.DOFade(1, 0.5f));
        }
        _seq.AppendCallback(() =>
        {
            _seq.Kill();
        });
        _restartTextSeq = DOTween.Sequence();
        _restartTextSeq.Append(restartText.DOScale(new Vector3(1.2f, 1.2f, 1.2f), 1f))
            .Append(restartText.DOScale(new Vector3(1f, 1f, 1f), 2f)).SetLoops(-1, LoopType.Restart);
    }


    public void SettingBtn()
    {
        _seq.Append(settingPanel.DOAnchorPosX(720, 1f).SetEase(Ease.OutBounce));

        _seq.AppendCallback(() =>
        {
            _seq.Kill();
        });

    }

    #region QuitBtn
    public void ClickDownQuitBtn()
    {
        _seq = DOTween.Sequence();
        Debug.Log("Down");
        _seq.Join(quitBtnTransform.DOScale(new Vector3(1.5f, 1.5f, 1.5f), .3f).SetEase(Ease.OutBounce));
        _seq.AppendCallback(() =>
        {
            _seq.Kill();
        });
    }

    public void ClickUpQuitBtn()
    {
        _seq = DOTween.Sequence();
        Debug.Log("Up");
        _seq.Join(quitBtnTransform.DOScale(new Vector3(1f, 1f, 1f), .3f).SetEase(Ease.OutBounce));
        _seq.AppendCallback(() =>
        {
            _seq.Kill();
        });
    }

    #endregion


    #region VolumeBtn
    public void ClickUpVolumeBtn()
    {
        _seq = DOTween.Sequence();
        Debug.Log("Up");
        _seq.Join(volumeBtnTransform.DOScale(new Vector3(4f, 4f, 4f), .3f).SetEase(Ease.OutBounce));
        _seq.AppendCallback(() =>
        {
            _seq.Kill();
        });
    }

    public void ClickDownVolumeBtn()
    {
        _seq = DOTween.Sequence();
        Debug.Log("Up");
        _seq.Join(volumeBtnTransform.DOScale(new Vector3(3f, 3f, 3f), .3f).SetEase(Ease.OutBounce));
        _seq.AppendCallback(() =>
        {
            _seq.Kill();
        });
    }
    #endregion
    public void RestartGame()
    {
        SceneManager.LoadScene(1);
    }
}
