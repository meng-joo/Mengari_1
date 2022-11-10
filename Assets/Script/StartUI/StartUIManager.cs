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
    [SerializeField] private Button _settingBackButton;
    [SerializeField] private List<GameObject> _startImageList = new List<GameObject>();

    [Header("이미지들")]
    [SerializeField] private RectTransform _storeBackGround;
    [SerializeField] private RectTransform _settingBackGround;

    private int _height; 

    Sequence seq;

    private void Awake()
    {
        _storeButton.onClick.AddListener(() => PopUpStoreUI());
        _startButton.onClick.AddListener(() => StartGame());
        _settingButton.onClick.AddListener(() => PopupSetting());
        _storeBackButton.onClick.AddListener(() => CloseStoreUI());
        _settingBackButton.onClick.AddListener(() => CloseSetting());
    }

    private void Start()
    {
        
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

        ButtonClickEffect(_storeButton, true, false);
        seq.AppendInterval(0.5f);
        seq.Append(_storeBackGround.DOAnchorPosY(0, 0.26f));
    }

    private void PopupSetting()
    {
        seq = DOTween.Sequence();

        ButtonClickEffect(_settingButton, false, false);
        seq.AppendInterval(0.1f);   
        seq.Append(_settingBackGround.DOAnchorPosY(-1920, 0.15f));
    }

    private void CloseStoreUI()
    {
        ButtonClickEffect(_storeBackButton, false, true);
        _storeBackGround.DOAnchorPosY(-1920, 0.34f);
    }

    private void CloseSetting()
    {
        ButtonClickEffect(_settingBackButton, false, true);
        _settingBackGround.DOAnchorPosY(0, 0.34f);
    }

    /// <summary>
    /// 버튼이 눌릴때 버튼과 함께 실행될 이펙트들
    /// </summary>
    /// <param name="button"></param>
    /// <param name="isFade"></param>
    private void ButtonClickEffect(Button button, bool isFade = false, bool isAble = false)
    {
        seq = DOTween.Sequence();

        seq.Append(button.transform.DOScale(0.95f, 0.1f));
        seq.Append(button.transform.DOScale(1.08f, 0.05f));

        int fadevalue = isFade ? 0 : 1;

        for (int i = 0; i < _startImageList.Count; i++)
        {
            Button b = _startImageList[i].GetComponent<Button>();
            if (b != null)
            {
                seq.AppendCallback(() => b.interactable = isAble);
            }
        }

        for (int i = 0; i < _startImageList.Count; i++)
        {
            seq.Append(_startImageList[i].GetComponent<Image>()?.DOFade(fadevalue, 0.05f));
            seq.Append(_startImageList[i].GetComponent<TextMeshProUGUI>()?.DOFade(fadevalue, 0.05f));
        }
    }
}