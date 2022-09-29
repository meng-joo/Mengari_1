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

    public void UpScale(bool isScaleUp)
    {
        DOTween.KillAll();
        if(isScaleUp == true)
        {
            transform.DOScale(1.2f, 0.3f);
            return;
        }
        transform.DOScale(1f, 0.3f);
    }
   
    // �Ѿ� ������ �̵� 
    public void MoveForward()
    {
        Debug.Log("������ �̵�"); 
        _rigid.AddForce(Vector3.forward * _bulletSpeed,ForceMode.Impulse);
    }

    // �Ѿ� �浹�� ���� (����ĳ��Ʈ) 
    public void DestroyBullet()
    {
        this.gameObject.SetActive(false); 
    }

    // �Ѿ� ������ �߾� ����Ʈ �ִٰ� ���� 
    public void CreateBullet()
    {

    }
    // ���ư��� ������ ����Ʈ, ȭ������ �������� ���� ȿ��(����, �ƿ���Ŀ��) 


    public override void Reset()
    {
    }
}
