using UnityEngine;

public class PlacementPreview : MonoBehaviour
{
    private const string RoomLayer = "Room";
    private const string TrapLayer = "Trap";
    
    [SerializeField] private SpriteRenderer visual;
    
    private Vector2Int _size;
    private bool _isValidPlacement;
    private int _rotationIndex;
    private Vector2Int _currentOrigin;

    private bool _isTrap;
    
    #region Setup

    public void SetRoom(RoomData room)
    {
        _isTrap = false;
        _size = room.size;
        
        ResetRotation();
        UpdateScale(GetSize());
        
        visual.sortingLayerName = RoomLayer;
        visual.sortingOrder = 5;
    }

    public void SetTrap(TrapData trap)
    {
        _isTrap = true;
        _size = new Vector2Int(
            Mathf.RoundToInt(trap.size.x),
            Mathf.RoundToInt(trap.size.y)
        );
        
        ResetRotation();
        UpdateScale(_size);
        
        visual.sortingLayerName = TrapLayer;
        visual.sortingOrder = 5;
    }
    
    public void ResetRotation()
    {
        _rotationIndex = 0;
        transform.rotation = Quaternion.identity;
    }
    
    #endregion
    
    #region Update

    public void UpdatePreview(Vector2Int center)
    {

        if (_isTrap)
        {
            UpdateTrapPreview(center);
            return;
        }
        
        UpdateRoomPreview(center);
    }

    private void UpdateRoomPreview(Vector2Int center)
    {
        var rotatedSize = GetSize();

        _currentOrigin = center - new Vector2Int(
            (rotatedSize.x - 1) / 2,
            (rotatedSize.y - 1) / 2
        );
        
        transform.position = GridManager.Instance.GridToWorld(center);
        
        UpdateScale(rotatedSize);
        
        _isValidPlacement = GridManager.Instance.CanPlaceRoom(_currentOrigin, rotatedSize);

        UpdateColor();
    }

    private void UpdateTrapPreview(Vector2Int gridPos)
    {
        var world = GridManager.Instance.GridToWorld(gridPos);
        
        var anchor = GetAnchorAtPosition(world);

        if (anchor)
        {
            transform.position = anchor.transform.position;
            _isValidPlacement = !anchor.IsOccupied;
            Debug.DrawLine(transform.position, anchor.transform.position, Color.green);
        }
        else
        {
            transform.position = world;
            _isValidPlacement = false;
        }
        
        UpdateColor();
    }
    
    #endregion
    
    #region Helpers
    
    private void UpdateScale(Vector2Int newSize)
    {
        var cellSize = GridManager.Instance.CellSize;

        visual.transform.localScale = new Vector3(
            newSize.x * cellSize,
            newSize.y * cellSize,
            1f
        );
    }
    
    private void UpdateColor()
    {
        visual.color = _isValidPlacement ?
            new Color(0f, 1f, 0f , 0.3f) :
            new Color(1f, 0f, 0f , 0.3f);
    }

    private TrapAnchor GetAnchorAtPosition(Vector2 worldPos)
    {
        var hit = Physics2D.Raycast(worldPos, Vector2.zero);
        return hit.collider ? hit.collider.GetComponent<TrapAnchor>() : null;
    }
    
    public Vector2Int GetSize()
    {
        return _rotationIndex % 2 == 0
            ? _size
            : new Vector2Int(_size.y, _size.x);
    }

    public void Rotate()
    {
        if (_isTrap)
            return;
        
        _rotationIndex = (_rotationIndex + 1) % 4;
        transform.rotation = Quaternion.Euler(0f, 0f, _rotationIndex * -90f);
    }

    public void SetSize(Vector2Int newSize)
    {
        _size = newSize;
    }
    
    public bool CanPlace() => _isValidPlacement;
    public Vector2Int GetOrigin() => _currentOrigin;
    
    #endregion
}