using UnityEngine;

namespace WiesnKrisn.TestAutoScooter
{
    public class TestPlayerController : MonoBehaviour
    {
        public static TestPlayerController Instance {get; private set;}
        
        private const int Speed = 6;
        private const int RotateSpeed = 100;

        private bool _stop;
        
        private void Awake()
        {
            Instance = this;
            
            _stop = true;
        }

        private void Update()
        {
            if (_stop) return;
            
            Vector3 movement = Vector3.zero;
            
            if (Input.GetKey(KeyCode.W))
            {
                movement = Vector3.up * (Time.deltaTime * Speed);
            }

            if (Input.GetKey(KeyCode.A))
            {
                transform.Rotate(Vector3.forward, -Time.deltaTime * RotateSpeed);
            }
            if (Input.GetKey(KeyCode.D))
            {
                transform.Rotate(Vector3.forward, Time.deltaTime * RotateSpeed);
            }
            
            transform.Translate(movement);
        }
        
        public void StartCar() => _stop = false;
        public void Stop() => _stop = true;
    }
}