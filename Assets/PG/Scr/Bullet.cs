using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening; 
public class Bullet : PoolableMono
{
    private MeshRenderer _meshRenderer;
    private CapsuleCollider _capsuleCollider;
    private ParticleEffect _particleEffect;

    // 총알 크기 up
    private Rigidbody _rigid;
    private float _bulletSpeed = 100;

    public ParticleEffect ParticleEffect => _particleEffect;

    private Vector3 _originScale = Vector3.one * 4;

    // 시간
    private bool _isTimer = false;
    private float _time = 0;
    private float _maxTime = 4f; 
    private void Start()
    {
        _originScale = transform.localScale;
    }
    private void Awake()
    {
        _rigid = GetComponent<Rigidbody>();
        _meshRenderer = GetComponent<MeshRenderer>();
        _capsuleCollider = GetComponent<CapsuleCollider>(); 

        _particleEffect = GetComponentInChildren<ParticleEffect>();
    }

    private void Update()
    {
        // Timer(); 
    }

    public void ScaleUp()
    {
        // 클릭중 
        Debug.Log("스케일업");
        transform.localScale = Vector3.Lerp(transform.localScale, _originScale * 1.4f, Time.deltaTime* 10f);
        Time.timeScale = 0.2f; 
    }
    public void UpScale()
    {
        DOTween.KillAll();
        transform.DOScale(_originScale.x * 1.2f, _originScale.y * 1.2f);
    }

    private Vector3 moveDir = new Vector3(-1,0.1f,0); 
    // 총알 앞으로 이동 
    public void MoveForward()
    {
        Debug.Log("앞으로 이동"); 
        _rigid.AddForce(moveDir * _bulletSpeed,ForceMode.Impulse);
        Time.timeScale = 1f;

        //StartTimer(); 
    }

    // 총알 충돌시 삭제 (레이캐스트) 
    public void DestroyBullet()
    {
        PoolManager.Instance.Push(this); 
    }

    public void Rendering(bool isActive)
    {
        _meshRenderer.enabled = isActive;
        _capsuleCollider.enabled = isActive; 
    }

    // 총알 생성시 중앙 이펙트 있다가 생성 
    public void CreateBullet()
    {

    }

    public override void Reset()
    {
        _time = 0;
        _isTimer = false; 
        transform.position = Vector3.one;
        transform.localScale = _originScale;
        _rigid.velocity = Vector3.zero;  
        Rendering(false); 
    }
    // 날아갈때 렌더러 이펙트, 화면으로 빨려들어가는 듯한 효과(진동, 아웃포커싱) 

    // 타이머 관련 
    private void StartTimer()
    {
        _time = 0;
        _isTimer = true; 
    }

    private void Timer()
    {
        if (_isTimer == false) return; 

        _time += Time.deltaTime;
        if (_time > _maxTime)
        {
            PoolManager.Instance.Push(this);
        }
    }
}
