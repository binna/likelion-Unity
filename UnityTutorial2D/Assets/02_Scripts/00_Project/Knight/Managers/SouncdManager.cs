using UnityEngine;

namespace Knight
{
    public class SouncdManager : MonoBehaviour
    {
        private AudioSource _audio;
        private AudioSource _eventAudio;

        [SerializeField]
        private AudioClip[] clips;

        private void Awake()
        {
            _audio = GetComponent<AudioSource>();
            
            BGMSoundPlay("01 Get Ready for Adventure");
        }

        public void BGMSoundPlay(string clipName)
        {
            foreach (var clip in clips)
            {
                if (clip.name == clipName)
                {
                    _audio.clip = clip;
                    _audio.Play();
                    return;
                }
            }
            Debug.Log($"{clipName}을 찾지 못했습니다.");
        }

        public void EventSoundPlay(string clipName)
        {
            foreach (var clip in clips)
            {
                if (clip.name == clipName)
                {
                    _audio.PlayOneShot(clip);
                    return;
                }
            }
            Debug.Log($"{clipName}을 찾지 못했습니다.");
        }
    }
}