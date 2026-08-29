using UnityEngine;
using WiesnKrisn.Menus;

namespace WiesnKrisn.Audio
{
    public class MusicManager : MonoBehaviour
    {
        private void Awake()
        {
            AudioSource audioSource = GetComponent<AudioSource>();
            float volume            = PlayerPrefs.GetFloat(Settings.VolumeMusicKey);
            audioSource.volume      = volume;
        }
    }
}