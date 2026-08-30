using System;
using UnityEngine;

namespace WiesnKrisn.RollerCoaster
{
    public class InputChecker : MonoBehaviour
    {
        public static InputChecker Instance;

        private bool _started = false;
        
        private void Awake()
        {
            Instance = this;
        }

        public void StartIt() => _started = true;
        
        private void OnGUI()
        {
            if (!_started) return;
            
            Event e = Event.current;
            if (e.isKey && e.type == EventType.KeyDown && e.keyCode != KeyCode.None)
            {
                if (!KeySelector.Instance.GetAvailableKeys().Contains(e.keyCode) || !KeySelector.Instance.GetUsedKeys().Contains(e.keyCode))
                {
                    WrongKeyCounter.Instance.IncreaseCounter();
                    return;
                }
                
                KeySelector.Instance.PressedKey(e.keyCode);
                
                RollerCoasterSounds.Instance.PlayWoooh();
            }
        }
    }
}