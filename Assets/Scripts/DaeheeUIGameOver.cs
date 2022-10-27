using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;

public class DaeheeUIGameOver : MonoBehaviour
{

    private Sequence _seq;
    private Sequence _restartTextSeq;
    public List<Image> btnImageList;
    public TextMeshProUGUI titleText;
    public RectTransform restartText;

    void Awake()
    {
        foreach (Image image in btnImageList)
        {
            _seq.Join(image.DOFade(0, 0f));
        }
        titleText.DOFade(0, 0f);
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

}
