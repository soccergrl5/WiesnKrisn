using TMPro;
using UnityEngine;
using UnityEngine.UI;
using WiesnKrisn.Roles;

namespace WiesnKrisn.UI
{
    public class CluesUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text[] cluesTexts;
        [SerializeField] private Button[] cluesButtons;

        [SerializeField] private string id;
        
        private int[] _status = new int[3];
        
        private void Awake()
        {
            cluesButtons[0].onClick.AddListener(() =>
            {
                _status[0] = (_status[0] + 1) % 3;
                PlayerPrefs.SetInt(id + 0, _status[0]);

                switch (_status[0])
                {
                    case 0:
                        cluesTexts[0].color = Color.black;
                        break;
                    
                    case 1:
                        cluesTexts[0].color = Color.green;
                        break;
                    
                    case 2:
                        cluesTexts[0].color = Color.red;
                        break;
                }
            });
            cluesButtons[1].onClick.AddListener(() =>
            {
                _status[1] = (_status[1] + 1) % 3;
                PlayerPrefs.SetInt(id + 1, _status[1]);

                switch (_status[1])
                {
                    case 0:
                        cluesTexts[1].color = Color.black;
                        break;
                    
                    case 1:
                        cluesTexts[1].color = Color.green;
                        break;
                    
                    case 2:
                        cluesTexts[1].color = Color.red;
                        break;
                }
            });
            cluesButtons[2].onClick.AddListener(() =>
            {
                _status[2] = (_status[2] + 1) % 3;
                PlayerPrefs.SetInt(id + 2, _status[2]);

                switch (_status[2])
                {
                    case 0:
                        cluesTexts[2].color = Color.black;
                        break;
                    
                    case 1:
                        cluesTexts[2].color = Color.green;
                        break;
                    
                    case 2:
                        cluesTexts[2].color = Color.red;
                        break;
                }
            });
            
            cluesButtons[0].enabled = false;
            cluesButtons[1].enabled = false;
            cluesButtons[2].enabled = false;
        }

        public void SetFirstText(Witnesses witness, string text)
        {
            cluesTexts[0].text      = text + "\nfrom " + witness;
            cluesButtons[0].enabled = true;

            _status[0] = PlayerPrefs.GetInt(id + 0, 0);
            switch (_status[0])
            {
                case 0:
                    cluesTexts[0].color = Color.black;
                    break;
                    
                case 1:
                    cluesTexts[0].color = Color.green;
                    break;
                    
                case 2:
                    cluesTexts[0].color = Color.red;
                    break;
            }
        }
        public void SetSecondText(Witnesses witness, string text)
        {
            cluesTexts[1].text      = text + "\nfrom " + witness;
            cluesButtons[1].enabled = true;

            _status[1] = PlayerPrefs.GetInt(id + 1, 0);
            switch (_status[1])
            {
                case 0:
                    cluesTexts[1].color = Color.black;
                    break;
                    
                case 1:
                    cluesTexts[1].color = Color.green;
                    break;
                    
                case 2:
                    cluesTexts[1].color = Color.red;
                    break;
            }
        }
        public void SetThirdText(Witnesses witness, string text)
        {
            cluesTexts[2].text      = text + "\nfrom " + witness;
            cluesButtons[2].enabled = true;

            _status[2] = PlayerPrefs.GetInt(id + 2, 0);
            switch (_status[2])
            {
                case 0:
                    cluesTexts[02].color = Color.black;
                    break;
                    
                case 1:
                    cluesTexts[2].color = Color.green;
                    break;
                    
                case 2:
                    cluesTexts[2].color = Color.red;
                    break;
            }
        }

        public void ResetColors()
        {
            PlayerPrefs.SetInt(id + 0, 0);
            PlayerPrefs.SetInt(id + 1, 0);
            PlayerPrefs.SetInt(id + 2, 0);
        }
    }
}