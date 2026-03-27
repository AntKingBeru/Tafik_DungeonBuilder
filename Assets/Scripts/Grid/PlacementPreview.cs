using UnityEngine;

public class PlacementPreview : MonoBehaviour
{
    [SerializeField] private SpriteRenderer visual;
    [SerializeField] private Vector2Int size = new(3, 3);

    private bool _isValidPlacement;
    private int _rotationIndex;
    private Vector2Int _currentOrigin;

    public void UpdatePreview(Vector2Int center)
    {
        var rotatedSize = GetSize();

        _currentOrigin = center - new Vector2Int(
            (rotatedSize.x - 1) / 2,
            (rotatedSize.y - 1) / 2
        );
        
        transform.position = GridManager.Instance.GridToWorld(center);
        
        _isValidPlacement = GridManager.Instance.CanPlaceRoom(_currentOrigin, rotatedSize);

        UpdateColor();
    }
    
    private void UpdateColor()
    {
        visual.color = _isValidPlacement ?
            new Color(0f, 1f, 0f , 0.3f) :
            new Color(1f, 0f, 0f , 0.3f);
    }

    public void Rotate()
    {
        _rotationIndex = (_rotationIndex + 1) % 4;
        transform.rotation = Quaternion.Euler(0f, 0f, _rotationIndex * -90f);
    }

    public Vector2Int GetSize()
    {
        return _rotationIndex % 2 == 0 ? size : new Vector2Int(size.y, size.x);
    }

    public void SetSize(Vector2Int newSize)
    {
        size = newSize;
    }
    
    public bool CanPlace() => _isValidPlacement;
    public Vector2Int GetOrigin() => _currentOrigin;
}