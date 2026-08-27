using TMPro;
using UnityEngine;
using UnityEngine.UI;
using WiesnKrisn.CandyShop;
using WiesnKrisn.Interactable.Texts;
using WiesnKrisn.Roles;

namespace WiesnKrisn.UI
{
    public class TextboxUI : MonoBehaviour
    {
        public static TextboxUI Instance {get; private set;}

        [SerializeField] private TMP_Text textField;
        [SerializeField] private Button[] optionButtons;

        private int _maxLength;
        private float _betweenLettersTime;
        
        private void Awake()
        {
            Instance = this;
            
            HideOptionButtons();
            Hide();
            
            optionButtons[0].onClick.AddListener(() =>
            {
                HideOptionButtons();
                TextManager.Instance.OptionSelected(1);
            });
            optionButtons[1].onClick.AddListener(() =>
            {
                HideOptionButtons();
                TextManager.Instance.OptionSelected(2);
            });
            optionButtons[2].onClick.AddListener(() =>
            {
                HideOptionButtons();
                TextManager.Instance.OptionSelected(3);
            });
            
            TextManager.Instance.UIReady();
        }

        public void DisplayText(string text, float duration)
        {
            Show();
            
            textField.maxVisibleCharacters = 1;
            textField.text = text;
            
            _maxLength          = text.Length;
            _betweenLettersTime = duration / _maxLength;
            Invoke(nameof(IncreaseVisibility), _betweenLettersTime);
        }

        private void IncreaseVisibility()
        {
            textField.maxVisibleCharacters++;
            
            if (textField.maxVisibleCharacters < _maxLength)
                Invoke(nameof(IncreaseVisibility), _betweenLettersTime);
        }

        public void SkipToEndOfTextbox()
        {
            CancelInvoke();
            textField.maxVisibleCharacters = _maxLength;
        }

        public void TextsForTwoOptions(Witnesses witness)
        {
            string option1;
            bool option1Available;
            if (witness == Witnesses.SaufiGroup || witness == Witnesses.SaufiGroup2)
            {
                float prize = GameManager.Instance.GetBeerPrize();
                option1 = "Drink a Beer - " + prize + "€";
                option1Available = prize <= GameManager.Instance.GetMoney();
            }
            else if (witness == Witnesses.AperoliGroup ||
                     witness == Witnesses.AperoliGroup2)
            {
                float prize = GameManager.Instance.GetAperolPrize();
                option1 = "Drink an Aperol - " + prize + "€";
                option1Available = prize <= GameManager.Instance.GetMoney();
            }
            else if (witness == Witnesses.KarussellParents)
            {
                option1          = "Fix Carousel";
                option1Available = true;
            }
            else
            {
                float prize =
                    GameManager.Instance.GetPrizeOfGame(
                        GameManager.Instance.GetGameForWitness(witness));

                string game = "Play the Game - ";
                switch (GameManager.Instance.GetGameForWitness(witness))
                {
                    case Games.Dosenwerfen:
                        game = "Try Can Knockdown - ";
                        break;
                    
                    case Games.Autoscooter:
                        game = "Try Autoscooter - ";
                        break;
                    
                    case Games.RollerCoaster:
                        game = "Try Roller Coaster - ";
                        break;
                    
                    case Games.GhostTrain:
                        game = "Try Ghost Ride - ";
                        break;
                    
                    case Games.FerrisWheel:
                        game = "Ride Ferris Wheel - ";
                        break;
                    
                    case Games.Greifautomat:
                        game = "Try Plushie-Grapple - ";
                        break;
                }
                
                option1 = game + prize + "€";
                option1Available = prize <= GameManager.Instance.GetMoney();
            }
            
            optionButtons[0].GetComponentInChildren<TMP_Text>().text = option1;
            optionButtons[0].interactable                            = option1Available;
            
            optionButtons[2].GetComponentInChildren<TMP_Text>().text = "No Thanks!";
        }
        
        public void TextsForTwoOptions(Attractions attraction)
        {
            string option1;
            bool option1Available;
            
            if (attraction == Attractions.CandyBar)
            {
                option1          = "Open Candy Shop";
                option1Available = true;
            }
            else if (attraction == Attractions.Karussell)
            {
                option1          = "Fix Carousel";
                option1Available = true;
            }
            else
            {
                float prize           = GameManager.Instance.GetPrizeOfGame(GameManager.Instance.GetGameForAttraction(attraction));
                option1Available = prize <= GameManager.Instance.GetMoney();
                string game           = "";
            
                switch (GameManager.Instance.GetGameForAttraction(attraction))
                {
                    case Games.Dosenwerfen:
                        game = "Try Can Knockdown - ";
                        break;
                    
                    case Games.Autoscooter:
                        game = "Try Autoscooter - ";
                        break;
                    
                    case Games.RollerCoaster:
                        game = "Try Roller Coaster - ";
                        break;
                    
                    case Games.GhostTrain:
                        game = "Try Ghost Ride - ";
                        break;
                    
                    case Games.FerrisWheel:
                        game = "Ride Ferris Wheel - ";
                        break;
                    
                    case Games.Greifautomat:
                        game = "Try Plushie-Grapple - ";
                        break;
                }

                option1 = game + prize + "€";
            }
            
            optionButtons[0].GetComponentInChildren<TMP_Text>().text = option1;
            optionButtons[0].interactable                            = option1Available;
            
            optionButtons[2].GetComponentInChildren<TMP_Text>().text = "No Thanks!";
        }
        
        public void ShowTwoOptions()
        {
            optionButtons[0].gameObject.SetActive(true);
            optionButtons[2].gameObject.SetActive(true);
        }

        public void TextsForThreeOptions(Witnesses witness)
        {
            string option2        = "Drink a Beer - " + GameManager.Instance.GetBeerPrize() + "€";
            bool option2Available = GameManager.Instance.GetBeerPrize() <= GameManager.Instance.GetMoney();

            if (witness == Witnesses.AutoscooterKid)
            {
                option2          = "Buy Cotton Candy - " + GameManager.Instance.GetCandyPrize(Candy.CottonCandy) + "€";
                option2Available = GameManager.Instance.GetCandyPrize(Candy.CottonCandy) <= GameManager.Instance.GetMoney();
            }
                
            optionButtons[1].GetComponentInChildren<TMP_Text>().text = option2;
            optionButtons[1].interactable                            = option2Available;
        }

        public void ShowThreeOptions()
        {
            optionButtons[0].gameObject.SetActive(true);
            
            optionButtons[1].gameObject.SetActive(true);
            
            optionButtons[2].gameObject.SetActive(true);
        }

        private void HideOptionButtons()
        {
            optionButtons[0].gameObject.SetActive(false);
            optionButtons[1].gameObject.SetActive(false);
            optionButtons[2].gameObject.SetActive(false);
        }
        
        public void Hide() => gameObject.SetActive(false);
        private void Show() => gameObject.SetActive(true);
    }
}