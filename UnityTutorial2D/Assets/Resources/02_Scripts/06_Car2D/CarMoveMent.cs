using UnityEngine;

public class CarMoveMent : MonoBehaviour
{
    public float moveSpeed = 3.0f;
    public Rigidbody2D CarRigidbody2D;

    private float horizontal;
    
    // 성능에 따라 다른 프레임으로 실행되는 유니티 기본 함수
    // 키 입력
    private void Start()
    {
        CarRigidbody2D.linearVelocity = Vector2.one;
        
    }

    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");     // X값
        
        // Transform 이동
        transform.position += Vector3.right * horizontal * moveSpeed * Time.deltaTime;
         }

    // 고정 프레임으로 실행되는 유니티 기본 함수
    // 물리적인 작용
    private void FixedUpdate()
    {
        // Rigidbody의 속도를 활용한 이동
        //CarRigidbody2D.linearVelocityX = horizontal * moveSpeed;
        //CarRigidbody2D.linearVelocityY = 0;
    }

    // 1번 실행
    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("Collision Enter " + other.gameObject.name);
        //other.gameObject.SetActive(false);
    }

    // 계속 실행
    private void OnCollisionStay2D(Collision2D other)
    {
        Debug.Log("Collision Stay " + other.gameObject.name);
    }

    // 1번 실행
    private void OnCollisionExit2D(Collision2D other)
    {
        Debug.Log("Collision Exit " + other.gameObject.name);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Trigger Enter " + other.gameObject.name);
    }

    void OnTriggerStay2D(Collider2D other)
    {
        Debug.Log("Trigger Stay " + other.gameObject.name);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("Trigger Exit " + other.gameObject.name);
    }
}