using UnityEngine;
using WiesnKrisn.Audio;

namespace WiesnKrisn.CandyShop
{
    public class CandyShopSounds : MonoBehaviour
    {
        public static CandyShopSounds Instance {get; private set;}

        [SerializeField] private AudioClip buy;
        
        private void Awake()
        {
            Instance = this;
        }

        public void PlayBuy()
        {
            SFXManager.Instance.PlayEffect(buy);
        }
    }
}