using UnityEngine;

namespace WiesnKrisn.Interactable
{
    public class InteractablePerson : InteractableType
    {
        [SerializeField] private string interactableName;
        
        protected override void Interaction()
        {
            Debug.Log(interactableName);
        }
    }
}