using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ? ì§œ : 2021-01-18 PM 4:05:58
// ?‘ì„±??: Rito

public abstract class AfterImageBase : MonoBehaviour
{
    /***********************************************************************
    *                               Public Fields
    ***********************************************************************/
    public Material _afterImageMaterial;
    public Gradient _afterImageGradient = new Gradient()
    {
        colorKeys = new GradientColorKey[]
        {
            new GradientColorKey(new Color(0.7f, 0.0f, 1.0f), 0.00f),
            new GradientColorKey(new Color(0.3f, 1.0f, 1.0f), 0.25f),
            new GradientColorKey(new Color(1.0f, 1.0f, 0.0f), 0.50f),
            new GradientColorKey(new Color(0.3f, 1.0f, 1.0f), 0.75f),
            new GradientColorKey(new Color(0.7f, 0.0f, 1.0f), 1.00f),
        }
    };

    [Range(0.01f, 5f), Tooltip("?‰ìƒ ?…ë°?´íŠ¸ ?ë„")]
    public float _colorUpdateSpeed = 1f;

    [Range(0.1f, 1.0f), Tooltip("?”ìƒ ?ì„± ì£¼ê¸°")]
    public float _bakingCycle = 0.1f;

    public AfterImageData _data = new AfterImageData();

    [Tooltip("Target Object???ì‹ ë©”ì‹œ?¤ë„ ?¬í•¨? ì? ?¬ë?")]
    public bool _containChildrenMeshes = true;

    /***********************************************************************
    *                               Protected Fields
    ***********************************************************************/
    protected GameObject _faderContainer;
    protected Color _currentColor;
    protected float _currentElapsedColorUpdateTime;
    protected float _currentElapsedBakeTime;

    protected Queue<AfterImageFaderBase> FaderWaitQueue { get; set; }   // ?¬ìš© ê°€?¥í•œ ëª©ë¡
    protected Queue<AfterImageFaderBase> FaderRunningQueue { get; set; } // ?„ì¬ ?œì„±?”ëœ ?”ìƒ ëª©ë¡
    protected int AvailableCount => FaderWaitQueue.Count;

    /***********************************************************************
    *                               Public Methods
    ***********************************************************************/
    #region .
    /// <summary> ?´ë?ì§€ê°€ ?¬ì‚¬?©í•  ì¤€ë¹„ê? ??</summary>
    public void SetImageReadyState(AfterImageFaderBase image)
    {
        FaderWaitQueue.Enqueue(image);
    }
    public void SetImageRunningState(AfterImageFaderBase image)
    {
        FaderRunningQueue.Enqueue(image);
    }

    #endregion

    /***********************************************************************
    *                               Protected Methods
    ***********************************************************************/

    protected abstract void Init();
    protected abstract void SetupFader(out AfterImageFaderBase fader);

    protected void UpdateColor()
    {
        _currentColor = _afterImageGradient.Evaluate(_currentElapsedColorUpdateTime);
    }
    protected void BakeImage()
    {
        AfterImageFaderBase fader;

        // 1. Get Or Create
        if (AvailableCount > 0)
        {
            fader = FaderWaitQueue.Dequeue();
        }
        else
        {
            SetupFader(out fader);
        }

        // 2. Set Pos/Rot, Color, Alpha
        fader.WakeUp(_currentColor);
        SetImageRunningState(fader);
    }

    /***********************************************************************
    *                               Unity Events
    ***********************************************************************/
    #region .
    protected void Start()
    {
        Init();
    }

    protected void Update()
    {
        _currentElapsedBakeTime += Time.deltaTime;
        _currentElapsedColorUpdateTime += Time.deltaTime * _colorUpdateSpeed;

        // 1. Bake
        if (_currentElapsedBakeTime >= _bakingCycle)
        {
            BakeImage();
            _currentElapsedBakeTime = 0f;
        }

        // 2. Update Color
        if (_currentElapsedColorUpdateTime > 1.0f)
        {
            _currentElapsedColorUpdateTime = 0f;
        }

        UpdateColor();
    }

    #endregion
}