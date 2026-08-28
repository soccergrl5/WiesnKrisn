using System.Timers;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{
    void Update()
    {
            transform.position += new Vector3(0, 0, 0.01f);
    }
}
