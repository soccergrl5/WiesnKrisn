using UnityEngine;

namespace WiesnKrisn.FerrisWheel
{
    public class MistakeManager : MonoBehaviour
    {
        public static MistakeManager Instance {get; private set;}

        private int _noticedMistakes  = 0;
        private const int MaxMistakes = 10;
        
        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            CounterUI.Instance.UpdateAmount(_noticedMistakes, MaxMistakes);
        }

        public void MistakeNoticed()
        {
            _noticedMistakes++;
            
            CounterUI.Instance.UpdateAmount(_noticedMistakes, MaxMistakes);

            if (_noticedMistakes == MaxMistakes)
            {
                CounterUI.Instance.Hide();
                GameOverScript.Instance.GameOver(true);
            }
        }
    }
}