using UnityEngine;
using WiesnKrisn.Audio;

public class SoundeffectScriptGeisterbahn : MonoBehaviour
{
    private static SoundeffectScriptGeisterbahn _instance;
    [SerializeField] private AudioClip shootingSound;

    public void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
    }

    public static SoundeffectScriptGeisterbahn Instance()
    {
        return _instance;
    }

    public void PlayShootingSound()
    {
        SFXManager.Instance.PlayEffect(shootingSound);
    }
    
}
