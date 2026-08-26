using UnityEngine;
using WiesnKrisn.Interactable.Texts;

namespace WiesnKrisn.Interactable
{
    public class InteractableAttractions : InteractableType
    {
        [SerializeField] private string interactableAttraction;
        
        protected override void Interaction()
        {
            switch (interactableAttraction)
            {
                case "RollerCoaster":
                    TextManager.Instance.ShowTextboxFor(Attractions.RollerCoaster);
                    break;
                
                case "Karussell":
                    TextManager.Instance.ShowTextboxFor(Attractions.Karussell);
                    break;
                
                case "FerrisWheel":
                    TextManager.Instance.ShowTextboxFor(Attractions.FerrisWheel);
                    break;
                
                case "CandyBar":
                    TextManager.Instance.ShowTextboxFor(Attractions.CandyBar);
                    break;
                
                default:
                    Debug.Log("Unknown Attraction: " + interactableAttraction);
                    break;
            }
        }
    }
}