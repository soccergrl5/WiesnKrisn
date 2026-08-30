using UnityEngine;
using WiesnKrisn.Audio;

namespace WiesnKrisn.WireGame
{
    public class WireGameSounds : MonoBehaviour
    {
        public static WireGameSounds Instance {get; private set;}

        [SerializeField] private AudioClip move;
        [SerializeField] private AudioClip finish;
        
        private void Awake()
        {
            Instance = this;
        }

        public void PlayMove()
        {
            SFXManager.Instance.PlayEffect(move);
        }

        public void PlayFinish()
        {
            SFXManager.Instance.PlayEffect(finish);
        }
    }
}