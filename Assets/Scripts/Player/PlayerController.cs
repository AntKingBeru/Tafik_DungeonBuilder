using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(PlayerStateMachine))]
public class PlayerController : MonoBehaviour
{
    [Header("Input")]
    [SerializeField] private InputActionReference moveAction;
    [SerializeField] private InputActionReference toggleModeAction;
    [SerializeField] private InputActionReference cancelAction;
    [SerializeField] private InputActionReference rotateAction; // Testing
    [SerializeField] private InputActionReference confirmAction; // Testing
    
    [Header("References")]
    [SerializeField] private PlayerMovement movement;
    [SerializeField] private PlayerStateMachine stateMachine;
    [SerializeField] private PlayerCarry carry;
    [SerializeField] private PlayerInteractor interactor; // Testing
    [SerializeField] private PlacementPreview previewPrefab; // Testing

    private Vector2 _moveInput;
    private RoomData _selectedRoom;
    
    private PlacementPreview _currentPreview; // Testing

    private void Start()
    {
        _currentPreview = Instantiate(previewPrefab);
        _currentPreview.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        EnableActions();

        moveAction.action.performed += OnMove;
        moveAction.action.canceled += OnMove;

        toggleModeAction.action.performed += OnToggleMode;
        cancelAction.action.performed += OnCancel;
        
        // Testing
        confirmAction.action.Enable();
        confirmAction.action.performed += OnConfirm;
        rotateAction.action.Enable();
        rotateAction.action.performed += OnRotate;
    }

    private void OnDisable()
    {
        moveAction.action.performed -= OnMove;
        moveAction.action.canceled -= OnMove;

        toggleModeAction.action.performed -= OnToggleMode;
        cancelAction.action.performed -= OnCancel;

        DisableActions();
        
        // Testing
        confirmAction.action.performed -= OnConfirm;
        confirmAction.action.Disable();
        rotateAction.action.performed -= OnRotate;
        rotateAction.action.Disable();
    }

    private void Update()
    {
        if (stateMachine.IsInGameplay)
            movement.SetMovementInput(_moveInput);
        else
            movement.StopMovement();

        HandlePreview();
    }
    
    #region Input Handlers

    private void OnMove(InputAction.CallbackContext context)
    {
        _moveInput = context.ReadValue<Vector2>();
    }

    private void OnToggleMode(InputAction.CallbackContext context)
    {
        stateMachine.CycleBuildMode();
    }

    private void OnCancel(InputAction.CallbackContext context)
    {
        stateMachine.ReturnToGameplay();
    }
    
    #endregion
    
    #region Action Management

    private void EnableActions()
    {
        moveAction.action.Enable();
        toggleModeAction.action.Enable();
        cancelAction.action.Enable();
    }

    private void DisableActions()
    {
        moveAction.action.Disable();
        toggleModeAction.action.Disable();
        cancelAction.action.Disable();
    }
    
    #endregion

    public void SetSelectedRoom(RoomData room)
    {
        _selectedRoom = room;
        
        if (_currentPreview)
            _currentPreview.SetSize(room.size);
    }

    private void OnConfirm(InputAction.CallbackContext context)
    {
        if (stateMachine.CurrentMode == PlayerMode.Clear)
        {
            var clearPos = interactor.GetHoveredGridPosition();
            GridManager.Instance.ClearCell(clearPos);
            return;
        }
        
        var corpse = interactor.GetHoveredCorpse();

        var revival = FindFirstObjectByType<RevivalController>();
        
        if (revival)
            revival.TryRevive();
        
        if (corpse)
        {
            if (!carry.HasCorpse)
            {
                carry.PickUp(corpse);
                return;
            }
        }
        else if (carry.HasCorpse)
        {
            carry.Drop();
            return;
        }
        
        if (stateMachine.CurrentMode != PlayerMode.Build)
            return;

        if (!_currentPreview.CanPlace())
            return;
        
        var center = interactor.GetHoveredGridPosition();

        if (!_selectedRoom)
            return;
        
        var size = _selectedRoom.size;

        if (!ResourceManager.Instance.TrySpend(_selectedRoom.stoneCost, _selectedRoom.woodCost))
            return;

        GridManager.Instance.PlaceRoom(
            center,
            size,
            _selectedRoom
        );
    }

    private void OnRotate(InputAction.CallbackContext context)
    {
        if (stateMachine.CurrentMode != PlayerMode.Build)
            return;
        
        _currentPreview.Rotate();
    }

    private void HandlePreview()
    {
        if (stateMachine.CurrentMode == PlayerMode.Build)
        {
            if (!_currentPreview.gameObject.activeSelf)
                _currentPreview.gameObject.SetActive(true);

            var gridPos = interactor.GetHoveredGridPosition();
            _currentPreview.UpdatePreview(gridPos);
            Debug.Log($"Mouse Grid: {gridPos}");
            Debug.Log($"Origin: {_currentPreview.GetOrigin()}");
        }
        else
            if (_currentPreview.gameObject.activeSelf)
                _currentPreview.gameObject.SetActive(false);
    }
}