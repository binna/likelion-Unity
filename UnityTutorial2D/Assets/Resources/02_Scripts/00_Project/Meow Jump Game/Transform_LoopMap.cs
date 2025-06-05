using UnityEngine;

// 트랜스폼 이동으로 무한 배경 구현
// 픽셀 크랙이 발생할 수 있으나, 대략적인 느낌만 보기 위한 임시 예제
// public class Transform_LoopMap : MonoBehaviour
// {
//     public float moveSpeed = 3.0f;
//
//     public Vector3 returnPos = new(30.0f, 1.5f, 0f);
//
//     void Update()
//     {
//         // 배경 왼쪽으로 이동하는 기능
//         //transform.position += Vector3.left * moveSpeed * Time.deltaTime;
//         
//         // Time.deltaTime : 프레임 간의 시간 간격 (매 프레임마다 달라지며, 성능에 따라 값이 달라질 수 있음)
//         // Time.fixedDeltaTime : 고정된 시간 간격 (물리 연산용, 항상 일정한 값)
//         
//         // transform.position += Vector3.left * moveSpeed * Time.deltaTime;
//         // Debug.Log(Time.deltaTime);
//         transform.position += Vector3.left * moveSpeed * Time.fixedDeltaTime;
//         Debug.Log(Time.fixedDeltaTime);
//
//         if (transform.position.x <= -30.0f)     // 이미지의 x축 값이 -30을 넘는 순간
//         {
//             transform.position = returnPos;     // 다시 사용하기 위해서 오른쪽 x = 30으로 초기화
//         }
//     }
// }
// 픽셀 크랙(Pixel Gap) 정의
// 두 스프라이트나 타일 사이에 생기는 실선 또는 틈 현상

// 2D 게임에서 자주 발생함
// 위의 예제는 소수점 단위로 이동하기 때문에 30.0f와 같은 정수 위치에 정확히 맞지 않아 발생함
// 격자에 정확히 맞거나 도형들이 딱 맞닿아 겹쳐지면 틈이 보이지 않지만,
// 두 도형 사이에 조금이라도 간격(차이)이 생기면 틈이 보이게 됨
///////////////////////////////////////////////////////////////////////////////////////////////////////////
public class Transform_LoopMap : MonoBehaviour
{
    public float moveSpeed = 3.0f;
    //public Vector3 returnPos;
    public float returnPosX = 15.0f;
    public float randomPosY;
    
    void Update()
    {
        transform.position += Vector3.left * moveSpeed * Time.deltaTime;
        if (transform.position.x <= -returnPosX)
        {
            // transform.position = returnPos;
            
            randomPosY = Random.Range(-8.0f, -3.0f);
            transform.position = new Vector3(returnPosX, randomPosY, 0);
        }
    }
}