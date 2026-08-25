using UnityEngine;

public class CameraCornersScript : MonoBehaviour
{
    public Camera cam;
    public static float MinX;
    public static float MaxX;
    public static float MinY;
    public static float MaxY;

    void Start()
    {
        // Ensure camera is assigned
        if (cam == null)
        {
            cam = Camera.main;
        }

        // Get the near plane corners
        var bottomLeft = cam.ViewportToWorldPoint(new Vector3(0, 0, -6));
        var topRight = cam.ViewportToWorldPoint(new Vector3(1, 1, -6));

        MinX = topRight.x / 2;
        MaxX = bottomLeft.x / 2;
        MinY = bottomLeft.y / 2;
        MaxY = topRight.y / 2;

    }
}
