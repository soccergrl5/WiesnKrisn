using UnityEngine;

public class JoystickReader : MonoBehaviour
{
    public   Vector2 touchDirection = Vector2.zero;
    [SerializeField] private GameObject grabber;
    private void Start()
    {
        //Subscribe to the action in JoyStick.cs
        JoystickScript.OnJoyStickMoved += GetJoyStickDirection;
    }
 
    void GetJoyStickDirection(Vector2 touchPosition)
    {
        //Touch direction updating every time joystick is moved.
        touchDirection = touchPosition;
        
        //should adapt the position of the grabber
        //We need to make sure the z-coordinate sets it only to values -1, -2, -3, -4, -5
        var zCor = 0;
        var xCor = touchDirection.x * 7;
        if (touchDirection.y < 1 && touchDirection.y > 0.6)
        {
            zCor = -1;
        } else if (touchDirection.y <= 0.6 && touchDirection.y > 0.2)
        {
            zCor = -2;
        }else if (touchDirection.y <= 0.2 && touchDirection.y > -0.2)
        {
            zCor = -3;
        }else if (touchDirection.y <= -0.2 && touchDirection.y > -0.6)
        {
            zCor = -4;
        }else if (touchDirection.y <= -0.6 && touchDirection.y > -1)
        {
            zCor = -5;
        }
        print(grabber.transform.position);
        grabber.transform.position = new Vector3(xCor, grabber.transform.position.y, zCor);
    }
}