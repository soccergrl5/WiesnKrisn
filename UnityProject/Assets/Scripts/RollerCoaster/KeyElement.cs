using TMPro;
using UnityEngine;

namespace WiesnKrisn.RollerCoaster
{
    public class KeyElement : MonoBehaviour
    {
        private KeyCode _keyCode;
        
        public void StartTimer(KeyCode keyCode)
        {
            _keyCode = keyCode;
            
            GetComponentInChildren<TMP_Text>().text = _keyCode.ToString();
            
            Invoke(nameof(Fail), 3f);
        }

        private void Fail()
        {
            KeySelector.Instance.FailedKey(_keyCode);
        }
    }
}