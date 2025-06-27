using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    public SpriteRenderer[] renderers;
    
    public float moveSpeed;
    private float _horizontal;
    private readonly float _jumpPower = 10f;

    private bool _isGround;
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        renderers = GetComponentsInChildren<SpriteRenderer>(true);
    }

    void Update()
    {
        _horizontal = Input.GetAxis("Horizontal");

        Jump();
    }

    void FixedUpdate()
    {
        Move();
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        _isGround = true;
         
        renderers[2].gameObject.SetActive(false);
    }
    
    void OnCollisionExit2D(Collision2D other)
    {
        _isGround = false;
        
        renderers[0].gameObject.SetActive(false);
        renderers[1].gameObject.SetActive(false);
        renderers[2].gameObject.SetActive(true);
    }


    /// <summary>
    /// 캐릭터 움직임에 따라 이미지의 Flip 상태가 변하는 기능
    /// </summary>
    private void Move()
    {
        if (!_isGround)
            return; 
        
        if (_horizontal != 0)
        {
            // 움직일 때
            renderers[0].gameObject.SetActive(false);
            renderers[1].gameObject.SetActive(true);
            
            _rigidbody.linearVelocityX = _horizontal * moveSpeed;     // 물리적인 이동
            
            if (_horizontal > 0)
            {
                renderers[0].flipX = false;
                renderers[1].flipX = false;
                renderers[2].flipX = false;
                return;
            }
            
            renderers[0].flipX = true;
            renderers[1].flipX = true;
            renderers[2].flipX = true;
            return;
        }
        
        if (_horizontal == 0)
        {
            // 움직이지 않을때
            renderers[0].gameObject.SetActive(true);
            renderers[1].gameObject.SetActive(false);
        }
    }
    
    private void Jump()
    {
        // 물리적인 연속적인 힘이 아니다
        // 물리적인 순간적인 힘이다
        // 따라서 FixedUpdate에 넣으면, Jump 키가 안 먹는다
        if (Input.GetButtonDown("Jump"))
        {
            _rigidbody.AddForceY(_jumpPower, ForceMode2D.Impulse);
        }
    }
}
