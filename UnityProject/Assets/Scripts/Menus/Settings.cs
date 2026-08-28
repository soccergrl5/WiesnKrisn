using UnityEngine;
using UnityEngine.UI;

namespace WiesnKrisn.Menus
{
    public class Settings : MonoBehaviour
    {
        public static Settings Instance {get; private set;}
        
        [SerializeField] private Slider volumeVoice;
        [SerializeField] private Slider voulumeSFX;
        
        [SerializeField] private Button close;
        
        public const string VolumeVoiceKey = "VolumeVoice";
        public const string VolumeSFXKey = "VolumeSFX";
        
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
            
            close.onClick.AddListener(Hide);
        }

        private void Start()
        {
            Hide();
            
            if (PlayerPrefs.GetInt("StartedOnce") == 0)
            {
                PlayerPrefs.SetInt("StartedOnce", 1);

                PlayerPrefs.SetFloat(VolumeVoiceKey, 0.5f);
                PlayerPrefs.SetFloat(VolumeSFXKey, 0.5f);
            }
            
            volumeVoice.value = PlayerPrefs.GetFloat(VolumeVoiceKey);
            voulumeSFX.value  = PlayerPrefs.GetFloat(VolumeSFXKey);
        }
        
        private void Hide() => gameObject.SetActive(false);
        public void Show() => gameObject.SetActive(true);
    }
}