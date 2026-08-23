using UnityEngine;

namespace WiesnKrisn
{
    public class CameraMovement : MonoBehaviour
    {
        [SerializeField] private float backgroundWidth;

        private float _rightMax;
        private float _leftMax;

        private float _pos = 0f;

        private readonly float _speed = 10f;
        
        private void Start()
        {
            // Calculate Boundaries for Camera
            float cameraWidth = Camera.main.orthographicSize * Camera.main.aspect;
            
            _rightMax = backgroundWidth / 2 - cameraWidth;
            _leftMax  = -(backgroundWidth / 2 - cameraWidth);
        }

        private void Update()
        {
            if (InputBlock.Instance.IsBlocked() || InputBlock.Instance.IsPaused()) return;
            
            // Move Camera
            float movement = 0f;
            
            if (Input.GetKey(KeyCode.D))
            {
                movement += Time.deltaTime * _speed;
            }
            else if (Input.GetKey(KeyCode.A))
            {
                movement -= Time.deltaTime * _speed;
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
    }
}

