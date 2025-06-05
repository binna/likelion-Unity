using UnityEngine;

namespace Cat
{
    public class CatController : MonoBehaviour
    {
        [SerializeField] 
        private SoundManager soundManager;

        private readonly float _limitY = 8.5f;
        private readonly float _jumpPower = 7.0f;
        private readonly Vector3 _initPosition = new(-659.76f, -616.8785f, 11.36894f);
        
        private const int MaxJumpCnt = 5;
        
        private Rigidbody2D _catRigidbody;
        private Animator _catAnimator;

        private int _jumpCount;

        void Start()
        {
            _catRigidbody = GetComponent<Rigidbody2D>();
            _catAnimator = GetComponent<Animator>();
            
            CreateGame();
        }
        
        public void CreateGame()
        {
            soundManager.SetBGMSound();
            transform.localPosition = _initPosition; 
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space) && _jumpCount < MaxJumpCnt)
            {
                soundManager.OnJumpSound();
                
                _catAnimator.SetTrigger("Jump");
                _catRigidbody.AddForceY(_jumpPower, ForceMode2D.Impulse);
                _jumpCount++;

                if (_catRigidbody.linearVelocityY > _limitY)
                    _catRigidbody.linearVelocityY = _limitY;
            }
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Ground"))
            {
                _catAnimator.SetTrigger("Ground");;
                _jumpCount = 0;
            }
        }
    }
}