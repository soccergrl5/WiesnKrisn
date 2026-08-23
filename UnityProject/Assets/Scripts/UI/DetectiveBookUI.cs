using UnityEngine;

namespace WiesnKrisn.UI
{
    public class DetectiveBookUI : MonoBehaviour
    {
        public static DetectiveBookUI Instance {get; private set;}

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
            InputBlock.Instance.UnBlockInput();
        }

        private void Show()
        {
            gameObject.SetActive(true);
            InputBlock.Instance.BlockInput();
        }
    }
}