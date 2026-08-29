using UnityEngine;

namespace WiesnKrisn.WireGame
{
    public class SingleField : MonoBehaviour
    {
        [SerializeField] private bool startField;
        [SerializeField] private bool endField;
        [SerializeField] private int startColor;

        [SerializeField] private SpriteRenderer wireSprite;

        [SerializeField] private Sprite[] blue;
        [SerializeField] private Sprite[] red;
        [SerializeField] private Sprite[] green;
        [SerializeField] private Sprite[] orange;

        private Sprite[] _currentColor;
        
        private bool _clickable  = false;
        private int _currentWire = 0;

        private string _id;

        private bool _inputBlocked = false;

        private int _enterDirection; // 1: left, 2: right, 3: top, 4: down
        private int _leaveDirection;
        
        private void Start()
        {
            _id = transform.position.x + " " + transform.position.y;
            
            if (startField)
            {
                _clickable   = true;
                _currentWire = startColor;
                
                SetColorForWire();

                wireSprite.sprite = _currentColor[0];
            }
            else if (endField)
            {
                _currentWire = startColor;
                
                SetColorForWire();
                
                wireSprite.sprite = _currentColor[2];
            }
            else
            {
                wireSprite.sprite = null;
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
                
                WireManager.Instance.GetLastActiveField().LeaveDirection(this);
                EnterDirection();
                
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

                    lastActive._enterDirection = 0;
                    lastActive.SelectRightSprite();
                    
                    _leaveDirection = 0;
                    SelectRightSprite();
                    
                    if (startField)
                        _clickable = true;
                }
                
                return;
            }

            if (!IsValidPosition() || WireManager.Instance.GetLastActiveField().IsEndField()) return;
                
            _currentWire = activeWire;
            SetColorForWire();
            
            EnterDirection();
            WireManager.Instance.GetLastActiveField().LeaveDirection(this);
            
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
            SetColorForWire();

            _enterDirection = 0;
            SelectRightSprite();
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
                    _currentColor = red;
                    break;
                
                case 2:
                    _currentColor = blue;
                    break;
                
                case 3:
                    _currentColor = green;
                    break;
                
                case 4:
                    _currentColor = orange;
                    break;
                
