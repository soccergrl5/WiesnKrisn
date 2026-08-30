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

        private bool _boughtStrawberrys = false;
        
        private void Awake()
        {
            chocolateStrawberry.onClick.AddListener(() =>
            {
                GameManager.Instance.BuyCandy(Candy.ChocolateStrawberry);
                
                CheckAvailability();
                
                _boughtStrawberrys = true;
                
                CandyShopSounds.Instance.PlayBuy();
            });
            
            candiedAlmonds.onClick.AddListener(() =>
            {
                GameManager.Instance.BuyCandy(Candy.CandiedAlmonds);
                
                CheckAvailability();
                
                CandyShopSounds.Instance.PlayBuy();
            });
            
            cottonCandy.onClick.AddListener(() =>
            {
                GameManager.Instance.BuyCandy(Candy.CottonCandy);
                
                CheckAvailability();
                
                CandyShopSounds.Instance.PlayBuy();
            });
            
            exit.onClick.AddListener(() =>
            {
                GameManager.Instance.ExitMiniGame(_boughtStrawberrys);
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