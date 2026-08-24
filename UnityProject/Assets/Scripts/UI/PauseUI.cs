using UnityEngine;

namespace WiesnKrisn.UI
{
    public class PauseUI : MonoBehaviour
    {
        public static PauseUI Instance {get; private set;}

        private void Awake()
        {
            Instance = this;
            Hide();
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