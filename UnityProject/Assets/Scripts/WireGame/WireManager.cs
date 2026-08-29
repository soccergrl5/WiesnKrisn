using System.Collections.Generic;
using UnityEngine;

namespace WiesnKrisn.WireGame
{
    public class WireManager : MonoBehaviour
    {
        public static WireManager Instance {get; private set;}

        private int _activeWire = 0;
        
        private SingleField _endField;
        private SingleField _lastActiveField;
        
        private List<int> _finishedWires = new List<int>();
        private const int MaxWires       = 4;
        
        private void Awake()
        {
            Instance = this;
        }
        
        public void SetCurrentEndField(SingleField field) => _endField = field;
        public SingleField GetCurrentEndField() => _endField;
        public void SetLastActiveField(SingleField field) => _lastActiveField = field;
        public SingleField GetLastActiveField() => _lastActiveField;

        public void ActivateEndField() => _endField.Activate();
        public void UnselectLastField() => _lastActiveField.Unselect();
        
        public void SetActiveWire(int wire) => _activeWire = wire;
        public int GetActiveWire() => _activeWire;
        
        public void AddFinishedWire(int wire)
        {
            _finishedWires.Add(wire);

            if (_finishedWires.Count == MaxWires)
            {
                foreach (SingleField field in FindObjectsByType<SingleField>(FindObjectsInactive.Include, FindObjectsSortMode.None))
                {
                    field.BlockInput();
                }
                GameOverScript.Instance.GameOver(true);
            }
        }

        public void RemoveFinishedWire(int wire) => _finishedWires.Remove(wire);
    }
}