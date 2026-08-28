using System;
using UnityEngine;

namespace WiesnKrisn.Menus
{
    public class Credits : MonoBehaviour
    {
        [SerializeField] private GameObject endPanel;

        private void Start()
        {
            endPanel.SetActive(false);
        }

        public void CreditsEnd()
        {
            endPanel.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}