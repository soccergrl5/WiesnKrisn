using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace WiesnKrisn.RollerCoaster
{
    public class KeyElement : MonoBehaviour
    {
        private KeyCode _keyCode;
        
        private int _positionIndex;
        
        public void StartTimer(KeyCode keyCode, int positionIndex)
        {
            _keyCode       = keyCode;
            _positionIndex = positionIndex;
            
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

        public int GetPositionIndex() => _positionIndex;
    }
}