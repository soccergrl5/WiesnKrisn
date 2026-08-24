using UnityEngine;

namespace WiesnKrisn.Interactable
{
    public class InteractableScene : InteractableType
    {
        [SerializeField] private string sceneName;
        
        protected override void Interaction()
        {
            GameManager.Instance.ChangeLocation(sceneName);
        }
        
        public void TriggerExternal() => Interaction();
    }
}