using System;
using UnityEngine;

public class CanScript : MonoBehaviour
{
    public static int Counter = 0;
    private bool _hit = false;
    private void Update()
    {
        if (gameObject.transform.position.y < -2)
        {
            if (!_hit)
            {
                Counter++;
                _hit = true;
            }
        }

    }

    void Start()
    {
        GetComponent<Rigidbody>().solverIterations = 12;
    }
}