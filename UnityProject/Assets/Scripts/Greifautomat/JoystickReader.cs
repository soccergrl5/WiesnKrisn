using UnityEngine;

public class JoystickReader : MonoBehaviour
{
    public   Vector2 touchDirection = Vector2.zero;
    [SerializeField] private GameObject grabber;
    private float _xCor = 0;
    private float _zCor = 0;
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
        //We need to make sure the z-coordinate sets it only to values -1, -2, -3
        _zCor = CalculateZCoordinate();
        GrabberScript.GetInstance().ChangeColorAccordingToDimension();
        
        _xCor = touchDirection.x * CameraCornersScript.Instance.GetMaxX();
        grabber.transform.position = new Vector3(_xCor, grabber.transform.position.y, _zCor);
    }

    private float CalculateZCoordinate()
    {
        if (touchDirection.y < 1 && touchDirection.y > 0.3)
        {
            return -1;
        } if (touchDirection.y <= 0.3 && touchDirection.y > -0.3)
        {
             return -2;
        }

        if (touchDirection.y <= -0.3 && touchDirection.y > -1)
        {
            return -3;
        }

        return 0;
    }
}