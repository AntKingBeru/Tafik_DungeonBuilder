using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float smoothTime = 0.2f;

    private Vector3 _velocity;

    private void LateUpdate()
    {
        if (!target)
            return;
        
        var targetPos = target.position;
        targetPos.z = -10f;

        transform.position = Vector3.SmoothDamp(
            transform.position,
            targetPos,
            ref _velocity,
            smoothTime
        );
    }
}