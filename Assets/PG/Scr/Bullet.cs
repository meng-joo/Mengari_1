using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening; 
public class Bullet : PoolableMono
{
    private ParticleEffect _particleEffect;

    // 총알 크기 up
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
   
    // 총알 앞으로 이동 
    public void MoveForward()
    {
        Debug.Log("앞으로 이동"); 
        _rigid.AddForce(Vector3.forward * _bulletSpeed,ForceMode.Impulse);
    }

    // 총알 충돌시 삭제 (레이캐스트) 
    public void DestroyBullet()
    {
        this.gameObject.SetActive(false); 
    }

    // 총알 생성시 중앙 이펙트 있다가 생성 
    public void CreateBullet()
    {

    }
    // 날아갈때 렌더러 이펙트, 화면으로 빨려들어가는 듯한 효과(진동, 아웃포커싱) 


    public override void Reset()
    {
    }
}
