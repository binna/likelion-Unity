using System.Collections;
using UnityEngine;

namespace Knight.Adventure
{
    public class Goblin : MonsterCore
    {
        private const float TRACE_DISTANCE = 5f;
        private const float ATTACK_DISTANCE = 1.5f;
        
        private float _timer;
        private float _idleTime; 
        private float _patrolTime;
        
        private bool _isAttack;
        
        private void Start()
        {
            Init(10f, 3f, 2f);
        }

        protected override void Idle()
        {
            _timer += Time.deltaTime;

            // 정찰
            if (_timer >= _idleTime)
            {
                // 방향 랜덤
                var scaleX = Random.Range(0, 2) == 1? 1 : -1;
                transform.localScale = new Vector3(scaleX, 1, 1);
                
                _timer = 0f;
                _patrolTime = Random.Range(1f, 5f);
                _animator.SetBool("isRun", true);
                
                ChangeState(State.PATROL);
            }
            
            // 추격
            if (_toPlayerDistance <= TRACE_DISTANCE && _isTrace)
            {
                _timer = 0f;
                _animator.SetBool("isRun", true);
                
                ChangeState(State.TRACE);
            }
        }
        
        protected override void Patrol()
        {
            transform.position += Vector3.right * transform.localScale.x * speed * Time.deltaTime;
            
            _timer += Time.deltaTime;
            
            // 정찰 시간 끝
            if (_timer >= _patrolTime)
            {
                _timer = 0f;
                _idleTime = Random.Range(1f, 5f);
                _animator.SetBool("isRun", false);
                
                ChangeState(State.IDLE);
                return;
            }
            
            // 추격
            if (_toPlayerDistance <= TRACE_DISTANCE && _isTrace)
            {
                _timer = 0f;
                ChangeState(State.TRACE);
            }
        }
        
        protected override void Trace()
        {
            var toMonster = (_playerTransform.position - transform.position).normalized;
            var scaleX = toMonster.x < 0f ? -1f : 1f;
            
            transform.position += Vector3.right * scaleX * speed * Time.deltaTime;
            transform.localScale = new Vector3(scaleX, 1f, 1f);
            
            if (_toPlayerDistance > TRACE_DISTANCE)
            {
                _animator.SetBool("isRun", false);
                ChangeState(State.IDLE);
                return;
            }
            
            if (_toPlayerDistance < ATTACK_DISTANCE)
            {
                ChangeState(State.ATTACK);
            }
        }
        
        protected override void Attack()
        {
            if (!_isAttack)
                StartCoroutine(AttackRoutine());
        }

        IEnumerator AttackRoutine()
        {
            _isAttack = true;
            _animator.SetTrigger("Attack");
            yield return new WaitForSeconds(1f);
            
            _animator.SetBool("isRun", false);

            yield return new WaitForSeconds(attackTime - 1f);
            _isAttack = false;
            ChangeState(State.IDLE);
        }
    }
}