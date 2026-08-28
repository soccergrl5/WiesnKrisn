using System;
using UnityEngine;
using UnityEngine.UI;

namespace WiesnKrisn.Menus
{
    public class Settings : MonoBehaviour
    {
        public static Settings Instance {get; private set;}
        
        [SerializeField] private Slider volumeVoice;
        
        [SerializeField] private Button close;
        
        public const string VolumeVoiceKey = "VolumeVoice";
        
        private void Awake()
        {
            Instance = this;
            
            volumeVoice.onValueChanged.AddListener(volume =>
            {
                PlayerPrefs.SetFloat(VolumeVoiceKey, volume);
            });
            
            close.onClick.AddListener(Hide);
        }

        private void Start()
        {
            Hide();
            
            if (PlayerPrefs.GetInt("StartedOnce") == 0)
            {
                PlayerPrefs.SetInt("StartedOnce", 1);

                PlayerPrefs.SetFloat(VolumeVoiceKey, 0.5f);
            }
            
            volumeVoice.value = PlayerPrefs.GetFloat(VolumeVoiceKey);
        }
        
        private void Hide() => gameObject.SetActive(false);
        public void Show() => gameObject.SetActive(true);
    }
}