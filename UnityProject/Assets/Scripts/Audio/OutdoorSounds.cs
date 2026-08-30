using UnityEngine;

namespace WiesnKrisn.Audio
{
    public class OutdoorSounds : MonoBehaviour
    {
        public static OutdoorSounds Instance {get; private set;}

        [SerializeField] private AudioClip prost;
        [SerializeField] private AudioClip pencil;

        private void Awake()
        {
            Instance = this;
        }

        public void PlayProst() => SFXManager.Instance.PlayEffect(prost);
        public void PlayPencil() => SFXManager.Instance.PlayEffect(pencil);
    }
}