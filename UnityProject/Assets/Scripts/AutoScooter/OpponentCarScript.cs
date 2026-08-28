using UnityEngine;

public class OpponentCarScript : MonoBehaviour
{
    [SerializeField] private GameObject playerCar;
    private Rigidbody2D _rigidbodyOpponent;
    
    private float _speed = 0.0f;

    private bool _moveForward= true;
    private bool _stopMoving = false;
    private bool _justCrashed = false;

    public void Awake()
    {
        _rigidbodyOpponent = GetComponent<Rigidbody2D>();
    }

    public void Start()
    {
        InvokeRepeating(nameof(TurnTowardsPlayerCar), 0f, 3f);
    }

    public void Update()
    {
        MovingForward();
    }
    
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.Equals("PlayerCar"))
        {
            Vector3 directionToOther = (other.transform.position - transform.position).normalized;
            Vector3 myMovementDirection = GetComponent<Rigidbody2D>().linearVelocity.normalized;
                    
            if (Vector3.Dot(myMovementDirection, directionToOther) > 0 && !_justCrashed)
            {
                ScoreCounter.AddToOpponentScore(1);
                _justCrashed = true;
                Invoke(nameof(SetJustCrashedFalse), 5.0f);
            }
        }
    }

    private void SetJustCrashedFalse()
    {
        _justCrashed = false;
    }

    //This method moves the opponent car automatically. It is distributed into moving forward and turning a bit towards the player
    private void AutomatedMovement()
    {
        //Erste Idee: Wir bewegen uns kurz, drehen uns zum spieler hin, bewegen uns wieder, drehen uns wieder etwas hin
        TurnTowardsPlayerCar();
        MovingForward();
    }

    // Can be compared to accelerating the player car
    private void MovingForward()
    {
        while (_moveForward && _speed < 1.0f)
        {
            _speed += 1f;
        }

        while (_stopMoving && _speed > 0.6f)
        {
            _speed -= 5f;
        }

        while (_stopMoving&& _speed < 0.6f)
        {
            _speed -= 2f;
        }
        if(_stopMoving && _speed < 1.0f)
        {
            _speed = 0;
        }
        _rigidbodyOpponent.AddForce(transform.up * _speed);

        if (_speed == 0)
        {
            _rigidbodyOpponent.linearVelocity = Vector2.zero;
        }
        
        else
        {
            _rigidbodyOpponent.linearVelocity = transform.forward;
        }
    }

    private void TurnTowardsPlayerCar()
    {
        //Wir wollen hier den Vector haben in welcher Richtung der Spieler ist und uns da dann teilweise hindrehen
        //Frage: Ist es sinnvoll, uns auch etwas zu weit drehen können? Ich denke schon
        float tempZ = Vector2.Angle(transform.up, playerCar.transform.position - transform.position);
        transform.Rotate(transform.forward, tempZ, Space.Self);

    }
}
