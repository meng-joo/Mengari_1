using UnityEngine;
using PathCreation;
using System.Collections;

public class Followers : MonoBehaviour
{
    public PathCreator pathCreator;
    public float speed;
    public float time;
    float distance;

    private bool isLazer = false;

    private void Start()
    {
        WallSystem.stageLevel = 0;
        StartCoroutine(Timer());
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("스테이지 레벨 : "+WallSystem.stageLevel++);
            speed = Mathf.Log(WallSystem.stageLevel * 80f);
            Debug.Log("속도 : "+speed);

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


    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(time);
        if(other.CompareTag("Bullet"))
        {
            WallSystem.stageLevel++;
        }
    }
}
