using TMPro;
using UnityEngine;
using WiesnKrisn.Roles;

namespace WiesnKrisn.UI
{
    public class CluesUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text[] cluesTexts;

        public void SetFirstText(Witnesses witness, string text)
        {
            cluesTexts[0].text = text + "\nfrom " + witness;
        }
        public void SetSecondText(Witnesses witness, string text)
        {
            cluesTexts[1].text = text + "\nfrom " + witness;
        }
        public void SetThirdText(Witnesses witness, string text)
        {
            cluesTexts[2].text = text + "\nfrom " + witness;
        }
    }
}