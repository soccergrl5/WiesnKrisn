using UnityEngine;
using UnityEngine.UI;

namespace WiesnKrisn.UI
{
    public class DrunkOMeterUI : MonoBehaviour
    {
        public static DrunkOMeterUI Instance {get; private set;}

        [SerializeField] private Slider slider;

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            slider.value = 1 - GameManager.Instance.GetDrunkOMeter();
        }

        public void UpdateValue(float amount)
        {
            slider.value = 1 - amount;
        }
    }
}