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

    private bool isLazer = false;
    private WallManager wallManager;    

    private void Start()
    {
        wallManager = GameObject.Find("Manager").GetComponent<WallManager>();
        StartCoroutine(Timer());
    }

    void Update()
    {

        speed = Mathf.Log(WallManager.stageLevel * 1.00001f);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            speed = Mathf.Log(WallManager.stageLevel * 80f);
            
        }
        distance += speed * Time.deltaTime;
        transform.localPosition = pathCreator.path.GetPointAtDistance(distance);
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
        //Debug.Log(time);
        if (collision.transform.CompareTag("Bullet"))
        {
            Debug.Log("‰çÀ½");
            WallManager.stageLevel++;
            wallManager.levelUpEvent?.Invoke();
        }
    }


}
