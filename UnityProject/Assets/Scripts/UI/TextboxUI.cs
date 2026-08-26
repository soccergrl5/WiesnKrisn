using TMPro;
using UnityEngine;
using UnityEngine.UI;
using WiesnKrisn.Interactable.Texts;

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

        public void ShowTwoOptions(string option1, bool option1Available, string option2)
        {
            optionButtons[0].gameObject.SetActive(true);
            optionButtons[0].GetComponentInChildren<TMP_Text>().text = option1;
            optionButtons[0].interactable = option1Available;
            
            optionButtons[2].gameObject.SetActive(true);
            optionButtons[2].GetComponentInChildren<TMP_Text>().text = option2;
        }

        public void ShowThreeOptions(string option1, bool option1Available, string option2, bool option2Available, string option3)
        {
            optionButtons[0].gameObject.SetActive(true);
            optionButtons[0].GetComponentInChildren<TMP_Text>().text = option1;
            optionButtons[0].interactable = option1Available;
            
            optionButtons[1].gameObject.SetActive(true);
            optionButtons[1].GetComponentInChildren<TMP_Text>().text = option2;
            optionButtons[1].interactable = option2Available;
            
            optionButtons[2].gameObject.SetActive(true);
            optionButtons[2].GetComponentInChildren<TMP_Text>().text = option3;
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