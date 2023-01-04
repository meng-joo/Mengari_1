using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    [SerializeField]
    private Bullet _bulletPrefab;
    [SerializeField]
    private ParticleEffect _effect; 
    [SerializeField]
    private float _bulletHeight = 0.3f; // �Ѿ� ������ ���� 
    private Vector3 _screenSize;
    private Camera _mainCam;

    private Ray _ray;
    private LayerMask _layerMask;

    private void Awake()
    {
        _mainCam = Camera.main;
    }
    private void Start()
    {
        _layerMask = 1 << LayerMask.NameToLayer("Ground");
        _screenSize = new Vector3(Screen.width / 2, Screen.height / 6, 0);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            CreateBullet(); 
        }
    }
    /// <summary>
    /// �Ѿ� ����
    /// </summary>
    [ContextMenu("CreateBullet")]
    public void CreateBullet()
    {
        Debug.Log("�Ѿ� ����");
        Vector3 pos = Vector3.zero;
        RaycastHit hit;
        _ray = _mainCam.ScreenPointToRay(_screenSize);

        if (Physics.Raycast(_ray, out hit, 1000, _layerMask))
        {
            pos = hit.point;
            pos.y += _bulletHeight;
            Debug.DrawRay(_ray.origin, _ray.direction * 1000f, Color.red, 5f);
        }
        else
        {
            Debug.LogError("�Ѿ��� ������ �� �����ϴ�(Raycast NULL)");
            return;
        }
        //pos = _mainCam.ScreenToWorldPoint(new Vector3(_screenSize.x, _screenSize.y, 1));
        //Debug.Log(pos); 
        //pos.y = _bulletHeight;
        StartCoroutine(CreateBullet(pos)); 
    }

    private Bullet _curBullet; 

    IEnumerator CreateBullet(Vector3 bulletPos)
    {
        //ParticleEffect effect = PoolManager.Instance.Pop(PoolType.BulletCreateEffect) as ParticleEffect;"
        Bullet bullet = PoolManager.Instance.Pop(PoolType.Bullet) as Bullet;

        _curBullet = bullet; 

        bullet.SetPosAndRot(bulletPos, Vector3.up * 270);

        bullet.ParticleEffect.transform.position = bulletPos;
        bullet.ParticleEffect.StartEffect();

        yield return new WaitForSeconds(bullet.ParticleEffect.Duration);  

        Debug.Log(bullet.ParticleEffect.Duration);

        bullet.Rendering(true); 
        //Bullet bullet = Instantiate(_bulletPrefab, bulletPos, Quaternion.Euler(Vector3.right * 90));

        Draggable draggable;
        draggable = bullet.GetComponent<Draggable>();

        // �Ѿ� �巡�� �̺�Ʈ ��� 
        //draggable.beginDragEvent = () => { draggable.beginDragEvent = null; bullet.UpScale(); };
        draggable.endDragEvent = null;
        draggable.stayClickEvent = null;

        draggable.endDragEvent = () => {  bullet.MoveForward(); };
        draggable.endClickEvent = () => { Time.timeScale = 1f; };
        draggable.stayClickEvent = () => {  bullet.ScaleUp(); };
        //draggable.exitPointerEvent = () => { draggable.exitPointerEvent = null; bullet.MoveForward(); };
    }

    /// <summary>
    /// �Ѿ� ���� 
    /// </summary>
    public void ResetBullets()
    {
        PoolManager.Instance.Push(_curBullet); 
    }

}
