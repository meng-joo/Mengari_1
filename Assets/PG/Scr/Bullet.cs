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

    // �Ѿ� ũ�� up
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

    // �Ѿ� ������ �̵� 
    public void MoveForward()
    {
        Debug.Log("������ �̵�"); 
        _rigid.AddForce(Vector3.left * _bulletSpeed,ForceMode.Impulse);
    }

    // �Ѿ� �浹�� ���� (����ĳ��Ʈ) 
    public void DestroyBullet()
    {
        PoolManager.Instance.Push(this); 
    }

    public void Rendering(bool isActive)
    {
        _meshRenderer.enabled = isActive;
        _capsuleCollider.enabled = isActive; 
    }

    // �Ѿ� ������ �߾� ����Ʈ �ִٰ� ���� 
    public void CreateBullet()
    {

    }

    public override void Reset()
    {
        transform.position = Vector2.one;
        Rendering(false); 
    }
    // ���ư��� ������ ����Ʈ, ȭ������ �������� ���� ȿ��(����, �ƿ���Ŀ��) 

}
