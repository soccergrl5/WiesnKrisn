using UnityEngine;

namespace WiesnKrisn.UI
{
    public class UIButtons : MonoBehaviour
    {
        private bool _interactedE   = false;
        private bool _interactedTab = false;
        
        private void Update()
        {
            // Pause Special Treatment
            if (Input.GetKey(KeyCode.Tab) && !_interactedTab)
            {
                PauseUI.Instance.ToggleUI();
                _interactedTab = true;
            }

            if (Input.GetKeyUp(KeyCode.Tab))
                _interactedTab = false;
            
            if (InputBlock.Instance.IsPaused()) return;
            
            // Buttons Pressed
            if (Input.GetKey(KeyCode.E) && !_interactedE)
            {
                DetectiveBookUI.Instance.ToggleUI();
                _interactedE = true;
            }
            
            // Buttons Released
            if (Input.GetKeyUp(KeyCode.E)) 
                _interactedE = false;
        }
    }
}