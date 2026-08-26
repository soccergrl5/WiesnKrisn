using System.Collections.Generic;
using UnityEngine;

public class GrabberScript : MonoBehaviour
{
    [SerializeField] private GameObject grabber;
    private static GrabberScript Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    
    public void GrabPlushie()
    {
        print(grabber.transform.position);
        while (grabber.transform.position.y > 0)
        {
            grabber.transform.position = new Vector3(grabber.transform.position.x, grabber.transform.position.y - 0.00001f, grabber.transform.position.z);
        }
    }

    
    public void ChangeColorAccordingToDimension()
    {
        SpriteRenderer[] spriteRenderer = grabber.GetComponentsInChildren<SpriteRenderer>();
        foreach (SpriteRenderer child in spriteRenderer)
        {
            switch (grabber.transform.position.z)
            {
                case -3: child.color = new Color(0, 0.85f, 1); break;
                case -2: child.color = new Color(0, 0.58f, 0.74f); break;
                case -1: child.color = new Color(0, 0.38f, 0.49f); break;
                default: child.color = Color.white; break;
            }
        }
        
    }

    public static GrabberScript GetInstance()
    {
        return Instance;
    }
}
