using UnityEngine;

namespace WiesnKrisn.Movement
{
    public class CameraMovement : MonoBehaviour
    {
        public static CameraMovement Instance {get; private set;}
        
        [SerializeField] private float backgroundWidth;
        
        [SerializeField] private Animator animator;
        [SerializeField] private SpriteRenderer spriteRenderer;

        private float _rightMax;
        private float _leftMax;

        private float _pos = 0f;

        private bool _inverted = false;

        private const float Speed = 10f;

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            if (Camera.main == null) return;
            
            // Calculate Boundaries for Camera
            float cameraWidth = Camera.main.orthographicSize * Camera.main.aspect;
            
            _rightMax = backgroundWidth / 2 - cameraWidth;
            _leftMax  = -(backgroundWidth / 2 - cameraWidth);

            _pos = GameManager.Instance.GetCamPosition();
            transform.position = new Vector3(_pos, 0, -10);
            
            spriteRenderer.flipX = GameManager.Instance.GetCameraFacingLeft();
            
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
                
                spriteRenderer.flipX = _inverted;
            }
            else if (Input.GetKey(KeyCode.A))
            {
                movement -= Time.deltaTime * Speed * invert;
                
                spriteRenderer.flipX = !_inverted;
            }

            if (_pos + movement > _rightMax || _pos + movement < _leftMax)
            {
                movement = 0;
            }
            
            transform.Translate(movement, 0, 0);
            _pos += movement;
            
            animator.SetBool("Walking", movement != 0);
        }

        public float GetPos() => _pos;
        public bool IsFacingLeft() => spriteRenderer.flipX;

        public void Invert() => _inverted = true;
        public void UndoInvert() => _inverted = false;
    }
}

