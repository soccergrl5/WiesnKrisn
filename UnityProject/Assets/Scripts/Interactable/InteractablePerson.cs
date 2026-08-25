using UnityEngine;
using WiesnKrisn.Interactable.Texts;
using WiesnKrisn.Roles;

namespace WiesnKrisn.Interactable
{
    public class InteractablePerson : InteractableType
    {
        [SerializeField] private string interactableName;
        
        protected override void Interaction()
        {
            switch (interactableName)
            {
                case "SaufiGroup":
                    TextManager.Instance.ShowTextboxFor(Witnesses.SaufiGroup);
                    break;
                
                case "SaufiGroup2":
                    TextManager.Instance.ShowTextboxFor(Witnesses.SaufiGroup2);
                    break;
                
                case "AperoliGroup":
                    TextManager.Instance.ShowTextboxFor(Witnesses.AperoliGroup);
                    break;
                
                case "AperoliGroup2":
                    TextManager.Instance.ShowTextboxFor(Witnesses.AperoliGroup2);
                    break;
                
                case "AutoscooterKid":
                    TextManager.Instance.ShowTextboxFor(Witnesses.AutoscooterKid);
                    break;
                
                case "KarussellParents":
                    TextManager.Instance.ShowTextboxFor(Witnesses.KarussellParents);
                    break;
                
                case "KarussellKid":
                    TextManager.Instance.ShowTextboxFor(Witnesses.KarussellKid);
                    break;
                
                case "Influenci":
                    TextManager.Instance.ShowTextboxFor(Witnesses.Influenci);
                    break;
                
                case "SuessigkeitenFan":
                    TextManager.Instance.ShowTextboxFor(Witnesses.SuessigkeitenFan);
                    break;
                
                case "Achterbahni":
                    TextManager.Instance.ShowTextboxFor(Witnesses.Achterbahni);
                    break;
                
                case "Geisterbahni":
                    TextManager.Instance.ShowTextboxFor(Witnesses.Geisterbahni);
                    break;
                
                case "DosiWerfi":
                    TextManager.Instance.ShowTextboxFor(Witnesses.DosiWerfi);
                    break;
                
                case "GreifiTypi":
                    TextManager.Instance.ShowTextboxFor(Witnesses.GreifiTypi);
                    break;
            }
        }
    }
}