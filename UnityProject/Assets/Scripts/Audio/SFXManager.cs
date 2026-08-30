using UnityEngine;
using WiesnKrisn.Menus;

namespace WiesnKrisn.Audio
{
    public class SFXManager : MonoBehaviour
    {
        public static SFXManager Instance {get; private set;}
        
        private AudioSource _audioSource;

        private void Awake()
        {
            Instance = this;
            
            _audioSource = GetComponent<AudioSource>();
            float volume            = PlayerPrefs.GetFloat(Settings.VolumeMusicKey);
            _audioSource.volume      = volume;
        }

        public void PlayEffect(AudioClip clip)
        {
            _audioSource.PlayOneShot(clip);
        }
    }
}