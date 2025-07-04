using UnityEngine;

namespace Knight.Adventure
{
    public abstract class MonsterCore : MonoBehaviour
    {
        public enum State
        {
            IDLE,
            PATROL,
            TRACE,
            ATTACK
        }
        
        [SerializeField]
        private State state = State.IDLE;

        [SerializeField] 
        protected float hp;
        
        [SerializeField] 
        protected float speed;
        
        [SerializeField]
        public float attackTime;

        protected Animator _animator;
        protected Rigidbody2D _rigidbody;
        
        protected Transform _playerTransform;
        
        protected float _toPlayerDistance;
        
        protected bool _isTrace;
        
        protected abstract void Idle();
        protected abstract void Patrol();
        protected abstract void Trace();
        protected abstract void Attack();

        protected void ChangeState(State newState)
        {
            state = newState;
        }
        
        protected void Init(float hp, float speed, float attackTime)
        {
            this.hp = hp;
            this.speed = speed;
            this.attackTime = attackTime;

            _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
            
            _animator = GetComponent<Animator>();
            _rigidbody = GetComponent<Rigidbody2D>();
        }
       
        private void Update()
        {
            // 두 위치(몬스터 -> 플레이어) 간의 벡터(방향 + 거리 포함)
            // 즉, 플레이어 위치를 향한 백터
            var toPlayer = transform.position - _playerTransform.position;
            
            // 거리 추출
            // 항상 양수
            _toPlayerDistance = toPlayer.magnitude;

            // 정규화된 방향 벡터(길이는 1, 방향 정보만 유지)
            var toPlayerDirection = toPlayer.normalized;
            
            // 몬스터가 바라보는 방향 계산
            // localScale.x가 양수면 오른쪽, 음수면 왼쪽을 바라보는 것으로 판단
            // Vector3.right는 +X축 방향
            var moveDirection = Vector3.right * transform.localScale.x;
            
            float dotValue = Vector3.Dot(moveDirection, toPlayerDirection);
            
            // 몬스터 기준, 플레이어가 시야각 안에 있는지 확인
            _isTrace = dotValue < -0.5f;
            
            switch (state)
            {
                case State.IDLE:
                    Idle();
                    break;
                case State.PATROL:
                    // 정찰 기능
                    Patrol();
                    break;
                case State.TRACE:
                    // 추적 기능
                    Trace();
                    break;
                case State.ATTACK:
                    Attack();
                    break;
            }
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Ground") 
                || other.gameObject.CompareTag("Player"))
                return;
            
            var scaleX = transform.localScale.x * -1;
            transform.localScale = new Vector3(scaleX, 1f, 1f);
        }
    }
}