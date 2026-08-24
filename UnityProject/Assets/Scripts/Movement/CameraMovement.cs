using UnityEngine;

namespace WiesnKrisn.Movement
{
    public class CameraMovement : MonoBehaviour
    {
        public static CameraMovement Instance {get; private set;}
        
        [SerializeField] private float backgroundWidth;

        private float _rightMax;
        private float _leftMax;

        private float _pos = 0f;

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
        }

        private void Update()
        {
            if (InputBlock.Instance.IsBlocked() || InputBlock.Instance.IsPaused()) return;
            
            // Move Camera
            float movement = 0f;
            
            if (Input.GetKey(KeyCode.D))
            {
                movement += Time.deltaTime * Speed;
            }
            else if (Input.GetKey(KeyCode.A))
            {
                movement -= Time.deltaTime * Speed;
            }
            else
            {
                return;
            }

            if (_pos + movement > _rightMax || _pos + movement < _leftMax)
            {
                movement = 0;
            }
            
            transform.Translate(movement, 0, 0);
            _pos += movement;
        }

        public float GetPos() => _pos;
    }
}

