using UnityEngine;

namespace WiesnKrisn.Movement
{
    public class PlayerMovement : MonoBehaviour
    {
        private Animator _animator;
        private SpriteRenderer _spriteRenderer;
        
        private float _rightMax;
        private float _leftMax;

        private float _pos = -7;
        
        private const float Speed = 5f;

        private void Awake()
        {
            _animator       = GetComponent<Animator>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void Start()
        {
            float cameraWidth = Camera.main.orthographicSize * Camera.main.aspect;

            _rightMax = cameraWidth - 0.8f;
            _leftMax  = -cameraWidth + 0.8f;
            
            transform.position = new Vector3(_pos, transform.position.y, transform.position.z);
        }
        
        private void Update()
        {
            if (InputBlock.Instance.IsBlocked()
                || InputBlock.Instance.IsPaused()
                || InputBlock.Instance.IsTextbox())
                return;
            
            // Move Camera
            float movement = 0f;
            
            if (Input.GetKey(KeyCode.D))
            {
                movement += Time.deltaTime * Speed;
                
                _spriteRenderer.flipX = false;
            }
            else if (Input.GetKey(KeyCode.A))
            {
                movement -= Time.deltaTime * Speed;
                
                _spriteRenderer.flipX = true;
            }

            if (_pos + movement > _rightMax || _pos + movement < _leftMax)
            {
                movement = 0;
            }
            
            transform.Translate(movement, 0, 0);
            _pos += movement;
            
            _animator.SetBool("Walking", movement != 0);
        }
    }
}