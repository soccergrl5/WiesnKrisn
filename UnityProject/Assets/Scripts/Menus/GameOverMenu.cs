using System;
using UnityEngine;
using UnityEngine.UI;

namespace WiesnKrisn.Menus
{
    public class GameOverMenu : MonoBehaviour
    {
        [SerializeField] private Button retry;
        [SerializeField] private Button mainMenu;

        [SerializeField] private string ending;

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

        private void Start()
        {
            if (ending == "Drunk")
                PlayerPrefs.SetInt(Endings.DrunkKey, 1);
            
            if (ending == "WrongGuy")
                PlayerPrefs.SetInt(Endings.WrongGuyKey, 1);
        }
    }
}