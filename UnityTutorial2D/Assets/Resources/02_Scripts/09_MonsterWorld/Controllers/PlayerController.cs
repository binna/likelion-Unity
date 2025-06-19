using System;
using UnityEngine;

namespace MonsterWorld
{
    public class PlayerController : MonoBehaviour
    {
        private Animator _animator;

        [SerializeField] 
        private float moveSpeed = 3f;
        
        [SerializeField]
        private Collider2D weaponCollider;

        private float _hp;
        
        private float _horizontal;
        private float _vertical;
        
        private float _rawHorizontal;
        private float _rawVertical;

        private bool _isAttacking;
        
        void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        void Update()
        {
            Move();
            Attack();
        }

        void Move()
        {
            _horizontal = Input.GetAxis("Horizontal");
            _vertical = Input.GetAxis("Vertical");

            _rawHorizontal = Input.GetAxisRaw("Horizontal");
            _rawVertical = Input.GetAxisRaw("Vertical");
            
            bool isMoving = _rawHorizontal != 0f || _rawVertical != 0f;
            _animator.SetBool("isRunning", isMoving);

            var dir = new Vector3(_horizontal, _vertical, 0).normalized;
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

        void Attack()
        {
            if (Input.GetKeyDown(KeyCode.Space) && !_isAttacking)
            {
                _animator.SetTrigger("Attack");
                weaponCollider.enabled = true;
                _isAttacking = true;
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            Debug.Log("Trigger Enter");
            if (_isAttacking)
            {
                BaseMonster monster = other.GetComponent<BaseMonster>();
                if (monster != null)
                {
                    StartCoroutine(monster.Hit(1));
                    Debug.Log("데미지 먹음");
                }
                
                _isAttacking = false;
                weaponCollider.enabled = false; 
            }
        }
    }
}