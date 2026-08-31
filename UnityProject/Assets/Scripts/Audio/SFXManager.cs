using UnityEngine;
using WiesnKrisn.Menus;

namespace WiesnKrisn.Audio
{
    public class SFXManager : MonoBehaviour
    {
        public static SFXManager Instance {get; private set;}
        
        private AudioSource _audioSource;

        private bool _playingRepeating;

        private void Awake()
        {
            Instance = this;
            
            _audioSource        = GetComponent<AudioSource>();
            float volume        = PlayerPrefs.GetFloat(Settings.VolumeSFXKey);
            _audioSource.volume = volume;
        }

        public void PlayEffect(AudioClip clip)
        {
            _audioSource.PlayOneShot(clip);
        }

        public void PlayRepeating(AudioClip clip)
        {
            if (_playingRepeating) return;
            
            _audioSource.clip = clip;
            _audioSource.loop = true;
            _audioSource.Play();
            
            _playingRepeating = true;
        }

        public void StopRepeating()
        {
            _audioSource.Stop();
            _audioSource.loop = false;
            _audioSource.clip = null;
            
            _playingRepeating = false;
        }
    }
}