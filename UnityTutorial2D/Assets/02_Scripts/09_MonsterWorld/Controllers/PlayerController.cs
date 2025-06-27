using System.Collections;
using UnityEngine;

namespace MonsterWorld
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] 
        private float moveSpeed = 3f;
        
        [SerializeField]
        private Collider2D weaponCollider;

        private const int GROUND_LAYER = 10;
        
        private readonly float checkRadius = 0.2f;          // 발 밑 체크 반경
        private readonly float DownDropCooldown  = 0.3f;
        private readonly LayerMask platformLayer = 1 << 10;
        
        private Animator _animator;
        private Rigidbody2D _rigidbody;
        private Collider2D _collider;

        private float _hp;
        
        private float _horizontal;
        private float _rawHorizontal;

        private bool _isAttacking;
        private bool _isJumping;
        
        private bool _hasHitMonster;

        private bool _isDropping = false;
        
        void Awake()
        {
            _animator = GetComponent<Animator>();
            _rigidbody = GetComponent<Rigidbody2D>();
            _collider = GetComponent<Collider2D>();
        }

        void Update()
        {
            Move();
            Attack();
            Jump();
            DownDrop();
        }

        private void Move()
        {
            _horizontal = Input.GetAxis("Horizontal");

            _rawHorizontal = Input.GetAxisRaw("Horizontal");
            
            bool isMoving = _rawHorizontal != 0f;
            _animator.SetBool("isRunning", isMoving);

            var dir = new Vector3(_horizontal, 0, 0).normalized;
            transform.position += dir * moveSpeed * Time.deltaTime;

            Vector3 position = transform.position;
            position.x = Mathf.Clamp(position.x, -8f, 8f);
            transform.position = position;
            
            if (_rawHorizontal != 0f)
            {
                Vector3 scale = transform.localScale;
                
                // 이 캐릭터가 실제로 어디로 움직이고 있는지 → 그 부호만 뽑아서 방향 Flip에 사용
                scale.x = Mathf.Sign(_rawHorizontal) * Mathf.Abs(scale.x);
                
                transform.localScale = scale;
            }
        }

        private void Attack()
        {
            if (Input.GetKeyDown(KeyCode.Space) && !_isAttacking)
            {
                StartCoroutine(AttackRoutine());
            }
        }

        private IEnumerator AttackRoutine()
        {
            _animator.SetTrigger("Attack");
            
            yield return null;
            
            _isAttacking = true;
            weaponCollider.enabled = true;
            AnimatorStateInfo info = _animator.GetCurrentAnimatorStateInfo(0);
            
            yield return new WaitForSeconds(info.length);
            
            _isAttacking = false;
            _hasHitMonster = false;
            
            weaponCollider.enabled = false;
        }

        private void Jump()
        {
            if (!_isJumping 
                && (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)))
            {
                _rigidbody.AddForceY(10, ForceMode2D.Impulse);
            }
        }
        
        void DownDrop()
        {
            if (Input.GetKeyDown(KeyCode.S) 
                || Input.GetKeyDown(KeyCode.DownArrow))
            {
                Collider2D platform = Physics2D.OverlapCircle(
                    transform.position + Vector3.down * 0.7f,
                    checkRadius,
                    platformLayer
                );

                if (platform != null)
                    StartCoroutine(DownDropRoutine(platform));
            }
        }

        IEnumerator DownDropRoutine(Collider2D platformCollider)
        {
            Physics2D.IgnoreCollision(_collider, platformCollider, true);
            _rigidbody.AddForce(Vector2.down * 3f, ForceMode2D.Impulse);
            yield return new WaitForSeconds(DownDropCooldown);
            Physics2D.IgnoreCollision(_collider, platformCollider, false);
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (_isAttacking && _hasHitMonster == false)
            {
                BaseMonster monster = other.GetComponent<BaseMonster>();
                if (monster != null)
                {
                    _hasHitMonster = true;
                    StartCoroutine(monster.Hit(1));
                }
            }
        }
        
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Ground"))
            {
                if (_isJumping)
                {
                    _isJumping = false;
                    _animator.SetBool("isJumping", _isJumping);
                }
            }
        }

        private void OnCollisionExit2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Ground"))
            {
                if (!_isJumping)
                {
                    _isJumping = true;
                    _animator.SetBool("isJumping", _isJumping);
                }
            }
        }
    }
}