using System.Collections.Generic;
using UnityEngine;

public class GrabberScript : MonoBehaviour
{
    [SerializeField] private GameObject grabber;
    
    public void GrabPlushie()
    {
        print(grabber.transform.position);
        while (grabber.transform.position.y > 0)
        {
            grabber.transform.position = new Vector3(grabber.transform.position.x, grabber.transform.position.y - 0.00001f, grabber.transform.position.z);
        }
        
    }
}
