using UnityEngine;

public class GrabberCollideScript : MonoBehaviour
//Should be assigned to the front part
{
    private bool _grabbed = false;
    //The grabber is the parent of this object
    [SerializeField] private GameObject grabber;
    public void OnTriggerEnter(Collider other)
    {
        //We only want to grab one/ the first plushie
        if (!_grabbed)
        {
            other.gameObject.GetComponent<PlushieScript>().SetIsHit(true);
            _grabbed = true;
        }
    }
}
