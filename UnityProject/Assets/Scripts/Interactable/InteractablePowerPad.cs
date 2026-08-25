using UnityEngine;

namespace WiesnKrisn.Interactable
{
    public class InteractablePowerPad : InteractableType
    {
        [SerializeField] private string interactableAttraction;
        
        protected override void Interaction()
        {
            Debug.Log("Power Pad: " + interactableAttraction);
        }
    }
}