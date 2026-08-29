using UnityEngine;
using UnityEngine.UI;
using WiesnKrisn.Roles;

namespace WiesnKrisn.Menus
{
    public class GameOverSuspect : MonoBehaviour
    {
        [SerializeField] private Sprite[] suspects;
        [SerializeField] private Image suspectImage;

        private void Start()
        {
            Suspects suspect = GameManager.Instance.GetEndSuspect();

            suspectImage.sprite = suspects[(int)suspect];
        }
    }
}