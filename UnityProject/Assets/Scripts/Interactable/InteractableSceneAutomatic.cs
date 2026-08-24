using UnityEngine;

namespace WiesnKrisn.Interactable
{
    public class InteractableSceneAutomatic : MonoBehaviour
    {
        private InteractableScene _interactableScene;

        private void Start()
        {
            _interactableScene = GetComponentInParent<InteractableScene>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            _interactableScene.TriggerExternal();
        }
    }
}