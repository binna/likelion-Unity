using UnityEngine;

public class Movement : MonoBehaviour
{
    private float moveSpeed = 5.0f;

    public static int coinCount;

    private void Update()
    {
        // 부드럽게 증감하는 값
        float horizontal = Input.GetAxis("Horizontal");     // X
        float vertical = Input.GetAxis("Vertical");         // Z
        
        // 크기(거리) 없이 방향 정보만 담고 있는 값
        Vector3 direction = new Vector3(horizontal, 0, vertical);
        //Debug.Log($"현재 입력 : {direction}");
        
        // 방향 벡터에 속도와 시간을 곱해서 위치 이동
        // 방향(direction)
        // 속도(moveSpeed) * 시간(Time.deltaTime) = 위치
        transform.position += direction * moveSpeed * Time.deltaTime;
        
        // 백터는 방향과 크기를 함께 가진 값으로
        // 위치(transform.position) * 방향(direction) = 최종 위치 백터
        // LookAt 함수를 통해 해당 방향으로 오브젝트가 바라보도록 회전 처리
        transform.LookAt(transform.position + direction);
    }
}