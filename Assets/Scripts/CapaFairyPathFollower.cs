using PathCreation;
using UnityEngine;

public class CapaFairyPathFollower : MonoBehaviour {
    public PathCreator pathCreator;
    public float speed = 40f;
    float distanceTraveled;
    public bool isPlaying = false;

    void Update() {
        distanceTraveled += speed * Time.deltaTime;
        transform.position = pathCreator.path.GetPointAtDistance(distanceTraveled);
    }

}
