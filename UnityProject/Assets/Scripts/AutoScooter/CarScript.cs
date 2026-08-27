using UnityEngine;
using UnityEngine.InputSystem;

public class CarScript : MonoBehaviour
{
    private Vector2 _movement;
    
    private float _speed = 0.0f;

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

    private void AddSpeedWhenPressingW()
    {
        while (Input.GetKeyDown(KeyCode.W) && _speed < 1.0f)
        {
            _speed += 0.1f;
            print(_speed);
        }

        while (Input.GetKeyUp(KeyCode.W) && _speed > 0.6f)
        {
            _speed -= 0.5f;
            print(_speed);
        }

        while (Input.GetKeyUp(KeyCode.W) && _speed < 0.6f)
        {
            _speed -= 0.2f;
            print(_speed);
        }
        if(Input.GetKey(KeyCode.W) && _speed < 1.0f)
        {
            _speed = 0;
        }
        _rigidBodyCar.AddForce(transform.up * _speed);

        if (_speed == 0)
        {
            _rigidBodyCar.linearVelocity = Vector2.zero;
        }
        
        else
        {
            _rigidBodyCar.linearVelocity = transform.forward;
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
