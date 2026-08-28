using System;
using TMPro;
using UnityEngine;

namespace WiesnKrisn.FerrisWheel
{
    public class CounterUI : MonoBehaviour
    {
        public static CounterUI Instance {get; private set;}

        private void Awake()
        {
            Instance = this;
        }

        public void UpdateAmount(int amount, int max)
        {
            GetComponent<TMP_Text>().text = amount + "/" + max;
        }
    }
}