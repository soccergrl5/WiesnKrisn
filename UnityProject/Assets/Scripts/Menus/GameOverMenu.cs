using UnityEngine;
using UnityEngine.UI;

namespace WiesnKrisn.Menus
{
    public class GameOverMenu : MonoBehaviour
    {
        [SerializeField] private Button retry;
        [SerializeField] private Button mainMenu;

        private void Awake()
        {
            retry.onClick.AddListener(() =>
            {
                GameManager.Instance.RestartGame();
            });
            
            mainMenu.onClick.AddListener(() =>
            {
                GameManager.Instance.BackToMainMenu();
            });
        }
    }
}