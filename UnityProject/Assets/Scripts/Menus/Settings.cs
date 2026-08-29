using UnityEngine;
using UnityEngine.UI;

namespace WiesnKrisn.Menus
{
    public class Settings : MonoBehaviour
    {
        public static Settings Instance {get; private set;}
        
        [SerializeField] private Slider volumeVoice;
        [SerializeField] private Slider voulumeSFX;
        [SerializeField] private Slider volumeMusic;
        
        [SerializeField] private Button close;
        
        public const string VolumeVoiceKey = "VolumeVoice";
        public const string VolumeSFXKey = "VolumeSFX";
        public const string VolumeMusicKey = "VolumeMusic";
        
        private void Awake()
        {
            Instance = this;
            
            volumeVoice.onValueChanged.AddListener(volume =>
            {
                PlayerPrefs.SetFloat(VolumeVoiceKey, volume);
            });
            voulumeSFX.onValueChanged.AddListener(volume =>
            {
                PlayerPrefs.SetFloat(VolumeSFXKey, volume);
            });
            volumeMusic.onValueChanged.AddListener(volume =>
            {
                PlayerPrefs.SetFloat(VolumeMusicKey, volume);
            });
            
            close.onClick.AddListener(Hide);
        }

        private void Start()
        {
            Hide();
            
            if (PlayerPrefs.GetInt("StartedOnce") == 0)
            {
                PlayerPrefs.SetInt("StartedOnce", 1);

                PlayerPrefs.SetFloat(VolumeVoiceKey, 1f);
                PlayerPrefs.SetFloat(VolumeSFXKey, 0.5f);
                PlayerPrefs.SetFloat(VolumeMusicKey, 0.5f);
            }
            
            volumeVoice.value = PlayerPrefs.GetFloat(VolumeVoiceKey);
            voulumeSFX.value  = PlayerPrefs.GetFloat(VolumeSFXKey);
            volumeMusic.value = PlayerPrefs.GetFloat(VolumeMusicKey);
        }
        
        private void Hide() => gameObject.SetActive(false);
        public void Show() => gameObject.SetActive(true);
    }
}