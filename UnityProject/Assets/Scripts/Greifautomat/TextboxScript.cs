using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using WiesnKrisn.UI;

public class TextboxScript : MonoBehaviour
{
    [SerializeField] TMP_Text textbox;
    private float _delay = 5f;

    public void Start()
    {
        // Destroys the text object after 5 seconds
        Destroy(textbox.gameObject, _delay);
        PlushieTypes plushieTypeToGet = PlushieOrganizerScript.Instance().GetPlushieTypeToGet();
            
        textbox.text = "Get the " + plushieTypeToGet.ToString() + " plushie into the dropbox!";
    }
}

