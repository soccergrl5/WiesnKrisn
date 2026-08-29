using UnityEngine;

namespace WiesnKrisn.TestAutoScooter
{
    public class TestAutoscooterStartUI : MonoBehaviour
    {
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                gameObject.SetActive(false);
                
                TestPlayerController.Instance.StartCar();
                TestOpponentCar.Instance.StartCar();
                TestAutoscooterManager.Instance.StartTimer();
            }
        }
    }
}