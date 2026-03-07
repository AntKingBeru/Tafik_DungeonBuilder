using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")] 
    [SerializeField] private float moveSpeed = 1.5f;
    
    [Header("References")]
    [SerializeField] private Rigidbody2D rigidBody;
    [SerializeField] private DungeonGrid dungeonGrid;
    
    private PlayerControls _controls;
    
    private Vector2 _moveInput;
    private Vector2 _moveVelocity;

    public bool IsMoving { get; private set; }

    private void Awake()
    {
        _controls = new PlayerControls();
    }

    private void OnEnable()
    {
        _controls.Enable();

        _controls.Player.Move.performed += OnMove;
        _controls.Player.Move.canceled += OnMove;
    }
    
    private void OnDisable()
    {
        _controls.Player.Move.performed -= OnMove;
        _controls.Player.Move.canceled -= OnMove;
        
        _controls.Disable();
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        _moveInput = context.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        _moveVelocity = _moveInput.normalized * moveSpeed;
        
        var targetPosition = rigidBody.position + _moveVelocity * Time.fixedDeltaTime;

        if (!IsPositionInsideDungeon(targetPosition))
        {
            rigidBody.linearVelocity = Vector2.zero;
            return;
        }
        
        rigidBody.linearVelocity = _moveVelocity;
        
        IsMoving = _moveInput.sqrMagnitude > 0.01f;
    }

    private bool IsPositionInsideDungeon(Vector2 worldPos2D)
    {
        var worldPos3D = new Vector3(worldPos2D.x, 0f, worldPos2D.y);
        
        var gridPos = dungeonGrid.WorldToGrid(worldPos3D);
        
        return dungeonGrid.IsTileWalkable(gridPos);
    }
}