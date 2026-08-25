using UnityEngine;

namespace WiesnKrisn.Interactable
{
    public class InteractableAttractions : InteractableType
    {
        [SerializeField] private string interactableAttraction;
        
        protected override void Interaction()
        {
            Debug.Log("Attraction: " + interactableAttraction);
        }
    }
}