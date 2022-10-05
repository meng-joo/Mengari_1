using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening; 

public class ParticleEffect : PoolableMono
{

    [SerializeField]
    private ParticleSystem _effectSystem;
    [SerializeField]
    private float _timer = 0f;
    [SerializeField]
    private float _duration; 
    [SerializeField]
    private bool _isPlay = false;

    [SerializeField]
    private Material _mat; 

    public float Duration => _duration; 
    public ParticleSystem EffectSystem
    {
        get
        {
            if(_effectSystem == null)
            {
                _effectSystem = GetComponent<ParticleSystem>();
            }
            return _effectSystem; 
        }
    }


    private void Awake()
    {
        _effectSystem = GetComponent<ParticleSystem>();
        var a = _effectSystem.GetComponentsInChildren<ParticleSystem>();
        for (int i = 0; i < a.Length; i++)
        {
            float dur = a[i].main.duration;
            if (_duration < dur)
            {
                _duration = dur;
            }
        }
        _isPlay = false;
    }

    Color color; 
    private void Update()
    {
        if(_isPlay == true)
        {
            _timer += Time.deltaTime; 
            if(_timer >= _duration)
            {
                StopEffect();
                // CreateBullet();
            }
            return; 
        }
    }
    
    public void Init()
    {

    }
    // alpha �� ���� ���� 

    [ContextMenu("��ƼŬ ����")]
    public void StartEffect()
    {
        gameObject.SetActive(true);
        EffectSystem.Play();
        _isPlay = true; 
    }

    [ContextMenu("��ƼŬ ����")]
    public void StopEffect()
    {
        EffectSystem.Stop();
        _isPlay = false;
        _timer = 0f;

        FadeOut(); 
    }

    /// <summary>
    /// alpha�� ���������� 
    /// </summary>
    private void FadeOut()
    {
        Debug.Log("���̵�ƿ�"); 
        _mat.DOFade(0, 0.2f);
    }

    public override void Reset()
    {
        StopEffect(); 
    }
}