                default:
                    break;
            }
        }

        private void EnterDirection()
        {
            SingleField previous = WireManager.Instance.GetLastActiveField();

            if (previous.GetXPosition() == GetXPosition() - 1)
                _enterDirection = 1;
            else if (previous.GetXPosition() == GetXPosition() + 1)
                _enterDirection = 2;
            else if (previous.GetYPosition() == GetYPosition() + 1)
                _enterDirection = 3;
            else if (previous.GetYPosition() == GetYPosition() - 1)
                _enterDirection = 4;
            else
                _enterDirection = 0;
            
            SelectRightSprite();
        }

        private void LeaveDirection(SingleField next)
        {
            if (next.GetXPosition() == GetXPosition() - 1)
                _leaveDirection = 1;
            else if (next.GetXPosition() == GetXPosition() + 1)
                _leaveDirection = 2;
            else if (next.GetYPosition() == GetYPosition() + 1)
                _leaveDirection = 3;
            else if (next.GetYPosition() == GetYPosition() - 1)
                _leaveDirection = 4;
            else
                _leaveDirection = 0;
            
            SelectRightSprite();
        }

        private void SelectRightSprite()
        {
            if (startField)
            {
                SpriteStartField();
                return;
            }

            if (endField)
            {
                SpriteEndField();
                return;
            }
            
            if (_enterDirection == 0 && _leaveDirection == 0)
            {
                //Debug.Log(_id + ": empty");
                //Reset
                
                transform.rotation = Quaternion.Euler(0, 0, 0);
                wireSprite.sprite  = null;
                return;
            }
            
            if (_enterDirection == 0 && _leaveDirection != 0)
            {
                Debug.Log(_id + ": Wtf?");
                return;
            }

            if (_leaveDirection == 0)
            {
                switch (_enterDirection)
                {
                    case 1:
                        //Debug.Log(_id + ": from left");
                        transform.rotation = Quaternion.Euler(0, 0, 90);
                        wireSprite.sprite  = _currentColor[4];
                        break;
                    
                    case 2:
                        //Debug.Log(_id + ": from right");
                        transform.rotation = Quaternion.Euler(0, 0, -90);
                        wireSprite.sprite  = _currentColor[4];
                        break;
                    
                    case 3:
                        //Debug.Log(_id + ": from top");
                        transform.rotation = Quaternion.Euler(0, 0, 0);
                        wireSprite.sprite  = _currentColor[4];
                        break;
                    
                    case 4:
                        //Debug.Log(_id + ": from bottom");
                        transform.rotation = Quaternion.Euler(0, 0, 180);
                        wireSprite.sprite  = _currentColor[4];
                        break;
                }
                
                return;
            }

            if ((_enterDirection == 1 && _leaveDirection == 2)
                || (_enterDirection == 2 && _leaveDirection == 1))
            {
                //Debug.Log(_id + ": horizontal");
                transform.rotation = Quaternion.Euler(0, 0, 90);
                wireSprite.sprite  = _currentColor[5];
            }
            else if ((_enterDirection == 3 && _leaveDirection == 4)
                     || (_enterDirection == 4 && _leaveDirection == 3))
            {
                //Debug.Log(_id + ": vertical");
                transform.rotation = Quaternion.Euler(0, 0, 0);
                wireSprite.sprite  = _currentColor[5];
            }
            else if ((_enterDirection == 1 && _leaveDirection == 3)
                     || (_enterDirection == 3 && _leaveDirection == 1))
            {
                //Debug.Log(_id + ": left/top");
                transform.rotation = Quaternion.Euler(0, 0, 90);
                wireSprite.sprite  = _currentColor[6];
            }
            else if ((_enterDirection == 1 && _leaveDirection == 4)
                     || (_enterDirection == 4 && _leaveDirection == 1))
            {
                //Debug.Log(_id + ": left/bottom");
                transform.rotation = Quaternion.Euler(0, 0, 180);
                wireSprite.sprite  = _currentColor[6];
            }
            else if ((_enterDirection == 2 && _leaveDirection == 3)
                     || (_enterDirection == 3 && _leaveDirection == 2))
            {
                //Debug.Log(_id + ": right/top");
                transform.rotation = Quaternion.Euler(0, 0, 0);
                wireSprite.sprite  = _currentColor[6];
            }
            else if ((_enterDirection == 2 && _leaveDirection == 4)
                     || (_enterDirection == 4 && _leaveDirection == 2))
            {
                //Debug.Log(_id + ": right/bottom");
                transform.rotation = Quaternion.Euler(0, 0, -90);
                wireSprite.sprite  = _currentColor[6];
            }
        }

        private void SpriteStartField()
        {
            switch (_leaveDirection)
            {
                case 0:
                    //Reset
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                    wireSprite.sprite  = _currentColor[0];
                    break;
                
                case 1:
                    //Left
                    transform.rotation = Quaternion.Euler(0, 0, -90);
                    wireSprite.sprite  = _currentColor[1];
                    break;
                
                case 2:
                    //Right
                    transform.rotation = Quaternion.Euler(0, 0, 90);
                    wireSprite.sprite  = _currentColor[1];
                    break;
                
                case 3:
                    //Top
                    transform.rotation = Quaternion.Euler(0, 0, 180);
                    wireSprite.sprite  = _currentColor[1];
                    break;
                
                case 4:
                    //Bottom
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                    wireSprite.sprite  = _currentColor[1];
                    break;
            }
        }

        private void SpriteEndField()
        {
            switch (_enterDirection)
            {
                case 0:
                    //Reset
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                    wireSprite.sprite  = _currentColor[2];
                    break;
                
                case 1:
                    //Left
                    transform.rotation = Quaternion.Euler(0, 0, -90);
                    wireSprite.sprite  = _currentColor[3];
                    break;
                
                case 2:
                    //Right
                    transform.rotation = Quaternion.Euler(0, 0, 90);
                    wireSprite.sprite  = _currentColor[3];
                    break;
                
                case 3:
                    //Top
                    transform.rotation = Quaternion.Euler(0, 0, 180);
                    wireSprite.sprite  = _currentColor[3];
                    break;
                
                case 4:
                    //Bottom
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                    wireSprite.sprite  = _currentColor[3];
                    break;
            }
        }

        public void BlockInput() => _inputBlocked = true;
    }
}