using UnityEngine;

public class CatController : MonoBehaviour
{
    private Rigidbody2D catRigidbody;

    public float jumpPower = 10.0f;
    public int jumpCount = 0;
    //public bool isGround;
    
    void Start()
    {
        catRigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // 스페이스 키 입력
        //if (Input.GetKeyDown(KeyCode.Space) && isGround)
        if (Input.GetKeyDown(KeyCode.Space) && jumpCount < 2)
        {
            catRigidbody.AddForceY(jumpPower, ForceMode2D.Impulse);
            jumpCount++;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            jumpCount = 0;
            //isGround = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        // if (other.gameObject.CompareTag("Ground"))
        // {
        //     isGround = false;
        // }
    }
}
