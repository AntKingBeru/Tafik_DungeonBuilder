using UnityEngine;
using System.Collections.Generic;

public class DungeonGrid : MonoBehaviour
{
    [Header("Grid Settings")]
    [SerializeField] private Vector2 gridSize = new Vector2(15, 15);
    [SerializeField] private float tileSize = 1f;

    [Header("Room Placement")]
    [SerializeField] private Transform roomParent;
    
    private HashSet<Vector2Int> _occupiedTiles = new HashSet<Vector2Int>();

    public bool IsTileWalkable(Vector2Int tile)
    {
        return !IsTileOccupied(tile);
    }
    
    #region Grid Conversion
    
    public Vector2Int WorldToGrid(Vector3 worldPosition)
    {
        var x = Mathf.FloorToInt(worldPosition.x / tileSize);
        var y = Mathf.FloorToInt(worldPosition.y / tileSize);
        
        return new Vector2Int(x, y);
    }

    public Vector3 GridToWorld(Vector2Int gridPosition)
    {
        return new Vector3(
            gridPosition.x * tileSize,
            0f,
            gridPosition.y * tileSize
        );
    }
    
    #endregion
    
    #region Bounds
    
    public bool IsInsideGrid(Vector2Int pos)
    {
        return pos.x >= 0 &&
               pos.y >= 0 &&
               pos.x < gridSize.x &&
               pos.y < gridSize.y;
    }
    
    #endregion
    
    #region Occupancy

    public bool IsTileOccupied(Vector2Int pos)
    {
        return _occupiedTiles.Contains(pos);
    }

    public void SetTileOccupied(Vector2Int pos)
    {
        _occupiedTiles.Add(pos);
    }
    
    #endregion
    
    #region Room Placement
    
    public bool CanPlaceRoom(Vector2Int origin, Vector2Int roomSize)
    {
        foreach (var tile in GridRectangleIterator.Iterate(origin, roomSize.x, roomSize.y))
        {
            if (!IsInsideGrid(tile)) return false;
            
            if (IsTileOccupied(tile)) return false;
        }
        
        return true;
    }

    public void PlaceRoom(Vector2Int origin, Vector2Int roomSize, GameObject roomPrefab)
    {
        if (!CanPlaceRoom(origin, roomSize)) return;

        foreach (var tile in GridRectangleIterator.Iterate(origin, roomSize.x, roomSize.y))
        {
            SetTileOccupied(tile);
        }
        
        var worldPos = GridToWorld(origin);
        var room = Instantiate(roomPrefab, worldPos, Quaternion.identity);
        
        if (roomParent != null) room.transform.SetParent(roomParent);
    }
    
    #endregion
    
    #region Debug
    #if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.gray;

        for (var x = 0; x < gridSize.x; x++)
        {
            var start = new Vector3(x * tileSize, 0f, 0f);
            var end = new Vector3(x * tileSize, 0f, gridSize.y * tileSize);
            
            Gizmos.DrawLine(start, end);
        }

        for (var y = 0; y < gridSize.y; y++)
        {
            var start = new Vector3(0f, y * tileSize, 0f);
            var end = new Vector3(gridSize.x * tileSize, 0f, y * tileSize);
            
            Gizmos.DrawLine(start, end);
        }
    }
    #endif
    #endregion
}