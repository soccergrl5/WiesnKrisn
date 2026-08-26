using UnityEngine;

public class CameraCornersScript : MonoBehaviour
{
    public Camera cam;
    private float MinX {get; set; }
    private float MaxX { get; set; }
    private float MinY { get; set; }
    private float MaxY { get; set; }
    public static CameraCornersScript Instance;

    void Awake()
    {
        // Ensure camera is assigned
        if (cam == null)
        {
            cam = Camera.main;
        }

        if (Instance == null)
        {
            Instance = this;
        }
        
        InitializeCorners();
    }

    private void InitializeCorners()
    {
        // Get the near plane corners
        var bottomLeft = cam.ViewportToWorldPoint(new Vector3(0, 0, -6));
        var topRight = cam.ViewportToWorldPoint(new Vector3(1, 1, -6));

        MinX = topRight.x / 2;
        MaxX = bottomLeft.x / 2;
        MinY = bottomLeft.y / 2;
        MaxY = topRight.y / 2;
    }

    public float GetMinX()
    {
        return MinX;
    }

    public float GetMaxX()
    {
        return MaxX;
    }
}
