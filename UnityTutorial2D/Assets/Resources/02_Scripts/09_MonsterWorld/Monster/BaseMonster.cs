using System.Collections;
using UnityEngine;

namespace MonsterWorld
{
    public abstract class BaseMonster : MonoBehaviour
    {
        private SpriteRenderer _spriteRenderer;
        private Animator _animator;

        [SerializeField] 
        protected float hp = 3f;

        [SerializeField]
        protected float moveSpeed = 3f;

        private SpawnManager _spawnManager;
        private int _direction = 1;
        private bool _isMoving = true;
        private bool _isTakingHit;

        protected abstract void Init();
        
        public void SetFacingDirection(bool isFaceLeft)
        {
            if (isFaceLeft)
            {
                _direction = -1;
                _spriteRenderer.flipX = true;
                return;
            }
            
            _direction = 1;
            _spriteRenderer.flipX = false;
        }
        
        public IEnumerator Hit(float damage)
        {
            if (_isTakingHit)
                yield break;

            _isTakingHit = true;
            _isMoving = false;

            hp -= damage;

            if (hp <= 0)
            {
                _animator.SetTrigger("Death");
                
                // 아이템 생성
                _spawnManager.DropItem(transform.position);

                yield return new WaitForSeconds(1f);
                Destroy(gameObject);

                yield break;
            }

            _animator.SetTrigger("Hit");

            yield return new WaitForSeconds(0.65f);
            _isTakingHit = false;
            _isMoving = true;
        }

        void Awake()
        {
            _spawnManager = FindFirstObjectByType<SpawnManager>();
            
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _animator = GetComponent<Animator>();

            Init();
        }

        void OnMouseDown()
        {
            StartCoroutine(Hit(1));
        }

        void Update()
        {
            Move();
        }

        void Move()
        {
            if (!_isMoving)
                return;

            transform.position += Vector3.right * _direction * moveSpeed * Time.deltaTime;

            if (transform.position.x > 8f)
            {
                SetFacingDirection(true);
            }
            else if (transform.position.x < -8f)
            {
                SetFacingDirection(false);
            }
        }
    }
}