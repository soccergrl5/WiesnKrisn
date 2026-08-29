using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlushieOrganizerScript : MonoBehaviour
{
    [SerializeField] private GameObject plushie;
    private PlushieTypes _plushieTypeToGet;
    private static PlushieOrganizerScript _instance;
    public Color darkColor = new Color(0, 0.85f, 1);
    public Color middleColor = new Color(0, 0.85f, 1);
    public Color lightColor = new Color(0, 0.85f, 1);



    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        SelectPlushieToGet();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DividePlushiesEvenly();
    }

    private void DividePlushiesEvenly()
    {
        List<PlushieTypes> plushieTypes = CreatePlushieList();
        //We need to make sure to stay within the borders of the camera
        var minX = CameraCornersScript.Instance.GetMinX();
        var maxX = CameraCornersScript.Instance.GetMaxX();
        var steps = (maxX - minX) / 3; //The 3 is not variable, since we want 3*3 plushies

        //We want 9 plushies on 3 z layers
        for (int x = 0; x < 3; x++)
        {
            for (int z = -3; z < 0; z++)
            {
                float tempX;
                switch (x)
                {
                    //To add some variability, we add a random number to x
                    case 0:
                        //In the row with z=-3 the dropbox is in the bottom left corner, therefore there should spawn no plushies
                        if (z == -3)
                        {
                            //Be gone plushie
                            tempX = -100;
                        }
                        else
                        {
                            tempX = -steps + Random.Range(-steps / 2, steps / 2);
                        }
                        break;
                    case 1:
                        tempX = 0 + Random.Range(-steps / 2, steps / 2);
                        break;
                    case 2:
                        tempX = steps + Random.Range(-steps / 2, steps / 2);
                        break;
                    default:
                        tempX = 0;
                        break;
                }
                
                GameObject tempPlush = Instantiate(plushie, new Vector3(tempX, 0, z), Quaternion.identity);
                switch (z)
                {
                    case -3:
                        tempPlush.GetComponent<SpriteRenderer>().color = lightColor; break;
                    case -2:
                        tempPlush.GetComponent<SpriteRenderer>().color = middleColor; break;
                    case -1:
                        tempPlush.GetComponent<SpriteRenderer>().color = darkColor; break;
                    default: tempPlush.GetComponent<SpriteRenderer>().color = Color.white; break;

                }
                tempPlush.GetComponent<PlushieScript>().SetPlushieType(plushieTypes.Last());
                plushieTypes.RemoveAt(plushieTypes.Count - 1);
                
            }
        }
    }

    private List<PlushieTypes> CreatePlushieList()
    {
        List<PlushieTypes> plushies = new List<PlushieTypes>();
        plushies.Add(PlushieTypes.Bear);
        plushies.Add(PlushieTypes.Bear);
        plushies.Add(PlushieTypes.Bear);
        plushies.Add(PlushieTypes.Bee);
        plushies.Add(PlushieTypes.Bee);
        plushies.Add(PlushieTypes.Bee);
        plushies.Add(PlushieTypes.Unicorn);
        plushies.Add(PlushieTypes.Unicorn);
        plushies.Add(PlushieTypes.Unicorn);
        
        return plushies.OrderBy(x=> new System.Random().Next()).ToList();;
    }
    private void SelectPlushieToGet()
    {
        var rand = Random.Range(0, 2);
        var allPlushies = new[] { PlushieTypes.Bear, PlushieTypes.Bee, PlushieTypes.Unicorn };
        _plushieTypeToGet = allPlushies[rand];
    }

    public PlushieTypes GetPlushieTypeToGet()
    {
        return _plushieTypeToGet;
    }

    public static PlushieOrganizerScript Instance()
    {
        return _instance;
    }
}

public enum PlushieTypes{
    Bear, Bee, Unicorn
}
