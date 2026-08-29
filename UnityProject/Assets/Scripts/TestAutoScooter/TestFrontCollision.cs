using UnityEngine;

namespace WiesnKrisn.TestAutoScooter
{
    public class TestFrontCollision : MonoBehaviour
    {
        [SerializeField] private int car;
        
        private bool _onCollision;
        
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (_onCollision) return;
            if (other.collider.name.Contains("Border"))
                return;
            
            _onCollision = true;
            Debug.Log(gameObject.name + " -> " + other.collider.name);
            
            TestAutoscooterManager.Instance.HitOtherCar(car);
            
            Invoke(nameof(ReadyForNextCollision), 3f);
        }
        
        private void ReadyForNextCollision() => _onCollision = false;
    }
}