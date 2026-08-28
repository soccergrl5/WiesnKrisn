using UnityEngine;

namespace WiesnKrisn.RollerCoaster
{
    public class StartRollerCoaster : MonoBehaviour
    {
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                gameObject.SetActive(false);
                
                InputChecker.Instance.StartIt();
                KeySelector.Instance.StartCoaster();
            }
        }
    }
}