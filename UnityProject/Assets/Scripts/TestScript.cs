using UnityEngine;

namespace WiesnKrisn
{
    public class TestScript : MonoBehaviour
    {
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                GameManager.Instance.ChangeLocation("OutdoorAreaScene");
            }
        }
    }
}