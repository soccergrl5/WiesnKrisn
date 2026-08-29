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
                    TextManager.Instance.AddHintForSituation(Attractions.Karussell);
                    TextManager.Instance.ShowTextboxFor(Attractions.Karussell);
                    break;
                
                case "FerrisWheel":
                    TextManager.Instance.AddHintForSituation(Attractions.FerrisWheel);
                    TextManager.Instance.ShowTextboxFor(Attractions.FerrisWheel);
                    break;
                
                case "Autoscooter":
                    TextManager.Instance.AddHintForSituation(Attractions.Autoscooter);
                    TextManager.Instance.ShowTextboxFor(Attractions.Autoscooter);
                    break;
                
                case "GhostTrain":
                    TextManager.Instance.AddHintForSituation(Attractions.GhostTrain);
                    TextManager.Instance.ShowTextboxFor(Attractions.GhostTrain);
                    break;
                
                case "Dosenwerfen":
                    TextManager.Instance.ShowTextboxFor(Attractions.Dosenwerfen);
                    break;
                
                case "Greifautomat":
                    TextManager.Instance.ShowTextboxFor(Attractions.Greifautomat);
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