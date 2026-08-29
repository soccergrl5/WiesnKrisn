using System;
using TMPro;
using UnityEngine;

namespace WiesnKrisn.TestAutoScooter
{
    public class TestTimeDisplay : MonoBehaviour
    {
        public static TestTimeDisplay Instance;

        private void Awake()
        {
            Instance = this;
        }

        public void UpdateTimer(float time)
        {
            TimeSpan timeSpan = TimeSpan.FromSeconds(time);
            GetComponent<TMP_Text>().text = timeSpan.ToString("mm':'ss");
        }
    }
}