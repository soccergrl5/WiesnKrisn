using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
            Invoke(nameof(ColorChange), 1.5f);
        }

        private void ColorChange()
        {
            GetComponent<Image>().color = Color.red;
        }

        private void Fail()
        {
            KeySelector.Instance.FailedKey(_keyCode);
        }

        public Vector3 GetPosition() => _position;
    }
}