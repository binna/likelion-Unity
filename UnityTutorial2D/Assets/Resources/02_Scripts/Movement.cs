using UnityEngine;

public class Movement : MonoBehaviour
{
    private float moveSpeed = 5.0f;
    
    private void Start()
    {
        transform.position = new Vector3(0, 0, 0);
    }

    private void Update()
    {
        // Input System (Old-Legacy)
        // 입력값에 대한 약속된 시스템
        // if (Input.GetKey(KeyCode.W))
        //     transform.position += Vector3.forward * (moveSpeed * Time.deltaTime);
        //     
        // if (Input.GetKey(KeyCode.S))
        //     transform.position += Vector3.back * (moveSpeed * Time.deltaTime);
        //     
        // if (Input.GetKey(KeyCode.A))
        //     transform.position += Vector3.left * (moveSpeed * Time.deltaTime);
        //     
        // if (Input.GetKey(KeyCode.D))
        //     transform.position += Vector3.right * (moveSpeed * Time.deltaTime);
        
        // 부드럽게 증감하는 값
        // float horizontal = Input.GetAxis("Horizontal");     // z
        // float vertical = Input.GetAxis("Vertical");         // x
        
        // 딱 떨어지는 값
        float horizontal = Input.GetAxisRaw("Horizontal");     // z
        float vertical = Input.GetAxisRaw("Vertical");         // x

        Vector3 direction = new Vector3(horizontal, 0, vertical);
        //Debug.Log($"현재 입력 : {direction}");
        
        transform.position += direction * moveSpeed * Time.deltaTime;
    }
}