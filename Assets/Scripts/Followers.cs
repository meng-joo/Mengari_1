using UnityEngine;
using PathCreation;

public class Followers : MonoBehaviour
{
    public PathCreator pathCreator;
    public float speed = 5f;
    float distance;

    private bool isLazer = false;

    void Update()
    {
        distance += speed * Time.deltaTime;
        transform.position = pathCreator.path.GetPointAtDistance(distance);
        transform.rotation = pathCreator.path.GetRotationAtDistance(distance);
    }
}
