using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class BuildSystem : MonoBehaviour
{
    public static BuildSystem Instance { get; private set; }

    [Header("Parents")]
    [SerializeField] private Transform roomParent;
    
    [Header("Input Names")]
    [SerializeField] private string rotateActionName = "Rotate";
    [SerializeField] private string placeActionName = "Place";
    [SerializeField] private string cancelActionName = "Cancel";
    [SerializeField] private string exitActionName = "ExitBuildMode";
    
    private RoomInstance _currentRoomPrefab;
    private RoomInstance _previewRoom;
    
    private int _rotationIndex;
    
    private InputAction _rotateAction;
    private InputAction _placeAction;
    private InputAction _cancelAction;
    private InputAction _exitAction;
    
    private readonly List<Vector2Int> _highlightedCells = new();

    private void Awake()
    {
        Instance = this;
    }

    private void OnEnable()
    {
        _rotateAction = InputManager.Instance.GetBuildAction(rotateActionName);
        _placeAction = InputManager.Instance.GetBuildAction(placeActionName);
        _cancelAction = InputManager.Instance.GetBuildAction(cancelActionName);
        _exitAction = InputManager.Instance.GetBuildAction(exitActionName);
        
        _rotateAction.performed += OnRotate;
        _placeAction.performed += OnPlace;
        _cancelAction.performed += OnCancel;
        _exitAction.performed += OnExit;
    }
    
    private void OnDisable()
    {
        _rotateAction.performed -= OnRotate;
        _placeAction.performed -= OnPlace;
        _cancelAction.performed -= OnCancel;
        _exitAction.performed -= OnExit;
    }

    private void Update()
    {
        if (GridCursor.Instance.Mode != CursorMode.Build)
            return;
        
        UpdatePreviewPosition();
    }

    public void EnterBuildMode(RoomInstance roomPrefab)
    {
        _currentRoomPrefab = roomPrefab;

        _rotationIndex = 0;
        
        InputManager.Instance.EnableBuild();
        
        GridCursor.Instance.SetMode(CursorMode.Build);
        
        _previewRoom = Instantiate(roomPrefab, roomParent);
        
        _previewRoom.SetPreview(true);
    }

    public void ExitBuildMode()
    {
        ClearHighlights();
        
        if (_previewRoom)
            Destroy(_previewRoom.gameObject);
        
        GridCursor.Instance.SetMode(CursorMode.None);
        
        InputManager.Instance.EnablePlayer();
    }

    private void UpdatePreviewPosition()
    {
        ClearHighlights();
        
        var origin = GridCursor.Instance.CurrentCell;
        
        var size = GetRotatedSize(_previewRoom.Size);
        
        var worldPos = GetRoomWorldPosition(origin, size);
        
        _previewRoom.transform.position = worldPos;
        
        var valid = GridManager.Instance.CanPlaceRoom(origin, size);
        
        HighlightArea(origin, size, valid);

        SetPreviewColor(valid);
    }

    private void TryPlaceRoom()
    {
        var origin = GridCursor.Instance.CurrentCell;
        
        var size = GetRotatedSize(_previewRoom.Size);

        if (!GridManager.Instance.CanPlaceRoom(origin, size))
            return;

        var worldPos = GetRoomWorldPosition(origin, size);

        var room = Instantiate(
            _currentRoomPrefab,
            worldPos,
            _previewRoom.transform.rotation,
            roomParent
        );
        
        room.Initialize(origin);
        
        GridManager.Instance.RegisterRoom(room, origin, room.Size);
    }

    private void RotatePreview()
    {
        _rotationIndex = (_rotationIndex + 1) % 4;
        
        _previewRoom.transform.rotation = 
            Quaternion.Euler(0, 0, _rotationIndex * 90);
    }
    
    private Vector2Int GetRotatedSize(Vector2Int size)
    {
        if (_rotationIndex % 2 == 0)
            return size;
        
        return new Vector2Int(
            size.y,
            size.x
        );
    }
    
    private Vector3 GetRoomWorldPosition(Vector2Int origin, Vector2Int size)
    {
        return new Vector3(
            origin.x + size.x * 0.5f,
            origin.y + size.y * 0.5f,
            0f
        );
    }

    private void SetPreviewColor(bool valid)
    {
        var renderers = _previewRoom.GetComponentsInChildren<SpriteRenderer>();

        var c = valid ? Color.green : Color.red;
        c.a = 0.5f;
        
        foreach (var r in renderers)
            r.color = c;
    }

    private void HighlightArea(Vector2Int origin, Vector2Int size, bool valid)
    {
        var color = valid
            ? new Color(0f, 1f, 0f, 0.35f)
            : new Color(1f, 0f, 0f, 0.35f);

        for (var x = 0; x < size.x; x++)
        {
            for (var y = 0; y < size.y; y++)
            {
                var pos = origin + new Vector2Int(x, y);
                
                GridManager.Instance.HighlightCell(pos, color);
                
                _highlightedCells.Add(pos);
            }
        }
    }

    private void ClearHighlights()
    {
        foreach (var cell in _highlightedCells)
            GridManager.Instance.ClearHighlight(cell);
        
        _highlightedCells.Clear();
    }
    
    private void OnRotate(InputAction.CallbackContext context)
    {
        RotatePreview();
    }

    private void OnPlace(InputAction.CallbackContext context)
    {
        TryPlaceRoom();
    }
    
    private void OnCancel(InputAction.CallbackContext context)
    {
        if (_previewRoom)
            Destroy(_previewRoom.gameObject);

        _previewRoom = Instantiate(_currentRoomPrefab, roomParent);
        _previewRoom.SetPreview(true);
    }
    
    private void OnExit(InputAction.CallbackContext context)
    {
        ExitBuildMode();
    }
}