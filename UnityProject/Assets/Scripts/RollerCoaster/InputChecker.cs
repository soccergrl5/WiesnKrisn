using UnityEngine;

namespace WiesnKrisn.RollerCoaster
{
    public class InputChecker : MonoBehaviour
    {
        private void OnGUI()
        {
            Event e = Event.current;
            if (e.isKey && e.type == EventType.KeyDown && e.keyCode != KeyCode.None)
            {
                if (!KeySelector.Instance.GetAvailableKeys().Contains(e.keyCode) || !KeySelector.Instance.GetUsedKeys().Contains(e.keyCode))
                {
                    WrongKeyCounter.Instance.IncreaseCounter();
                    return;
                }
                
                KeySelector.Instance.PressedKey(e.keyCode);
            }
        }
    }
}