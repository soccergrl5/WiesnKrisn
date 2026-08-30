using UnityEngine;
using WiesnKrisn.Audio;

namespace WiesnKrisn.TestAutoScooter
{
    public class AutoScooterSounds : MonoBehaviour
    {
        public static AutoScooterSounds Instance {get; private set;}

        [SerializeField] private AudioClip crash;
        
        private void Awake()
        {
            Instance = this;
        }

        public void PlayCrash()
        {
            SFXManager.Instance.PlayEffect(crash);
        }
    }
}