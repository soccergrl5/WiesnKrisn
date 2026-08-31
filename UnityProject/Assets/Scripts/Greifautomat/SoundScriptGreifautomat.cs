using UnityEngine;
using UnityEngine.Serialization;
using WiesnKrisn.Audio;

public class SoundScriptGreifautomat : MonoBehaviour
{
    private static SoundScriptGreifautomat _instance;
    
    [SerializeField] private AudioClip grappleDownSound;
    [SerializeField] private AudioClip grappleMoveSound;
    [SerializeField] private AudioClip correctPlushieSound;
    [SerializeField] private AudioClip plushiePickUpSound;

    public void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
    }

    public static SoundScriptGreifautomat Instance()
    {
        return _instance;
    }
    public void PlayGrappleDownSound()
    {
        SFXManager.Instance.PlayEffect(grappleDownSound);
    }

    public void PlayGrappleMoveSound()
    {
        SFXManager.Instance.PlayRepeating(grappleMoveSound);
    }

    public void StopGrappleMoveSound()
    {
        SFXManager.Instance.StopRepeating();
    }

    public void PlayRightPlushieSound()
    {
        SFXManager.Instance.PlayEffect(correctPlushieSound);
    }

    public void PlayPlushiePickUpSound()
    {
        SFXManager.Instance.PlayEffect(plushiePickUpSound);
    }
}
