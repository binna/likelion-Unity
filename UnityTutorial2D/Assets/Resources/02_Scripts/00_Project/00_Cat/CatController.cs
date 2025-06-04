using UnityEngine;
using Cat;


public class CatController : MonoBehaviour
{
    public SoundManager soundManager;
    
    private Rigidbody2D _catRigidbody;
    private Animator _catAnimator;

    public float jumpPower = 10.0f;
    public int jumpCount = 0;
    //public bool isGround;
    
    void Start()
    {
        _catRigidbody = GetComponent<Rigidbody2D>();
        _catAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && jumpCount < 2)
        {
            _catAnimator.SetTrigger("Jump");
            _catAnimator.SetBool("isGround", false);
            _catRigidbody.AddForceY(jumpPower, ForceMode2D.Impulse);
            jumpCount++;
            
            soundManager.OnJumpSound();
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            _catAnimator.SetBool("isGround", true);
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
