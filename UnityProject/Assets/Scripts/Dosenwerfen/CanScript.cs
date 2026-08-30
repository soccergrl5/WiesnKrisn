using System;
using UnityEngine;

public class CanScript : MonoBehaviour
{
    private bool _hit = false;

    public void Awake()
    {
        _hit = false;
    }

    private void Update()
    {
        if (gameObject.transform.position.y < -2)
        {
            if (!_hit)
            {
                CountingScript.Instance().AddToPlayerScore(1);
                print(CountingScript.Instance().GetPlayerScore());
                _hit = true;
            }
        }

    }

    void Start()
    {
        GetComponent<Rigidbody>().solverIterations = 12;
    }
    
}