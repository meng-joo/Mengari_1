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
    private float _bulletSpeed = 50;

    public ParticleEffect ParticleEffect => _particleEffect;

    private Vector3 _originScale;

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

    public void UpScale()
    {
        DOTween.KillAll();
        transform.DOScale(0.4f, 0.3f);
    }

    // 총알 앞으로 이동 
    public void MoveForward()
    {
        Debug.Log("앞으로 이동"); 
        _rigid.AddForce(Vector3.left * _bulletSpeed,ForceMode.Impulse);
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
        transform.position = Vector2.one;
        Rendering(false); 
    }
    // 날아갈때 렌더러 이펙트, 화면으로 빨려들어가는 듯한 효과(진동, 아웃포커싱) 

}
