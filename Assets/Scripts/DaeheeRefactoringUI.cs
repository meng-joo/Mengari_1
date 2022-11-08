using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class DaeheeRefactoringUI : MonoBehaviour
{
    [Header("버튼들")]
    [SerializeField] private Button _rankingButton;
    [SerializeField] private Button _storeButton;
    [SerializeField] private Button _storeBackButton;
    [SerializeField] private Button _settingButton;
    [SerializeField] private Button _settingBackButton;
    [SerializeField] private Button _restartButton;
    [SerializeField] private List<GameObject> _endImageList = new List<GameObject>();

    [Header("이미지들")]
    [SerializeField] private Image _storeBackGround;
    [SerializeField] private Image _settingBackGround;


    Sequence seq;

    private void Awake()
    {
        _storeBackButton.interactable = true;
        _storeButton.onClick.AddListener(() => PopUpStoreUI());
        _storeBackButton.onClick.AddListener(() => CloseStoreUI());
        _settingButton.onClick.AddListener(() => PopupSetting());
        _settingBackButton.onClick.AddListener(() => CloseSetting());
        _restartButton.onClick.AddListener(() => Restart());
    }

    private void Restart()
    {
        SceneManager.LoadScene(1);
    }
    private void PopUpStoreUI()
    {
        seq = DOTween.Sequence();

        ButtonClickEffect(_storeButton /*true*//*, false*/);
        seq.AppendInterval(0.5f);
        seq.Append(_storeBackGround.transform.DOMoveY(2560 / 2, 0.26f));
        seq.AppendCallback(() =>
        {
            seq.Kill();
        });
    }

    private void CloseStoreUI()
    {
        ButtonClickEffect(_storeBackButton /*false*//*, true*/);
        _storeBackGround.transform.DOMoveY(-2560 / 2, 0.34f);
    }

    private void PopupSetting()
    {
        seq = DOTween.Sequence();

        ButtonClickEffect(_settingButton /*false*//*, false*/);
        seq.AppendInterval(0.1f);
        seq.Append(_settingBackGround.transform.DOMoveY(2560 / 2, 0.15f));
    }

    private void CloseSetting()
    {
        ButtonClickEffect(_settingBackButton /*false*//*, true*/);
        _settingBackGround.transform.DOMoveY(2560 + (2560 / 2), 0.34f);
    }

    /// <summary>
    /// 버튼이 눌릴때 버튼과 함께 실행될 이펙트들
    /// </summary>
    /// <param name="button"></param>
    /// <param name="isFade"></param>
    private void ButtonClickEffect(Button button/*, bool isFade = false*//*, bool isAble = true*/)
    {
        seq = DOTween.Sequence();

        seq.Append(button.transform.DOScale(1.8f, 0.1f));
        seq.Append(button.transform.DOScale(2f, 0.05f));

       // int fadevalue = isFade ? 0 : 1;

        for (int i = 0; i < _endImageList.Count; i++)
        {
            Button b = _endImageList[i].GetComponent<Button>();
            if (b != null)
            {
                Debug.Log(b.name);
                //seq.AppendCallback(() => b.interactable = isAble);
            }
        }

        //for (int i = 0; i < _endImageList.Count; i++)
        //{
        //    seq.Append(_endImageList[i].GetComponent<Image>()?.DOFade(fadevalue, 0.05f));
        //    seq.Append(_endImageList[i].GetComponent<TextMeshProUGUI>()?.DOFade(fadevalue, 0.05f));
        //}


        //seq.AppendCallback(()=>
        //{
        //seq.Kill();

        //});
    }
}
