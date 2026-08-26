using UnityEngine;
using WiesnKrisn.UI;

namespace WiesnKrisn.Interactable
{
    public class InteractablePowerPad : InteractableType
    {
        [SerializeField] private string interactableAttraction;
        
        protected override void Interaction()
        {
            switch (interactableAttraction)
            {
                case "FerrisWheel":
                    PowerPadUI.Instance.ShowPad(Attractions.FerrisWheel);
                    break;
                
                case "Autoscooter":
                    PowerPadUI.Instance.ShowPad(Attractions.Autoscooter);
                    break;
                
                case "GhostTrain":
                    PowerPadUI.Instance.ShowPad(Attractions.GhostTrain);
                    break;
                
                case "RollerCoaster":
                    PowerPadUI.Instance.ShowPad(Attractions.RollerCoaster);
                    break;
                
                case "Karussell":
                    GameManager.Instance.PlayAttraction(Attractions.Karussell);
                    break;
                
                default:
                    Debug.Log("Unknown Power Pad: " + interactableAttraction);
                    break;
            }
        }
    }
}