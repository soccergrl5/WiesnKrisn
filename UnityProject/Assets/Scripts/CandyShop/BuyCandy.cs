using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace WiesnKrisn.CandyShop
{
    public class BuyCandy : MonoBehaviour
    {
        [SerializeField] private Button chocolateStrawberry;
        [SerializeField] private Button candiedAlmonds;
        [SerializeField] private Button cottonCandy;

        [SerializeField] private Button exit;

        private void Awake()
        {
            chocolateStrawberry.onClick.AddListener(() =>
            {
                GameManager.Instance.BuyCandy(Candy.ChocolateStrawberry);
                
                CheckAvailability();
            });
            
            candiedAlmonds.onClick.AddListener(() =>
            {
                GameManager.Instance.BuyCandy(Candy.CandiedAlmonds);
                
                CheckAvailability();
            });
            
            cottonCandy.onClick.AddListener(() =>
            {
                GameManager.Instance.BuyCandy(Candy.CottonCandy);
                
                CheckAvailability();
            });
            
            exit.onClick.AddListener(() =>
            {
                GameManager.Instance.ExitMiniGame(true);
            });
        }

        private void Start()
        {
            CheckAvailability();
        }

        private void CheckAvailability()
        {
            float money = GameManager.Instance.GetMoney();
            
            if (money < GameManager.Instance.GetCandyPrize(Candy.ChocolateStrawberry))
                chocolateStrawberry.interactable = false;
            
            if (money < GameManager.Instance.GetCandyPrize(Candy.CandiedAlmonds))
                candiedAlmonds.interactable = false;
            
            if (money < GameManager.Instance.GetCandyPrize(Candy.CottonCandy))
                cottonCandy.interactable = false;
        }
    }
}