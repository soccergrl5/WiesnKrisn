using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace WiesnKrisn.Menus
{
    public class GameWonDecision : MonoBehaviour
    {
        [SerializeField] private Button love;
        [SerializeField] private Button arrest;

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
    }
}