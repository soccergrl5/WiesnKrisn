using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace WiesnKrisn.Menus
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private Button start;
        [SerializeField] private Button difficulty;
        [SerializeField] private Button settings;

        private bool _inEasyMode = true;
        
        private void Awake()
        {
            start.onClick.AddListener(() =>
            {
                GameManager.Instance.StartGame(_inEasyMode);
            });
            
            difficulty.onClick.AddListener(() =>
            {
                _inEasyMode = !_inEasyMode;
                
                difficulty.GetComponentInChildren<TMP_Text>().text = _inEasyMode ? "Difficulty: Easy" : "Difficulty: Hard";
            });
            
            settings.onClick.AddListener(() =>
            {
                Debug.Log("Settings");
            });
        }
    }
}