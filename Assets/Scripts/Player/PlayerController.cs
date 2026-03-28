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
    [SerializeField] private InputActionReference confirmAction;
    [SerializeField] private InputActionReference rotateAction;
    
    [Header("References")]
    [SerializeField] private PlayerMovement movement;
    [SerializeField] private PlayerStateMachine stateMachine;
    [SerializeField] private PlayerInteractor interactor;
    [SerializeField] private PlayerCarry carry;
    
    [Header("Placement")]
    [SerializeField] private PlacementPreview previewPrefab;
    
    private PlacementPreview _preview;
    
    private RoomData _selectedRoom;
    private TrapData _selectedTrap;
    
    private Vector2 _moveInput;

    private void Start()
    {
        _preview = Instantiate(previewPrefab);
        _preview.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        EnableActions();

        moveAction.action.performed += OnMove;
        moveAction.action.canceled += OnMove;
        
        toggleModeAction.action.performed += _ => stateMachine.CycleBuildMode();
        cancelAction.action.performed += _ => stateMachine.ReturnToGameplay();
        
        confirmAction.action.performed += OnConfirm;
        rotateAction.action.performed += _ =>  _preview.Rotate();
    }

    private void OnDisable()
    {
        moveAction.action.performed -= OnMove;
        moveAction.action.canceled -= OnMove;
        confirmAction.action.performed -= OnConfirm;

        DisableActions();
    }

    private void Update()
    {
        HandleMovement();
        HandlePreview();
    }
    
    #region Movement

    private void HandleMovement()
    {
        if (stateMachine.IsInGameplay)
            movement.SetMovementInput(_moveInput);
        else
            movement.StopMovement();
    }
    
    private void OnMove(InputAction.CallbackContext context)
    {
        _moveInput = context.ReadValue<Vector2>();
    }
    
    #endregion
    
    #region Confirm

    private void OnConfirm(InputAction.CallbackContext context)
    {
        if (TryDropCorpse())
            return;
        
        if (TryPickupCorpse())
            return;

        switch (stateMachine.CurrentMode)
        {
            case PlayerMode.Clear:
                HandleClear();
                break;
            case PlayerMode.Build:
                HandleBuildRoom();
                break;
            case PlayerMode.BuildTrap:
                HandleBuildTrap();
                break;
        }
    }
    
    #endregion
    
    #region Actions

    private void HandleClear()
    {
        var pos = interactor.GetHoveredGridPosition();
        GridManager.Instance.ClearCell(pos);
    }

    private void HandleBuildRoom()
    {
        if (!_selectedRoom || !_preview.CanPlace())
            return;

        var center = interactor.GetHoveredGridPosition();
        
        GridManager.Instance.PlaceRoom(center, _preview.GetSize(), _selectedRoom);
    }

    private void HandleBuildTrap()
    {
        if (!_selectedTrap)
            return;

        var anchor = interactor.GetHoveredAnchor();

        if (!anchor || anchor.IsOccupied)
            return;
        
        anchor.PlaceTrap(_selectedTrap.prefab, _selectedTrap);
    }

    private bool TryDropCorpse()
    {
        if (!carry.HasCorpse)
            return false;

        var dropZone = interactor.GetHoveredDropZone();
        
        if (!dropZone)
            return false;

        var corpse = carry.Drop();
        
        dropZone.ReceiveCorpse(corpse);
        
        return true;
    }

    private bool TryPickupCorpse()
    {
        if (carry.HasCorpse)
            return false;

        var corpse = interactor.GetHoveredCorpse();

        if (!corpse)
            return false;
        
        carry.PickUp(corpse);
        return true;
    }
    
    #endregion
    
    #region Preview

    private void HandlePreview()
    {
        if (stateMachine.CurrentMode != PlayerMode.Build || !_selectedRoom)
        {
            if (_preview.gameObject.activeSelf)
                _preview.gameObject.SetActive(false);

            return;
        }
        
        if (!_preview.gameObject.activeSelf)
            _preview.gameObject.SetActive(true);

        var gridPos = interactor.GetHoveredGridPosition();
        _preview.UpdatePreview(gridPos);
    }
    
    #endregion
    
    #region External

    public void SetSelectedRoom(RoomData room)
    {
        _selectedRoom = room;
        _selectedTrap = null;
    }
    
    public void SetSelectedTrap(TrapData trap)
    {
        _selectedRoom = null;
        _selectedTrap = trap;
    }
    
    #endregion
    
    #region Helpers

    private void EnableActions()
    {
        moveAction.action.Enable();
        toggleModeAction.action.Enable();
        cancelAction.action.Enable();
        confirmAction.action.Enable();
        rotateAction.action.Enable();
    }

    private void DisableActions()
    {
        moveAction.action.Disable();
        toggleModeAction.action.Disable();
        cancelAction.action.Disable();
        confirmAction.action.Disable();
        rotateAction.action.Disable();   
    }
    
    #endregion
}