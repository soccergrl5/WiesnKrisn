using UnityEngine;

namespace WiesnKrisn.Interactable
{
    [RequireComponent(typeof(Collider2D))]
    public class InteractablePerson : MonoBehaviour
    {
        private bool _interactable = false;
        private bool _interacted   = false;
        
        private void OnTriggerEnter2D(Collider2D other) => _interactable = true;
        private void OnTriggerExit2D(Collider2D other) => _interactable = false;

        private void Update()
        {
            if (InputBlock.Instance.IsBlocked() || InputBlock.Instance.IsPaused() || !_interactable) return;

            if (Input.GetKey(KeyCode.F) && !_interacted)
            {
                Debug.Log("YAYY");
                _interacted = true;
            }

            if (Input.GetKeyUp(KeyCode.F))
            {
                _interacted = false;
            }
        }
    }
}