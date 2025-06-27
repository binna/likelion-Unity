using UnityEngine;
using Random = UnityEngine.Random;

namespace Cat
{
    public class ItemController : MonoBehaviour
    {
        [SerializeField] 
        private GameObject pipe;

        [SerializeField] 
        private GameObject churu;

        [SerializeField] 
        private GameObject particle;

        private const float MOVE_SPEED_MIN = 3f;
        private const float RETURN_Y_MIN = -3f;
        private const float RETURN_Y_MAX = -8f;
        private const float START_POSITION_X = 45f;
        private const float BACKGROUND_END_X = -10f;
        private const float SPEED_UP_INTERVAL = 5f;

        private Define.ColliderType colliderType = Define.ColliderType.Pipe;
        
        private float _moveSpeedMax;
        private float _speedUpTimer;
        private float _randomPosY;
        private float _moveSpeed;
        
        private Vector3 _initPosition;

        private void Awake()
        {
            _initPosition = transform.localPosition;
        }

        private void OnEnable()
        {
            _moveSpeedMax = MOVE_SPEED_MIN;
            SetRandomSetting(_initPosition.x);
        }

        void Update()
        {
            transform.position += Vector3.left * _moveSpeed * Time.deltaTime;
            _speedUpTimer += Time.deltaTime;

            if (transform.position.x < BACKGROUND_END_X)
            {
                SetRandomSetting(START_POSITION_X);
            }
        }

        public void PlayEffect()
        {
            particle.SetActive(true);
        }

        private void SetRandomSetting(float positionX)
        {
            _randomPosY = Random.Range(RETURN_Y_MAX, RETURN_Y_MIN);
            transform.position = new Vector3(positionX, _randomPosY, 0);

            pipe.SetActive(false);
            churu.SetActive(false);
            particle.SetActive(false);

            colliderType = (Define.ColliderType)Random.Range(0, 3);
            
            _moveSpeed = Random.Range(MOVE_SPEED_MIN, _moveSpeedMax);
            
            if (_speedUpTimer > SPEED_UP_INTERVAL)
            {
                _speedUpTimer = 0;
                _moveSpeedMax += 1;
            }

            switch (colliderType)
            {
                case Define.ColliderType.Pipe:
                    pipe.SetActive(true);
                    break;
                case Define.ColliderType.Apple:
                    churu.SetActive(true);
                    break;
                case Define.ColliderType.Both:
                    pipe.SetActive(true);
                    churu.SetActive(true);
                    break;
            }
        }
    }
}