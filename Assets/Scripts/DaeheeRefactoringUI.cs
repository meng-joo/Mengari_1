using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Net.Http.Headers;

public class DaeheeRefactoringUI : MonoBehaviour
{
    [Header("버튼들")]
    [SerializeField] private Button _rankingButton;
    [SerializeField] private Button _storeButton;
    [SerializeField] private Button _storeBackButton;
    [SerializeField] private Button _settingButton;
    [SerializeField] private Button _settingBackButton;
    [SerializeField] private Button _restartButton;
    [SerializeField] private List<Button> _overBtnList;
    [SerializeField] private List<Button> _btnGrp;
    [SerializeField] private List<TextMeshProUGUI> _wallTextList;
    [SerializeField] private List<GameObject> _endImageList = new List<GameObject>();

    [Header("이미지들")]
    [SerializeField] private Image _storeBackGround;
    [SerializeField] private Image _settingBackGround;
    [Header("UI오브젝트들")]
    [SerializeField] private GameObject _startTextObj;

    [Header("사운드클립")]
    public AudioClip uiAudioClip;

    [SerializeField] private bool _isMute;

    Sequence seq;

    private void Awake()
    {
        _storeButton.onClick.AddListener(() => PopUpStoreUI());
        _storeBackButton.onClick.AddListener(() => CloseStoreUI());
        _settingButton.onClick.AddListener(() => PopupSetting());
        _settingBackButton.onClick.AddListener(() => CloseSetting());
        _restartButton.onClick.AddListener(() => Restart());
        foreach (Button btn in _btnGrp)
        {
            btn.onClick.AddListener(() => UISound());
        }
        _isMute = false;
        SetBtnPosOver();
        CloseWallText();
    }

    #region 오버화면 버튼 위치 설정 메서드
    void SetBtnPosOver()
    {
        seq = DOTween.Sequence();
        foreach(Button btn in _overBtnList)
        {
            seq.Join(btn.transform.DOMoveY(+300f, 0.3f));
        }
        seq.AppendInterval(2f);
        seq.AppendCallback(() => seq.Kill());
    }
    private void SetBtnPosStart()
    {
        seq = DOTween.Sequence();
        Debug.Log("sdfasdf");
        foreach (Button btn in _overBtnList)
        {
            seq.Join(btn.transform.DOMoveY(-300f, 0.3f));
        }
        seq.AppendInterval(2f);
        seq.AppendCallback(()=>seq.Kill());
    }
    #endregion


    #region Wall텍스트 명도 조절 메서드
    void ShowWallText()
    {
        seq = DOTween.Sequence();
        foreach(var go in _wallTextList)
        {
            seq.Join(go.DOFade(1, .3f));
        }
        seq.AppendInterval(1f);
        seq.AppendCallback(() => seq.Kill());
    }

    void CloseWallText()
    {
        seq = DOTween.Sequence();
        foreach (var go in _wallTextList)
        {
            seq.Join(go.DOFade(0, .2f));
        }
        seq.AppendInterval(1f);
        seq.AppendCallback(() => seq.Kill());
    }
    #endregion
    private void UISound()
    {
        if(!_isMute)
            SoundManager.instance.SFXPlay("ui", uiAudioClip);
    }
    private void Restart()
    {
        SetBtnPosStart();
        ShowWallText();
        _restartButton.gameObject.SetActive(false);

    }

    #region StoreUI 위치설정 메서드
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
    #endregion

    #region SettingUI 위치설정 메서드
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
    #endregion

    /// <summary>
    /// 버튼이 눌릴때 버튼과 함께 실행될 이펙트들
    /// </summary>
    /// <param name="button"></param>
    /// <param name="isFade"></param>
    private void ButtonClickEffect(Button button/*, bool isFade = false*//*, bool isAble = true*/)
    {
        seq = DOTween.Sequence();

        seq.Append(button.transform.DOScale(1.6f, 0.1f));
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
