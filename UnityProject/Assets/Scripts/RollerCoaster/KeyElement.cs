using TMPro;
using UnityEngine;

namespace WiesnKrisn.RollerCoaster
{
    public class KeyElement : MonoBehaviour
    {
        private KeyCode _keyCode;
        private Vector3 _position;
        
        public void StartTimer(KeyCode keyCode, Vector3 position)
        {
            _keyCode = keyCode;
            _position = position;
            
            transform.Translate(_position);
            GetComponentInChildren<TMP_Text>().text = _keyCode.ToString();
            
            Invoke(nameof(Fail), 3f);
        }

        private void Fail()
        {
            KeySelector.Instance.FailedKey(_keyCode);
        }

        public Vector3 GetPosition() => _position;
    }
}