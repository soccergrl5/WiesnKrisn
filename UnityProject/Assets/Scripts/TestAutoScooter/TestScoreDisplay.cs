using TMPro;
using UnityEngine;

namespace WiesnKrisn.TestAutoScooter
{
    public class TestScoreDisplay : MonoBehaviour
    {
        [SerializeField] private string color;

        public void UpdateScore(int score)
        {
            GetComponent<TMP_Text>().text = color + score;
        }
    }
}