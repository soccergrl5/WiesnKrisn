using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using WiesnKrisn.Roles;

namespace WiesnKrisn.Menus
{
    public class GameWonDecision : MonoBehaviour
    {
        [SerializeField] private Button love;
        [SerializeField] private Button arrest;

        [SerializeField] private TMP_Text text;
        
        private void Awake()
        {
            love.onClick.AddListener(() =>
            {
                SceneManager.LoadScene("GameWonLove");
            });
            
            arrest.onClick.AddListener(() =>
            {
                SceneManager.LoadScene("GameWonArrested");
            });
        }

        private void Start()
        {
            string endText = text.text;
            Suspects lover = GameManager.Instance.GetLover();
            
            text.text = endText.Replace("[]", WitnessNames.SuspectNames[lover].ToUpper());
        }
    }
}