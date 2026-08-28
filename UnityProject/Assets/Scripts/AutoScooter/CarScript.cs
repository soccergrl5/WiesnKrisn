using UnityEngine;
using UnityEngine.InputSystem;

public class CarScript : MonoBehaviour
{
    private Vector2 _movement;
    
    private float _acceleration = 10f;
    private float _deceleration = -5f;

    private float _rotation = 0.0f;
    private Rigidbody2D _rigidBodyCar;
    private bool _justCrashed = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        _movement = transform.forward;
        if (GetComponent<Rigidbody2D>() != null)
        {
            _rigidBodyCar = GetComponent<Rigidbody2D>();
        }
        else
        {
            gameObject.AddComponent<Rigidbody2D>();
            _rigidBodyCar = GetComponent<Rigidbody2D>();
        }
    }
    
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.Equals("OpponentCar"))
        {
            Vector3 directionToOther = (other.transform.position - transform.position).normalized;
            Vector3 myMovementDirection = _rigidBodyCar.linearVelocity.normalized;
                    
            if (Vector3.Dot(myMovementDirection, directionToOther) > 0 && !_justCrashed)
            {
                ScoreCounter.AddToPlayerScore(1);
                _justCrashed = true;
                Invoke(nameof(SetJustCrashedFalse), 5.0f);
            }
        }
    }

    private void SetJustCrashedFalse()
    {
        _justCrashed = false;
    }

    // Update is called once per frame
    void Update()
    {
        AddSpeedWhenPressingW();
        TurnWhenPressingAOrD();
    }
    
    //DO NOT TOUCH THIS UNDER ANY CIRCUMSTANCES I WARN YOU
    private void AddSpeedWhenPressingW()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            _rigidBodyCar.AddForce(_acceleration * transform.up);
            print(_rigidBodyCar.totalForce);

        }
        else
        {
            if (!(_rigidBodyCar.totalForce.x < 0 & _rigidBodyCar.totalForce.y < 0))
            {
                if (_rigidBodyCar.totalForce.x == 0 & _rigidBodyCar.totalForce.y == 0)
                {
                    _deceleration = 0;
                } 
                else
                {
                    _deceleration = (-_acceleration)/2;
                }
                _rigidBodyCar.AddForce(_deceleration * transform.up);
            }
            
        }

        
    }

    private void TurnWhenPressingAOrD()
    {
        Vector3 rotationAxis = Vector3.back;
        //A should turn the car to the right and D to the left, since we are going backwards
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
        {
            _rotation -= 10;
            rotationAxis = Vector3.forward;
        }
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyUp(KeyCode.A))
        {
            _rotation += 10;
            rotationAxis = Vector3.back;
        }
        
        transform.Rotate(rotationAxis, _rotation * Time.fixedDeltaTime);
    }
}
