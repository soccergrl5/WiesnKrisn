using UnityEngine;

namespace WiesnKrisn.Interactable
{
    public class InteractablePerson : InteractableType
    {
        protected override void Interaction()
        {
            Debug.Log("InteractablePerson");
        }
    }
}