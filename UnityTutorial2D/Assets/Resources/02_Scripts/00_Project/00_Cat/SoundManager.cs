using UnityEngine;

namespace Cat
{
    public class SoundManager : MonoBehaviour
    {
        public AudioSource audioSource;
        public AudioClip bgmClip;
        public AudioClip jumpClip;

        public void SetBGMSound()
        {
            audioSource.clip = bgmClip;         // 오디오 소스에 사운드 파일 설정
            audioSource.playOnAwake = true;     // 시작할 때 자동 재생
            audioSource.loop = true;            // 반복 기능
            audioSource.volume = 0.1f;          // 소리 음량
            
            audioSource.Play();                 // 시작
        }

        public void OnJumpSound()
        {
            audioSource.PlayOneShot(jumpClip);
        }

        void Start()
        {
        
        }

        void Update()
        {
        
        }
    }
}