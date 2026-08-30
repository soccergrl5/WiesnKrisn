using TMPro;
using UnityEngine;
using UnityEngine.UI;
using WiesnKrisn.Audio;
using WiesnKrisn.Roles;

namespace WiesnKrisn.UI
{
    public class CluesUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text[] cluesTexts;
        [SerializeField] private Image[] cluesMarker;
        [SerializeField] private TMP_Text[] witnessTexts;
        [SerializeField] private Image[] witnessMarker;
        [SerializeField] private Button[] cluesButtons;

        [SerializeField] private Sprite wrong;
        [SerializeField] private Sprite right;
        
        [SerializeField] private string id;
        
        private int[] _status = new int[3];
        
        private void Awake()
        {
            cluesButtons[0].onClick.AddListener(() =>
            {
                _status[0] = (_status[0] + 1) % 3;
                PlayerPrefs.SetInt(id + 0, _status[0]);

                SetColors(0);
            });
            cluesButtons[1].onClick.AddListener(() =>
            {
                _status[1] = (_status[1] + 1) % 3;
                PlayerPrefs.SetInt(id + 1, _status[1]);

                SetColors(1);
            });
            cluesButtons[2].onClick.AddListener(() =>
            {
                _status[2] = (_status[2] + 1) % 3;
                PlayerPrefs.SetInt(id + 2, _status[2]);

                SetColors(2);
            });
            
            cluesButtons[0].enabled = false;
            cluesButtons[1].enabled = false;
            cluesButtons[2].enabled = false;
        }

        public void SetFirstText(Witnesses witness, string text)
        {
            cluesTexts[0].text      = text;
            witnessTexts[0].text    = "- " + WitnessNames.Names[witness];
            cluesButtons[0].enabled = true;

            _status[0] = PlayerPrefs.GetInt(id + 0, 0);
            SetColors(0);
        }
        public void SetSecondText(Witnesses witness, string text)
        {
            cluesTexts[1].text      = text;
            witnessTexts[1].text    = "- " + WitnessNames.Names[witness];
            cluesButtons[1].enabled = true;

            _status[1] = PlayerPrefs.GetInt(id + 1, 0);
            SetColors(1);
        }
        public void SetThirdText(Witnesses witness, string text)
        {
            cluesTexts[2].text      = text;
            witnessTexts[2].text    = "- " + WitnessNames.Names[witness];
            cluesButtons[2].enabled = true;

            _status[2] = PlayerPrefs.GetInt(id + 2, 0);
            SetColors(2);
        }

        public void ResetColors()
        {
            PlayerPrefs.SetInt(id + 0, 0);
            PlayerPrefs.SetInt(id + 1, 0);
            PlayerPrefs.SetInt(id + 2, 0);
        }

        private void SetColors(int index)
        {
            switch (_status[index])
            {
                case 0:
                    cluesMarker[index].GetComponent<Image>().enabled   = false;
                    witnessMarker[index].GetComponent<Image>().enabled = false;
                    break;
                    
                case 1:
                    cluesMarker[index].GetComponent<Image>().enabled   = true;
                    witnessMarker[index].GetComponent<Image>().enabled = true;
                    
                    cluesMarker[index].GetComponent<Image>().sprite   = right;
                    witnessMarker[index].GetComponent<Image>().sprite = right;
                    
                    OutdoorSounds.Instance.PlayPencil();
                    break;
                    
                case 2:
                    cluesMarker[index].GetComponent<Image>().enabled   = true;
                    witnessMarker[index].GetComponent<Image>().enabled = true;
                    
                    cluesMarker[index].GetComponent<Image>().sprite   = wrong;
                    witnessMarker[index].GetComponent<Image>().sprite = wrong;
                    
                    OutdoorSounds.Instance.PlayPencil();
                    break;
            }
        }
    }
}