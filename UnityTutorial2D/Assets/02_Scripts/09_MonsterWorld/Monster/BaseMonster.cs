using System.Collections;
using UnityEngine;

namespace MonsterWorld
{
    public abstract class BaseMonster : MonoBehaviour
    {
        public struct MonsterRoute
        {
            public float minX;
            public float maxX;
            public Vector3 spawnPosition;
        }
        
        private readonly MonsterRoute[] _routes =
        {
            new() { minX = -8, maxX = 8, spawnPosition = new Vector3(0f, -2.6f, 0f) },
            new() { minX = 2.3f, maxX = 8f, spawnPosition = new Vector3(5f, 0f, 0f) },
            new() { minX = -8f, maxX = 3f, spawnPosition = new Vector3(0f, 2.7f, 0f) },
            new() { minX = -3f, maxX = 8f, spawnPosition = new Vector3(0f, 5.1f, 0f) },
        };
        
        private SpriteRenderer _spriteRenderer;
        private Animator _animator;

        protected float hp;
        protected float moveSpeed;

        private SpawnManager _spawnManager;
        private MonsterRoute _monsterRoute;
        
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

                yield return new WaitForSeconds(1f);
                var position = transform.position;
                Destroy(gameObject);
                SpawnManager.DeadMonster();
                
                yield return new WaitForSeconds(1f);
                _spawnManager.DropItem(position);

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

            _monsterRoute = _routes[Random.Range(0, _routes.Length)];
            transform.position = _monsterRoute.spawnPosition;

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

            if (transform.position.x > _monsterRoute.maxX)
            {
                SetFacingDirection(true);
                return;
            }
            
            if (transform.position.x < _monsterRoute.minX)
            {
                SetFacingDirection(false);
            }
        }
    }
}