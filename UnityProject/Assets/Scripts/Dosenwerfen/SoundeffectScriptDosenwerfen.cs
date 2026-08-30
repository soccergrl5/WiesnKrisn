using UnityEngine;
using WiesnKrisn.Audio;

public class SoundeffectScriptDosenwerfen : MonoBehaviour
{
    private static SoundeffectScriptDosenwerfen _instance;
    [SerializeField] private AudioClip ballThrowSound;
    [SerializeField] private AudioClip canHitSound;
    public void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
    }
    
    public static SoundeffectScriptDosenwerfen Instance()
    {
        return _instance;
    }

    public void PlayBallThrow()
    {
        SFXManager.Instance.PlayEffect(ballThrowSound);
    }

    public void PlayCanHit()
    {
        SFXManager.Instance.PlayEffect(canHitSound);
    }
}
