using UnityEngine;

public class GrabberHeadScript : MonoBehaviour
//Should be assigned to the front part
{
    private bool _grabbed = false;
    //The grabber is the parent of this object
    [SerializeField] private GameObject grabber;
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Plushie"))
        {
            //We only want to grab one/ the first plushie
            if (!_grabbed)
            {
                SoundScriptGreifautomat.Instance().PlayPlushiePickUpSound();
                other.gameObject.GetComponent<PlushieScript>().SetIsHit(true);
                _grabbed = true;
            }
        }
        
    }
}
