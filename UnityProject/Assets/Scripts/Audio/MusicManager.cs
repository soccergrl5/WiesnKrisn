using UnityEngine;
using WiesnKrisn.Menus;

namespace WiesnKrisn.Audio
{
    public class MusicManager : MonoBehaviour
    {
        public static MusicManager Instance {get; private set;}
        
        private void Awake()
        {
            Instance = this;
            
            AudioSource audioSource = GetComponent<AudioSource>();
            float volume            = PlayerPrefs.GetFloat(Settings.VolumeMusicKey);
            audioSource.volume      = volume;
        }
    }
}