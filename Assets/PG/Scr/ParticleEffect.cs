using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    }
    private void Start()
    {
        //_duration =
        var a = _effectSystem.GetComponentsInChildren<ParticleSystem>();
        for(int i = 0; i < a.Length; i++)
        {
            float dur = a[i].main.duration; 
            if (_duration < dur)
            {
                _duration = dur; 
            }
        }
        _isPlay = false; 
    }

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
        }
    }
    
    public void Init()
    {

    }

    [ContextMenu("파티클 생성")]
    public void StartEffect()
    {
        gameObject.SetActive(true);
        EffectSystem.Play();
        _isPlay = true; 
    }

    [ContextMenu("파티클 종료")]
    public void StopEffect()
    {
        EffectSystem.Stop();
        _isPlay = false;
        _timer = 0f; 
    }

    public override void Reset()
    {
        StopEffect(); 
    }
}
