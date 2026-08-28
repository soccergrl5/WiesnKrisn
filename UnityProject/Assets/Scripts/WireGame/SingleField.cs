using UnityEngine;

namespace WiesnKrisn.WireGame
{
    public class SingleField : MonoBehaviour
    {
        [SerializeField] private bool startField;
        [SerializeField] private bool endField;
        [SerializeField] private int startColor;
        
        private bool _clickable = false;
        private int _currentWire = 0;

        private string _id;

        private bool _inputBlocked = false;
        
        private void Start()
        {
            _id = transform.position.x + " " + transform.position.y;
            
            if (startField)
            {
                _clickable   = true;
                _currentWire = startColor;
            }
            else if (endField)
            {
                _currentWire = startColor;
            }
        }

        private void OnMouseDown()
        {
            if (_inputBlocked) return;
            if (!_clickable) return;
            
            WireManager.Instance.SetActiveWire(_currentWire);
            WireManager.Instance.SetCurrentEndField(this);
            WireManager.Instance.SetLastActiveField(this);
            
            if (!startField)
                _clickable = false;
        }

        private void OnMouseUp()
        {
            if (WireManager.Instance.GetActiveWire() == 0)
                return;
            
            WireManager.Instance.ActivateEndField();
            WireManager.Instance.SetActiveWire(0);
        }

        private void OnMouseEnter()
        {
            int activeWire = WireManager.Instance.GetActiveWire();
            
            if (activeWire == 0) return;

            if (endField)
            {
                if (!IsValidPosition()) return;
                if (_currentWire != activeWire) return;
                
                WireManager.Instance.SetActiveWire(0);
                _clickable = true;
                
                WireManager.Instance.AddFinishedWire(_currentWire);
                
                return;
            }
            
            if (_currentWire != 0)
            {
                if (_currentWire == activeWire)
                {
                    if (!IsValidPosition()) return;
                    
                    WireManager.Instance.SetCurrentEndField(this);

                    SingleField lastActive = WireManager.Instance.GetLastActiveField();
                    if (!lastActive.IsEndField())
                        WireManager.Instance.UnselectLastField();
                    else
                        WireManager.Instance.RemoveFinishedWire(_currentWire);
                    
                    if (startField)
                        _clickable = true;
                }
                
                return;
            }

            if (!IsValidPosition() || WireManager.Instance.GetLastActiveField().IsEndField()) return;
                
            _currentWire = activeWire;
            SetColorForWire();
            
            WireManager.Instance.SetCurrentEndField(this);
        }

        private void OnMouseExit()
        {
            if (WireManager.Instance.GetActiveWire() == 0 || _currentWire == 0 || WireManager.Instance.GetCurrentEndField() != this)
                return;
            
            WireManager.Instance.SetLastActiveField(this);
            
            if (startField)
                _clickable = false;
        }

        public void Activate() => _clickable = true;

        public void Unselect()
        {
            _clickable   = false;
            _currentWire = 0;
            GetComponent<SpriteRenderer>().color = Color.white;
        }
        
        private int GetXPosition() => (int)transform.position.x;
        private int GetYPosition() => (int)transform.position.y;

        private bool IsEndField() => endField;

        private bool IsValidPosition()
        {
            SingleField previous = WireManager.Instance.GetLastActiveField();
            bool validField = false;
            
            if (previous.GetXPosition() == (int)transform.position.x - 1 && previous.GetYPosition() == (int)transform.position.y)
                validField = true;
            else if (previous.GetXPosition() == (int)transform.position.x + 1 && previous.GetYPosition() == (int)transform.position.y)
                validField = true;
            else if (previous.GetYPosition() == (int)transform.position.y - 1 && previous.GetXPosition() == (int)transform.position.x)
                validField = true;
            else if (previous.GetYPosition() == (int)transform.position.y + 1 && previous.GetXPosition() == (int)transform.position.x)
                validField = true;
            
            return validField;
        }

        private void SetColorForWire()
        {
            switch (_currentWire)
            {
                case 1:
                    GetComponent<SpriteRenderer>().color = Color.red;
                    break;
                
                case 2:
                    GetComponent<SpriteRenderer>().color = Color.blue;
                    break;
                
                default:
                    GetComponent<SpriteRenderer>().color = Color.white;
                    break;
            }
        }

        public void BlockInput() => _inputBlocked = true;
    }
}