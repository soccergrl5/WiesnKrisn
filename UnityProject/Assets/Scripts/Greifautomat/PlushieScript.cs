using System;
using UnityEngine;

public class PlushieScript : MonoBehaviour
{
    [SerializeField] private Sprite bearSprite;
    [SerializeField] private Sprite duckSprite;
    [SerializeField] private Sprite unicornSprite;
    private PlushieTypes TypeForThisPlushie;

    private GameObject _grabber;
    private bool IsHit{ set;get; }
    
    void Awake()
    {
        IsHit = false;
    }

    void Start()
    {
        //This needs to be put in start and not awake since you get a nullpointer otherwise
        switch (TypeForThisPlushie)
        {
            case PlushieTypes.Bear: gameObject.GetComponentInChildren<SpriteRenderer>().sprite = bearSprite; break;
            case PlushieTypes.Bee: gameObject.GetComponentInChildren<SpriteRenderer>().sprite = duckSprite; break;
            case PlushieTypes.Unicorn: gameObject.GetComponentInChildren<SpriteRenderer>().sprite = unicornSprite; break;
        }
    }
    public void Update()
    {
        if (IsHit)
        {
            _grabber = GameObject.FindGameObjectWithTag("Grabber");
            gameObject.transform.position = _grabber.transform.position;
        }
    }

    public void SetIsHit(bool isHit)
    {
        IsHit = isHit;
    }

    public void SetPlushieType(PlushieTypes type)
    {
        TypeForThisPlushie = type;
    }

    public PlushieTypes GetPlushieType()
    {
        return TypeForThisPlushie;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("DropBox") && this.gameObject.tag.Equals("Plushie"))
        {
            print("Plushie in Box!");
            GetComponent<Rigidbody>().useGravity = true;
            IsHit = false;
        }
    }
}
