using UnityEngine;
using WiesnKrisn.Audio;

namespace WiesnKrisn.RollerCoaster
{
    public class RollerCoasterSounds : MonoBehaviour
    {
        public static RollerCoasterSounds Instance { get; private set; }

        [SerializeField] private AudioClip[] screams;
        [SerializeField] private AudioClip[] wooohs;

        private void Awake()
        {
            Instance = this;
        }

        public void PlayScream()
        {
            SFXManager.Instance.PlayEffect(screams[Random.Range(0, screams.Length)]);
        }

        public void PlayWoooh()
        {
            SFXManager.Instance.PlayEffect(wooohs[Random.Range(0, wooohs.Length)]);
        }
    }
}