using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float bulletSpeed = 100f;
    private float bulletDistance = 100f;
    
    public Transform firePos;       // 발사 위치
    
    void Update()
    {
        transform.position += transform.forward * bulletSpeed * Time.deltaTime;

        if (Math.Abs(Vector3.Dot(firePos.position, transform.position)) >= bulletDistance)
        {
            Destroy(gameObject);
        }
    }
}