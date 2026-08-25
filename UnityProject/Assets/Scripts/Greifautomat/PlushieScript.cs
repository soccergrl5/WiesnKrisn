using UnityEngine;

public class PlushieSkript : MonoBehaviour
{
    [SerializeField] private GameObject plushie;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DividePlushiesEvenly();
        
    }

    private void DividePlushiesEvenly()
    {
        //We want 9 plushies on 3 z layers
        for (int x = 0; x < 3; x++)
        {
            for (int z = -3; z < 0; z++)
            {
                //We need to make sure to stay within the borders of the camera
                var minX = CameraCornersScript.MinX;
                var maxX = CameraCornersScript.MaxX;
                
                var steps = (maxX - minX) / 3; //The 3 is not variable, since we want 3*3 plushies
                
                float tempX = 0;
                switch (x)
                {
                    //To add some variability, we add a random number to x
                    case 0:
                        tempX = -steps + Random.Range(-steps/2, steps/2);
                        break;
                    case 1:
                        tempX = 0 + Random.Range(-steps/2, steps/2);
                        break;
                    case 2:
                        tempX = steps + Random.Range(-steps/2, steps/2);
                        break;
                    default: 
                        tempX = 0;
                        break;
                }
                Instantiate(plushie, new Vector3(tempX, 0.2f, z), Quaternion.identity);
            }
        }
    }
}
