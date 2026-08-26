using UnityEngine;
using UnityEngine.UI;

namespace WiesnKrisn.Menus
{
    public class GameWonMenu : MonoBehaviour
    {
        [SerializeField] private Button again;
        [SerializeField] private Button hardMode;
        [SerializeField] private Button mainMenu;

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
        }
    }
}