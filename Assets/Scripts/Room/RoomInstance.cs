using UnityEngine;
using System.Linq;

public class RoomInstance : MonoBehaviour
{
    public RoomData Data { get; private set; }
    
    [SerializeField] private RoomSpawnPoint[] spawnPoints;

    private Vector2Int _center;
    private Vector2Int _origin;
    private Vector2Int _size;
    
    public void Initialize(RoomData data, Vector2Int center, Vector2Int size)
    {
        Data = data;
        _center = center;
        _origin = center - new Vector2Int(
            (size.x - 1) / 2,
            (size.y - 1) / 2
        );
        _size = size;
    }

    public Transform GetSpawnPoint(SpawnPointType type)
    {
        var point = spawnPoints.FirstOrDefault(p => p.type == type);
        
        return !point ? null : point.transform;
    }

    public bool IsEdgeRoom()
    {
        var pos = GridManager.Instance.WorldToGrid(transform.position);

        return pos.x == 0 ||
               pos.y == 0 ||
               pos.x == GridManager.Instance.Width - 1 ||
               pos.y == GridManager.Instance.Height - 1;
    }

    public Vector3 GetRandomPositionInside()
    {
        var x = Random.Range(0, _size.x);
        var y = Random.Range(0, _size.y);
        
        var gridPos = _origin + new Vector2Int(x, y);
        
        return GridManager.Instance.GridToWorld(gridPos);
    }

    public Vector2Int GetRandomCellPosition()
    {
        var offsetX = Random.Range(0, _size.x);
        var offsetY = Random.Range(0, _size.y);
        
        return _origin + new Vector2Int(offsetX, offsetY);
    }
    
    public Vector2Int GetCenter() => _center;
    public Vector2Int GetOrigin() => _origin;
    public Vector2Int GetSize() => _size;
}