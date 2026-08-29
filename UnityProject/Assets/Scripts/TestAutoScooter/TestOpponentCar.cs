using System;
using UnityEngine;

namespace WiesnKrisn.TestAutoScooter
{
    public class TestOpponentCar : MonoBehaviour
    {
        public static TestOpponentCar Instance {get; private set;}
        
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
            
            Vector3 movement = Vector3.up * (Time.deltaTime * Speed);

            Transform player  = TestPlayerController.Instance.transform;
            Vector3 direction = player.position - transform.position;
            float angle       = Vector2.SignedAngle(transform.up, direction);

            if (angle > 0)
                transform.Rotate(Vector3.forward, Time.deltaTime * RotateSpeed);
            else if (angle < 0)
                transform.Rotate(Vector3.forward, -Time.deltaTime * RotateSpeed);
            
            transform.Translate(movement);
        }
        
        public void StartCar() => _stop = false;
        public void Stop() => _stop = true;
    }
}