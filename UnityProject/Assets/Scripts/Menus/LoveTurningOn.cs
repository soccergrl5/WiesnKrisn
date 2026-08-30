using UnityEngine;
using WiesnKrisn.Audio;

namespace WiesnKrisn.Menus
{
    public class LoveTurningOn : MonoBehaviour
    {
        [SerializeField] private AudioClip buzz;

        [SerializeField] private Animator credits;
        
        public void PlayBuzz() => SFXManager.Instance.PlayEffect(buzz);

        public void EndFlicker()
        {
            SFXManager.Instance.PlayEffect(buzz);
            GetComponent<Animator>().SetTrigger("End");
            
            MusicManager.Instance.GetComponent<AudioSource>().Play();
            
            credits.SetTrigger("Start");
        }
    }
}