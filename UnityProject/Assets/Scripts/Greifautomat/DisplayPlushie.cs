using UnityEngine;
using UnityEngine.UI;

public class DisplayPlushie : MonoBehaviour
{
    [SerializeField] private Sprite bear;
    [SerializeField] private Sprite duck;
    [SerializeField] private Sprite unicorn;

    void Start()
    {
        switch (PlushieOrganizerScript.Instance().GetPlushieTypeToGet())
        {
            case PlushieTypes.Bear: GetComponent<Image>().sprite = bear; break;
            case PlushieTypes.Bee: GetComponent<Image>().sprite = duck; break;
            case PlushieTypes.Unicorn: GetComponent<Image>().sprite = unicorn; break;
            default: GetComponent<Image>().sprite = bear; break;
        }
    }
}
