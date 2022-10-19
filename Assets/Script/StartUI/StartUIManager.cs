using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;

public class StartUIManager : MonoBehaviour
{
    [Header("버튼들")]
    [SerializeField] private Button _storeButton;
    [SerializeField] private Button _startButton;
    [SerializeField] private Button _settingButton;
    [SerializeField] private Button _storeBackButton;
    [SerializeField] private List<GameObject> _startImageList = new List<GameObject>();

    [Header("이미지들")]
    [SerializeField] private Image _storeBackGround;

    Sequence seq;

    private void Awake()
    {
        _storeButton.onClick.AddListener(() => PopUpStoreUI());
        _startButton.onClick.AddListener(() => StartGame());
        _settingButton.onClick.AddListener(() => PopupSetting());
        _storeBackButton.onClick.AddListener(() => CloseStoreUI());
    }

    private void StartGame()
    {
        Debug.Log("게임 시작");
        ButtonClickEffect(_startButton, true);
        // 기능 구현 해야함
    }

    private void PopUpStoreUI()
    {
        seq = DOTween.Sequence();

        ButtonClickEffect(_storeButton, true);
        seq.AppendInterval(0.4f);
        seq.Append(_storeBackGround.transform.DOMoveY(2560 / 2, 0.26f));
    }

    private void PopupSetting()
    {
        ButtonClickEffect(_settingButton, true);
    }

    private void CloseStoreUI()
    {
        ButtonClickEffect(_storeBackButton);
        _storeBackGround.transform.DOMoveY(-2560 / 2, 0.34f);
    }

    /// <summary>
    /// 버튼이 눌릴때 버튼과 함께 실행될 이펙트들
    /// </summary>
    /// <param name="button"></param>
    /// <param name="isFade"></param>
    private void ButtonClickEffect(Button button, bool isFade = false)
    {
        seq = DOTween.Sequence();

        seq.Append(button.transform.DOScale(0.95f, 0.1f));
        seq.Append(button.transform.DOScale(1.08f, 0.05f));

        int fadevalue = isFade ? 0 : 1;

        for (int i = 0; i < _startImageList.Count; i++)
        {
            seq.Append(_startImageList[i].GetComponent<Image>()?.DOFade(fadevalue, 0.05f));
            seq.Append(_startImageList[i].GetComponent<TextMeshProUGUI>()?.DOFade(fadevalue, 0.05f));

            Button b = _startImageList[i].GetComponent<Button>();
            if (b != null)
            {
                seq.AppendCallback(() => b.interactable = !isFade);
            }
        }
    }
}