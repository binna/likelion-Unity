using UnityEngine;

namespace Cat
{
    public class CatController : MonoBehaviour
    {
        [SerializeField] 
        private SoundManager soundManager;

        [SerializeField] 
        private Transform hpRoot;

        [SerializeField] 
        private UIManager uiManager;

        private readonly Vector3 InitPosition = new(0, 5f, 0);
        
        private const float LINEAR_VELOCITY_Y_LIMIT = 10.5f;
        private const float JUMP_POWER = 7f;
        
        private Rigidbody2D _catRigidbody;
        private Animator _catAnimator;
        private GameObject[] _hps;

        private int _lifeCount;
        
        void Awake()
        {
            _catRigidbody = GetComponent<Rigidbody2D>();
            _catAnimator = GetComponent<Animator>();
            
            _hps = new GameObject[hpRoot.childCount];
            for (int i = 0; i < hpRoot.childCount; i++)
            {
                _hps[i] = hpRoot.GetChild(i).gameObject;
            }
        }
      
        private void OnEnable()
        {
            CreateGame();
        }

        void Update()
        {
            if (transform.position.y < -3f)
            {
                transform.localPosition = InitPosition; 
                return;
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                soundManager.OnJumpSound();
                
                _catAnimator.SetTrigger(Define.AnimationParameter.JUMP);
                _catRigidbody.AddForceY(JUMP_POWER, ForceMode2D.Impulse);

                // 자연스러운 점프를 위한 속도 제한
                if (_catRigidbody.linearVelocityY > LINEAR_VELOCITY_Y_LIMIT)
                    _catRigidbody.linearVelocityY = LINEAR_VELOCITY_Y_LIMIT;
            }

            var catRotation = transform.eulerAngles;
            catRotation.z = _catRigidbody.linearVelocityY * 2.5f;
            transform.eulerAngles = catRotation;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag(Define.TagType.APPLE))
            {
                other.gameObject.SetActive(false);
                other.transform.parent.GetComponent<ItemController>().PlayEffect();
                
                GameManager.PlusScore();
                soundManager.OnGainItemSound();
            }
        }
        
        private void OnCollisionEnter2D(Collision2D other)
        {
            switch (other.gameObject.tag)
            {
                case Define.TagType.PIPE:
                    transform.localPosition = InitPosition;
                    
                    soundManager.OnCollisionSound();
                    
                    _hps[_lifeCount].SetActive(false);
                    _lifeCount++;
                    
                    if (_lifeCount >= 5)
                    {
                        soundManager.SetOutroSound();
                        uiManager.OuterUI(GameManager.GetRecordText());
                    }
                    return;
                case Define.TagType.GROUND:
                    _catAnimator.SetTrigger(Define.AnimationParameter.GROUND);
                    return;
            }
        }

        public void CreateGame()
        {
            transform.localPosition = Vector3.zero; 
            soundManager.SetBGMSound();
            
            _lifeCount = 0;
            
            for (int i = 0; i < hpRoot.childCount; i++)
            {
                _hps[i].gameObject.SetActive(true);
            }
        }
    }
}