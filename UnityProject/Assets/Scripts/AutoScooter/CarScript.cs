using UnityEngine;
using UnityEngine.InputSystem;

public class CarScript : MonoBehaviour
{
    private Vector2 _movement;
    
    private float _speed = 0.0f;

    private float _rotation = 0.0f;
    private Rigidbody2D _rigidBodyCar;
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

    // Update is called once per frame
    void Update()
    {
        AddSpeedWhenPressingW();
        TurnWhenPressingAOrD();
    }

    private void AddSpeedWhenPressingW()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (_speed < 5f)
            {
                _speed += 0.05f;
            }
        }

        if (Input.GetKeyUp(KeyCode.W))
        {
            if (_speed > 0f)
            {
                _speed -= 0.05f;
            }
        }
        _rigidBodyCar.AddForce(transform.up * _speed);
    }

    private void TurnWhenPressingAOrD()
    {
        Vector3 rotationAxis;
        //A should turn the car to the right and D to the left, since we are going backwards
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
        {
            _rotation -= 0.5f;
            if (_rotation > 360)
            {
                float temp = _rotation - 360;
                _rotation = 0 + temp;
            }

            if (_rotation < 0)
            {
                float temp = _rotation; //This should be negative, right? Right???
                _rotation = 360 + temp; //reminder: temp is negative (I hope)
            }
            transform.Rotate(Vector3.forward, _rotation*Time.fixedDeltaTime);
        }
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyUp(KeyCode.A))
        {
            _rotation += 0.5f;
            if (_rotation > 360)
            {
                float temp = _rotation - 360;
                _rotation = 0 + temp;
            }

            if (_rotation < 0)
            {
                float temp = _rotation; //This should be negative, right? Right???
                _rotation = 360 + temp; //reminder: temp is negative (I hope)
            }

            transform.Rotate(Vector3.back, _rotation * Time.fixedDeltaTime);
        }
        

       
    }
}
