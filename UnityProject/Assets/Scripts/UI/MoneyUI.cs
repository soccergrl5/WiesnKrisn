using TMPro;
using UnityEngine;

namespace WiesnKrisn.UI
{
    public class MoneyUI : MonoBehaviour
    {
        public static MoneyUI Instance {get; private set;}
        
        [SerializeField] private TMP_Text text;
        
        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            text.text = GameManager.Instance.GetMoney().ToString("F");
        }

        public void UpdateAmount(float amount)
        {
            text.text = amount.ToString("F");
        }
    }
}