using UnityEngine;

namespace Cat
{
    public class ColliderEvent : MonoBehaviour
    {
        [SerializeField]
        private GameObject gameOver;
        
        [SerializeField]
        private SoundManager soundManager;
        
        private readonly Vector3 _initPosition = new(-651.52f, -617.3885f, 11.36894f);

        public void CreateGame()
        {
            transform.localPosition = _initPosition; 
        }

        void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                soundManager.OffSound();
                gameOver.SetActive(true);
            }
        }
    }
}