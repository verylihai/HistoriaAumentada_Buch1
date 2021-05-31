using PathCreation;
using UnityEngine;

public class CapaFairyPathFollower : MonoBehaviour {
    public PathCreator pathCreator;
    public float speed = 40f;
    float distanceTraveled;
    public bool isPlaying = false;

    void Update() {
        if (isPlaying) {
            foreach (var item in pathCreator.path.localPoints) {
                Debug.Log(item);
            }
            isPlaying = false;
        }
        distanceTraveled += speed * Time.deltaTime;
        transform.position = pathCreator.path.GetPointAtDistance(distanceTraveled);
    }

    public Vector3 getTransformPosition() {
        return transform.position;
    }
}
