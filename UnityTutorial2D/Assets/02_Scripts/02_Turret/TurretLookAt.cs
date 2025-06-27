using UnityEngine;

// 플레이어를 바라보는 기능
public class TurretLookAt : MonoBehaviour
{
    public Transform player;
    public Transform turretHead;
    
    public Bullet bulletPrefab;     // 총알 프리팹
    public Transform firePos;       // 발사 위치

    public float timer;
    public float cooldownTime;
    
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        turretHead.LookAt(player);

        timer += Time.deltaTime;
        if (timer >= cooldownTime)
        {
            timer = 0.0f;
            Instantiate(bulletPrefab, firePos.position, firePos.rotation).firePos = firePos;
        }
    }
}