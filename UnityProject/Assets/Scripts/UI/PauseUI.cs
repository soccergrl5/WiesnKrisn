using UnityEngine;
using UnityEngine.UI;

namespace WiesnKrisn.UI
{
    public class PauseUI : MonoBehaviour
    {
        public static PauseUI Instance {get; private set;}
        
        [SerializeField] private Button resumeButton;
        [SerializeField] private Button mainMenuButton;

        private void Awake()
        {
            Instance = this;
            Hide();
            
            resumeButton.onClick.AddListener(Hide);
            
            mainMenuButton.onClick.AddListener(() =>
            {
                GameManager.Instance.BackToMainMenu();
            });
        }

        public void ToggleUI()
        {
            if (gameObject.activeSelf)
                Hide();
            else
                Show();
        }
        
        private void Hide()
        {
            gameObject.SetActive(false);
            InputBlock.Instance.OnResume();
            
            if (InputBlock.Instance.IsBlocked()) return;
            Cursor.visible = false;
        }

        private void Show()
        {
            gameObject.SetActive(true);
            InputBlock.Instance.OnPause();
            
            Cursor.visible = true;
        }
    }
}