using UnityEngine;

namespace Cat
{
    public class SoundManager : MonoBehaviour
    {
        [SerializeField]
        private AudioSource audioSource;
        
        [SerializeField]
        private AudioClip bgmClip;
        
        [SerializeField]
        private AudioClip jumpClip;
        
        [SerializeField]
        private AudioClip introClip;

        void Start()
        {
            SetIntroSound();
        }
        
        public void SetIntroSound()
        {
            audioSource.clip = introClip;
            audioSource.playOnAwake = true;
            audioSource.loop = true;
            audioSource.volume = 0.1f;
            
            audioSource.Play();
        }

        public void SetBGMSound()
        {
            audioSource.clip = bgmClip;
            audioSource.playOnAwake = true;
            audioSource.loop = true;
            audioSource.volume = 0.1f;
            
            audioSource.Play();
            
            
        }

        public void OnJumpSound()
        {
            audioSource.PlayOneShot(jumpClip);
        }

        public void OffSound()
        {
            audioSource.Stop();
        }
    }
}