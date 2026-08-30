using UnityEngine;

namespace WiesnKrisn.Movement
{
    public class PlayerMovement : MonoBehaviour
    {
        public static PlayerMovement Instance {get; private set;}
        
        private Animator _animator;
        private SpriteRenderer _spriteRenderer;
        
        private float _rightMax;
        private float _leftMax;

        private float _pos = -7;

        private bool _inverted = false;
        
        private const float Speed = 5f;

        private void Awake()
        {
            Instance = this;
            
            _animator       = GetComponent<Animator>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void Start()
        {
            float cameraWidth = Camera.main.orthographicSize * Camera.main.aspect;

            _rightMax = cameraWidth - 0.8f;
            _leftMax  = -cameraWidth + 0.8f;
            
            transform.position = new Vector3(_pos, transform.position.y, transform.position.z);
            
            _inverted = GameManager.Instance.GetDrunkOMeter() > 0.6f;
        }
        
        private void Update()
        {
            if (InputBlock.Instance.IsBlocked()
                || InputBlock.Instance.IsPaused()
                || InputBlock.Instance.IsTextbox())
                return;
            
            // Move Camera
            float movement = 0f;
            float invert   = _inverted ? -1 : 1;
            
            if (Input.GetKey(KeyCode.D))
            {
                movement += Time.deltaTime * Speed * invert;
                
                _spriteRenderer.flipX = _inverted;
            }
            else if (Input.GetKey(KeyCode.A))
            {
                movement -= Time.deltaTime * Speed * invert;
                
                _spriteRenderer.flipX = !_inverted;
            }

            if (_pos + movement > _rightMax || _pos + movement < _leftMax)
            {
                movement = 0;
            }
            
            transform.Translate(movement, 0, 0);
            _pos += movement;
            
            _animator.SetBool("Walking", movement != 0);
        }

        public void Invert() => _inverted = true;
        public void UndoInvert() => _inverted = false;
    }
}