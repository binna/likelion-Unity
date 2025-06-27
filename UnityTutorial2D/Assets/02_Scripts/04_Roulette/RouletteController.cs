using UnityEngine;

public class RouletteController : MonoBehaviour
{
    private float rotSpeed = 5.0f;
    private bool isStop;

    void Start()
    {
        rotSpeed = 0.0f;
    }

    void Update()
    {
        transform.Rotate(Vector3.forward * rotSpeed);   // Z축 기준으로 회전하는 기능
        //transform.Rotate(0f, 0f, rotSpeed);
        
        // 마우스 왼쪽 버튼을 눌렀을 때 회전하는 기능
        if (Input.GetMouseButtonDown(0))
        {
            rotSpeed = 5.0f;
        }
        
        // 키도드 스페이스 버튼을 눌렀을 때 -> 1번 실행
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isStop = true;
        }

        if (isStop == true)
        {
            rotSpeed *= 0.98f;      // 현재 속도에서 특정 값만큼 줄이는 기능

            if (rotSpeed < 0.01f)
            {
                rotSpeed = 0.0f;
                isStop = false;
            }
        }
    }
}