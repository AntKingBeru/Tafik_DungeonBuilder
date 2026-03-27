using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float acceleration = 15f;
    [SerializeField] private float deceleration = 20f;
    
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;
    
    private Vector2 _currentVelocity;
    private Vector2 _targetVelocity;

    private void FixedUpdate()
    {
        var accelRate = (_targetVelocity.magnitude > 0.1f) ? acceleration : deceleration;

        _currentVelocity = Vector2.Lerp(
            rb.linearVelocity,
            _targetVelocity,
            accelRate * Time.fixedDeltaTime
        );
        
        rb.linearVelocity = _currentVelocity;
    }
    
    #region Public API
    
    public void SetMovementInput(Vector2 input)
    {
        _targetVelocity = input * moveSpeed;
    }

    public void StopMovement()
    {
        _targetVelocity = Vector2.zero;
    }
    
    #endregion
}