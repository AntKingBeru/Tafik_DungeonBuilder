using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float smoothTime = 0.2f;

    [Header("Bounds")]
    [SerializeField] private Vector2 minBounds;
    [SerializeField] private Vector2 maxBounds;
    
    [Header("Pixel Perfect")]
    [SerializeField] private int pixelPerUnit = 96;

    private Vector3 _offset;
    private Vector3 _velocity = Vector3.zero;

    private void Start()
    {
        _offset = transform.position - target.position;
    }

    private void LateUpdate()
    {
        Follow();
    }

    private void Follow()
    {
        var desiredPosition = target.position + _offset;
        
        var clampedX = Mathf.Clamp(desiredPosition.x, minBounds.x, maxBounds.x);
        var clampedY = Mathf.Clamp(desiredPosition.y, minBounds.y, maxBounds.y);
        
        var clampedPosition = new Vector3(clampedX, clampedY, transform.position.z);

        var smoothedPosition = Vector3.SmoothDamp(
            transform.position,
            clampedPosition,
            ref _velocity,
            smoothTime
        );
        
        var unitsPerPixel = 1f / pixelPerUnit;
        
        smoothedPosition.x = Mathf.Round(smoothedPosition.x / unitsPerPixel) * unitsPerPixel;
        smoothedPosition.y = Mathf.Round(smoothedPosition.y / unitsPerPixel) * unitsPerPixel;
        
        transform.position = smoothedPosition;
    }
}