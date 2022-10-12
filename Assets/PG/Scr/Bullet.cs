using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening; 
public class Bullet : PoolableMono
{
    private ParticleEffect _particleEffect;

    // �Ѿ� ũ�� up
    private Rigidbody _rigid;
    private float _bulletSpeed = 100;
    private void Awake()
    {
        _rigid = GetComponent<Rigidbody>(); 
    }

    public void UpScale()
    {
        DOTween.KillAll();
        transform.DOScale(1.2f, 0.3f);
    }

    public void SetPosAndRot(Vector3 pos, Vector3 angle)
    {
        transform.position = pos;
        transform.eulerAngles = angle; 
    }
   
    // �Ѿ� ������ �̵� 
    public void MoveForward()
    {
        Debug.Log("������ �̵�"); 
        _rigid.AddForce(Vector3.back * _bulletSpeed,ForceMode.Impulse);
    }

    // �Ѿ� �浹�� ���� (����ĳ��Ʈ) 
    public void DestroyBullet()
    {
        PoolManager.Instance.Push(this); 
    }

    // �Ѿ� ������ �߾� ����Ʈ �ִٰ� ���� 
    public void CreateBullet()
    {

    }
    // ���ư��� ������ ����Ʈ, ȭ������ �������� ���� ȿ��(����, �ƿ���Ŀ��) 


    public override void Reset()
    {
        transform.position = Vector2.one; 
    }
}
