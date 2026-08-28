using UnityEngine;

public class tmpOpponentCarScript : MonoBehaviour
{
    [SerializeField] private GameObject playerCar;
    private Rigidbody _rigidbodyOpponent;
    
    private float _acceleration = 3f;
    private float _deceleration = -1.5f;
    private bool _moveForward= true;
    private bool _stopMoving = false;
    private bool _justCrashed = false;

    public void Awake()
    {
        _rigidbodyOpponent = GetComponent<Rigidbody>();
    }

    public void Start()
    {
        InvokeRepeating(nameof(TurnTowardsPlayerCar), 0f, 3f);
    }

    public void Update()
    {
        MovingForward();
    }
    
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("PlayerCar"))
        {
            Vector3 directionToOther = (other.transform.position - transform.position).normalized;
            Vector3 myMovementDirection = GetComponent<Rigidbody>().linearVelocity.normalized;
                    
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
        if (_moveForward)
        {
            if (_rigidbodyOpponent != null)
            {
                _rigidbodyOpponent.AddForce(_acceleration * transform.up);

            }
        }
        else
        {
            if (!(_rigidbodyOpponent.GetAccumulatedForce().x < 0 & _rigidbodyOpponent.GetAccumulatedForce().y < 0))
            {
                if (_rigidbodyOpponent.GetAccumulatedForce().x == 0 & _rigidbodyOpponent.GetAccumulatedForce().y == 0)
                {
                    _deceleration = 0;
                } 
                else
                {
                    _deceleration = (-_acceleration)/2;
                }
                _rigidbodyOpponent.AddForce(_deceleration * transform.up);
            }
            
        }
    }

    private void TurnTowardsPlayerCar()
    {
        //Wir wollen hier den Vector haben in welcher Richtung der Spieler ist und uns da dann teilweise hindrehen
        //Frage: Ist es sinnvoll, uns auch etwas zu weit drehen können? Ich denke schon
        float tempZ = Vector3.Angle(Vector3.right, playerCar.transform.position - transform.position);
        transform.Rotate(transform.forward, tempZ, Space.Self);

    }
}
