using System.Collections;
using UnityEngine;
using WiesnKrisn.Interactable.Texts;
using WiesnKrisn.Menus;

namespace WiesnKrisn.Audio
{
    public class VoiceLineManager : MonoBehaviour
    {
        public static VoiceLineManager Instance {get; private set;}

        private AudioSource _audioSource;
        private Coroutine _coroutine;

        private int _specialLine  = 0;
        private string _beginning = null;
        private string _trait     = null;
        private string _and       = null;
        private string _trait2    = null;

        private const float MakeItSmooth = 0.2f;
        
        private void Awake()
        {
            Instance = this;
            
            _audioSource = GetComponent<AudioSource>();
            
            TextManager.Instance.AudioReady();

            float volume        = PlayerPrefs.GetFloat(Settings.VolumeVoiceKey);
            _audioSource.volume = volume;
        }

        public void SelectFirstTrait(int category, string info)
        {
            string path  = CreatePath(category, info);
            bool useWore = category is 0 or 3 or 6 or 8 or 9;

            _specialLine = 1;
            _beginning   = useWore ? "_1" : "_2";
            _trait       = "Traits/" + path;
        }

        public void SelectOtherTraits(int category1, string info1, int category2, string info2)
        {
            string path1 = CreatePath(category1, info1);
            bool useWore = category1 is 0 or 3 or 6 or 8 or 9;
            string path2 = CreatePath(category2, info2);
            
            _specialLine = 2;
            _beginning   = useWore ? "_1" : "_2";
            _trait       = "Traits/" + path1;
            _and         = "And";
            _trait2      = "Traits/" + path2;
        }
        
        public void PlayVoiceLine(string identifier, string category, int part)
        {
            StopCoroutine();
            
            if (_specialLine == 1)
            {
                _coroutine = StartCoroutine(PlaySingleTrait(identifier, category, part));
                return;
            }

            if (_specialLine == 2)
            {
                _coroutine = StartCoroutine(PlayMultipleTraits(identifier, category, part));
                return;
            }
            
            AudioClip clip = Resources.Load<AudioClip>("Audio/" + identifier + "/" + category + part);
            
            _audioSource.clip = clip;
            _audioSource.Play();
        }

        private IEnumerator PlaySingleTrait(string identifier, string category, int part)
        {
            _specialLine = 0;
            
            AudioClip clip    = Resources.Load<AudioClip>("Audio/" + identifier + "/" + category + part + _beginning);
            _audioSource.clip = clip;
            
            _audioSource.Play();
            yield return new WaitForSeconds(clip.length - MakeItSmooth);
            
            clip = Resources.Load<AudioClip>("Audio/" + identifier + "/" + _trait);
            _audioSource.clip = clip;
            _audioSource.Play();
        }

        private IEnumerator PlayMultipleTraits(string identifier, string category, int part)
        {
            _specialLine = 0;
            
            AudioClip clip    = Resources.Load<AudioClip>("Audio/" + identifier + "/" + category + part + _beginning);
            _audioSource.clip = clip;
            
            _audioSource.Play();
            yield return new WaitForSeconds(clip.length - MakeItSmooth);
            
            clip = Resources.Load<AudioClip>("Audio/" + identifier + "/" + _trait);
            _audioSource.clip = clip;
            _audioSource.Play();
            
            yield return new WaitForSeconds(clip.length - MakeItSmooth);
            
            clip = Resources.Load<AudioClip>("Audio/" + identifier + "/" + _and);
            _audioSource.clip = clip;
            _audioSource.Play();
            
            yield return new WaitForSeconds(clip.length - MakeItSmooth);
            
            clip = Resources.Load<AudioClip>("Audio/" + identifier + "/" + _trait2);
            _audioSource.clip = clip;
            _audioSource.Play();
        }

        private string CreatePath(int category, string info)
        {
            string path = "";

            switch (category)
            {
                case 0:
                    if (info.Contains("casual"))
                        path = "ClothesCasual";
                    else if (info.Contains("leather"))
                        path = "ClothesLederhose";
                    else
                        path = "ClothesDirndl";
                    break;
                
                case 1:
                    if (info.Contains("dark"))
                        path = "HairColorDark";
                    else if (info.Contains("light"))
                        path = "HairColorLight";
                    else
                        path = "HairColorColorful";
                    break;
                
                case 2:
                    if (info.Contains("red"))
                        path = "ClothingRed";
                    else if (info.Contains("green"))
                        path = "ClothingGreen";
                    else
                        path = "ClothingBlue";
                    break;
                
                case 3:
                    if (info.Contains("sneakers"))
                        path = "ShoesSneaker";
                    else if (info.Contains("high-heels"))
                        path = "ShoesHeels";
                    else
                        path = "ShoesHaferl";
                    break;
                
                case 4:
                    if (info.Contains("short"))
                        path = "HairstyleShort";
                    else if (info.Contains("open"))
                        path = "HairstyleOpen";
                    else
                        path = "HairstylePonytail";
                    break;
                
                case 5:
                    if (info.Contains("brown"))
                        path = "EyeColorBrown";
                    else if (info.Contains("green"))
                        path = "EyeColorGreen";
                    else
                        path = "EyeColorBlue";
                    break;
                
                case 6:
                    if (info.Contains("no"))
                        path = "HeadpieceNo";
                    else if (info.Contains("cap"))
                        path = "HeadpieceCap";
                    else
                        path = "HeadpieceHendl";
                    break;
                
                case 7:
                    if (info.Contains("no"))
                        path = "RoseNo";
                    else if (info.Contains("red"))
                        path = "RoseRed";
                    else
                        path = "RoseYellow";
                    break;
                
                case 8:
                    if (info.Contains("no"))
                        path = "LebkuchenNo";
                    else if (info.Contains("small"))
                        path = "LebkuchenSmall";
                    else
                        path = "LebkuchenBig";
                    break;
                
                case 9:
                    if (info.Contains("no"))
                        path = "GlassesNo";
                    else if (info.Contains("regular"))
                        path = "GlassesRegular";
                    else
                        path = "GlassesSun"; 
                    break;
            }
            
            return path;
        }
        
        public void Stop()
        {
            _audioSource.Stop();
            StopCoroutine();
        }

        private void StopCoroutine()
        {
            if (_coroutine != null)
                StopCoroutine(_coroutine);
        }
    }
}