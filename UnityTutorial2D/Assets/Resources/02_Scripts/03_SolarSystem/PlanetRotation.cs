using UnityEngine;

public class PlanetRotation : MonoBehaviour
{
    public Transform targetPlanet;
    public float rotSpeed = 30.0f;          // 자전 속도
    public float revolutionSpeed = 100.0f;  // 공전 속도
    public bool isRevolution;
    
    void Update()
    {
        // 자전
        transform.Rotate(Vector3.up, rotSpeed * Time.deltaTime);

        if (isRevolution)       // 만약 공전을 한다면
        {
            // 공전
            transform.RotateAround(targetPlanet.position, Vector3.up, rotSpeed * Time.deltaTime);
        }

    }
}
