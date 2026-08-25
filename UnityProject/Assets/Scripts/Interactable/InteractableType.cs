using UnityEngine;

namespace WiesnKrisn.Interactable
{
    [RequireComponent(typeof(Collider2D))]
    public abstract class InteractableType : MonoBehaviour
    {
        private bool _interactable = false;
        private bool _interacted   = false;
        
        private void OnTriggerEnter2D(Collider2D other) => _interactable = true;
        private void OnTriggerExit2D(Collider2D other) => _interactable = false;

        private void Update()
        {
            if (Input.GetKeyUp(KeyCode.F))
            {
                _interacted = false;
            }
            
            if (InputBlock.Instance.IsBlocked()
                || InputBlock.Instance.IsPaused()
                || InputBlock.Instance.IsTextbox()
                || !_interactable)
                return;

            if (Input.GetKey(KeyCode.F) && !_interacted)
            {
                _interacted = true;
                
                Interaction();
            }
        }

        protected abstract void Interaction();
    }
}