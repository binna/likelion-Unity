using UnityEngine;

namespace Knight
{
    public class KnightController_Keyboard : MonoBehaviour
    {
        [SerializeField] 
        private float moveSpeed = 3f;

        [SerializeField] 
        private float jumpPower = 13f;

        [SerializeField] 
        private float atkDamage = 3f;
        
        private Animator _animator;
        private Rigidbody2D _rigidbody;

        private Vector3 _inputDir;
        
        private bool _isGround;
        private bool _isAttack;
        private bool _isCombo;

        void Start()
        {
            _animator = GetComponent<Animator>();
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void Update()       // 일반적인 작업
        {
            InputKeyboard();
            Jump();
            Attack();
            //WaitCombo();
        }

        private void FixedUpdate()  // 물리적인 작업
        {
            Move();
        }

        void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Ground"))
            {
                _animator.SetBool("isGround", true);
                _isGround = true;
            }
        }

        private void OnCollisionExit2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Ground"))
            {
                _animator.SetBool("isGround", false);
                _isGround = false;
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Monster"))
            {
                Debug.Log($"{atkDamage}로 공격");
            }
        }

        void InputKeyboard()
        {
            float horizontal = Input.GetAxisRaw("Horizontal");     // 좌, 우
            float vertical = Input.GetAxisRaw("Vertical");         // 상, 하

            _inputDir = new Vector3(horizontal, vertical, 0);
            
            _animator.SetFloat("PositionX", _inputDir.x);
            _animator.SetFloat("PositionY", _inputDir.y); 
        }

        void Move()
        {
            if (_inputDir.x != 0)
            {
                var scaleX = _inputDir.x > 0 ? 1f : -1f;
                transform.localScale = new Vector3(scaleX, 1, 1);
                
                _rigidbody.linearVelocityX = _inputDir.x * moveSpeed;
            }
        }

        void Jump()
        {
            if (Input.GetKeyDown(KeyCode.Space) && _isGround)
            {
                _animator.SetTrigger("Jump");
                _rigidbody.AddForceY(jumpPower, ForceMode2D.Impulse);
            }
        }

        void Attack()
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {

                if (!_isAttack)
                {
                    _isAttack = true;
                    atkDamage = 3f;
                    _animator.SetTrigger("Attack");
                    return;
                }
                
                _isCombo = true;
            }
        }

        // 공격 애니메이션에 이벤트로 연결
        public void WaitCombo()
        {
            if (_isCombo)
            {
                atkDamage = 5f;
                _animator.SetBool("isCombo", true);
                return;
            }
        
            _isAttack = false;
            _animator.SetBool("isCombo", false);
        }
        
        // 콤보 애니메이션에 이벤트로 연결
        public void EndCombo()
        {
            _isAttack = false;
            _isCombo = false;
            _animator.SetBool("isCombo", false);
        }
    }
}