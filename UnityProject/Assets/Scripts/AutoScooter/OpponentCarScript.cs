using UnityEngine;

public class OpponentCarScript : MonoBehaviour
{
    private bool _justCrashed = false;
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
    }}
