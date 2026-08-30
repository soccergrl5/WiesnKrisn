using UnityEngine;
using UnityEngine.UI;

namespace WiesnKrisn.Menus
{
    public class GameWonMenu : MonoBehaviour
    {
        [SerializeField] private Button again;
        [SerializeField] private Button hardMode;
        [SerializeField] private Button mainMenu;

        [SerializeField] private string ending;
        
        private void Awake()
        {
            again.onClick.AddListener(() =>
            {
                GameManager.Instance.RestartGame();
            });
            
            hardMode.onClick.AddListener(() =>
            {
                GameManager.Instance.RestartGame(false);
            });
            
            mainMenu.onClick.AddListener(() =>
            {
                GameManager.Instance.BackToMainMenu();
            });
        }

        private void Start()
        {
            if (!GameManager.Instance.InEasyMode())
                hardMode.gameObject.SetActive(false);
            
            if (ending == "Arrest")
                PlayerPrefs.SetInt(Endings.ArrestKey, 1);
            
            if (ending == "Love")
                PlayerPrefs.SetInt(Endings.LoveKey, 1);
        }
    }
}