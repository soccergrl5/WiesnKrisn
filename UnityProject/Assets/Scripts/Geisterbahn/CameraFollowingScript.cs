using UnityEngine;

public class CameraFollowing : MonoBehaviour
{
    [SerializeField] private GameObject target;
    public Vector3 offset = new Vector3(0, 5, -10);
    public float smoothTime = 0.3f;
    private Vector3 _velocity = Vector3.zero;

    
    public void LateUpdate()
    {
        Vector3 targetPosition = target.transform.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref _velocity, smoothTime);
        
        transform.LookAt(target.transform);
    }
}
