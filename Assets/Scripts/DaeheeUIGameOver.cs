using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using Unity.XR.OpenVR;

public class DaeheeUIGameOver : MonoBehaviour
{

	private Sequence _seq;
	private Sequence _restartTextSeq;
	public List<Image> btnImageList;
	public TextMeshProUGUI titleText;
	public RectTransform restartText;


	public RectTransform settingPanel;
	public RectTransform quitBtnTransform;
	public RectTransform effectVolumeSound;
	public RectTransform bgmVolumeSound;

	public Image backgroundImg;


	#region Vive Valuable
	public GameObject vibeObject;
	public List<Image> VibeImages;
	private int _vibeIndex = 0;
	#endregion

	public AudioClip uiAudioClip;

    void Awake()
	{
		_seq = DOTween.Sequence();
		foreach (Image image in btnImageList)
		{
			_seq.Join(image.DOFade(0, 0f));
		}
		_seq.Join(settingPanel.DOScale(0, 0));
		_seq.Join(titleText.DOFade(0, 0f));
		_seq.Join(backgroundImg.DOFade(0, 0));
		_seq.OnComplete(() =>
		{
			_seq.Kill();
            StartUI();
        });
		backgroundImg.enabled = false;
	}

	void Start()
	{
		
	}

	void StartUI()
	{
		_seq = DOTween.Sequence();
		_seq.Append(titleText.DOFade(1, 2f));
		foreach (Image image in btnImageList)
		{
			_seq.Join(image.DOFade(1, 0.5f));
		}
		_seq.Join(titleText.DOFade(1, 0.3f));
		_seq.Join(backgroundImg.DOFade(0f, .3f)).OnComplete(()=>
		{
            _seq.Kill();
        });
		_restartTextSeq = DOTween.Sequence();
		_restartTextSeq.Append(restartText.DOScale(new Vector3(1.2f, 1.2f, 1.2f), 1f))
			.Append(restartText.DOScale(new Vector3(1f, 1f, 1f), 2f)).SetLoops(-1, LoopType.Restart);
        vibeObject.GetComponent<Image>().sprite = VibeImages[0].sprite;

    }


    public void SettingBtn()
	{
		SoundManager.instance.SFXPlay("ui", uiAudioClip);
        _seq = DOTween.Sequence();
        _seq.Append(settingPanel.DOAnchorPosX(0, 1f).SetEase(Ease.OutBounce));
		_seq.Join(settingPanel.DOScale(new Vector3(1, 1, 1), 0.7f));
		backgroundImg.enabled = true;
		_seq.Join(backgroundImg.DOFade(0.8f, 0.7f)).OnComplete(()=>
		{
			_seq.Kill();
		});
	}

	#region QuitBtn
	public void ClickDownQuitBtn()
	{
        SoundManager.instance.SFXPlay("ui", uiAudioClip);

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
		SoundManager.instance.SFXPlay("ui", uiAudioClip);

		_seq = DOTween.Sequence();
		Debug.Log("Up");
		_seq.Join(quitBtnTransform.DOScale(new Vector3(1f, 1f, 1f), .3f).SetEase(Ease.OutBounce));
		backgroundImg.enabled = false;
		_seq.Join(settingPanel.DOScale(new Vector3(0,0,0), 0.2f)).SetEase(Ease.Linear);
		_seq.Join(backgroundImg.DOFade(0, 0.2f));
		_seq.AppendCallback(() =>
		{
			_seq.Kill();
		});
	}

    #endregion


    #region VolumeBtn

    #region Effect
    public void ClickUpEffectVolumeBtn()
	{
        SoundManager.instance.SFXPlay("ui", uiAudioClip);

        _seq = DOTween.Sequence();
		Debug.Log("Up");
		_vibeIndex = _vibeIndex == 1 ? 0 : 1;
        vibeObject.GetComponent<Image>().sprite = VibeImages[_vibeIndex].sprite;
        _seq.Append(effectVolumeSound.DOScale(new Vector3(4f, 4f, 4f), .3f).SetEase(Ease.OutBounce));
		_seq.AppendCallback(() =>
		{
			_seq.Kill();
		});
	}

	public void ClickDownEffectVolumeBtn()
	{
		_seq = DOTween.Sequence();
		Debug.Log("Up");
		_seq.Append(effectVolumeSound.DOScale(new Vector3(3f, 3f, 3f), .3f).SetEase(Ease.OutBounce));
		_seq.AppendCallback(() =>
		{
			_seq.Kill();
		});
	}
    #endregion

    #region BGM
    public void ClickUpBGMVolumeBtn()
    {
        _seq = DOTween.Sequence();
        Debug.Log("Up");
        _seq.Join(bgmVolumeSound.DOScale(new Vector3(4f, 4f, 4f), .3f).SetEase(Ease.OutBounce));
        _seq.AppendCallback(() =>
        {
            _seq.Kill();
        });
    }

    public void ClickDownBGMVolumeBtn()
    {
        _seq = DOTween.Sequence();
        Debug.Log("Up");
        _seq.Join(bgmVolumeSound.DOScale(new Vector3(3f, 3f, 3f), .3f).SetEase(Ease.OutBounce));
        _seq.AppendCallback(() =>
        {
            _seq.Kill();
        });
    }


    #endregion
    #endregion
    public void RestartGame()
	{
        SoundManager.instance.SFXPlay("ui", uiAudioClip);

        SceneManager.LoadScene(1);
	}
}
