using UnityEngine;
using PathCreation;
using System.Collections;
using UnityEngine.Events;

public class Followers : MonoBehaviour
{
    public PathCreator pathCreator;
    public float speed;
    public float time;
    float distance;

    [SerializeField]
    private Vector3 _upYPos; // 조정할 좌표 

    private bool isLazer = false;
    private WallManager wallManager;


    private void Awake()
    {
        pathCreator ??= FindObjectOfType<PathCreator>(); 
    }

    private void Start()
    {
        wallManager = GameObject.Find("Manager").GetComponent<WallManager>();
        StartCoroutine(Timer());
    }

    void Update()
    {

        // speed = Mathf.Log(WallManager.stageLevel * 1.00001f);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            speed = Mathf.Log(WallManager.stageLevel * 80f);
        }

        distance += speed * Time.deltaTime;
        transform.localPosition = pathCreator.path.GetPointAtDistance(distance) + _upYPos;
        transform.localRotation = pathCreator.path.GetRotationAtDistance(distance);
    }

    IEnumerator Timer()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.01f);
            time += 0.01f;
        }
    }



    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(time);
        if (collision.transform.CompareTag("Bullet"))
        {
            Debug.Log("됬음");
            WallManager.stageLevel++;
            wallManager.levelUpEvent?.Invoke();
        }
    }


}
