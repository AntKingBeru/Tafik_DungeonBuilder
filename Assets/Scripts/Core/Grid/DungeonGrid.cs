using UnityEngine;
using System;

public class DungeonGrid : MonoBehaviour
{
    [Header("Grid Settings")]
    [SerializeField] private int width = 20;
    [SerializeField] private int height = 20;
    [SerializeField] private float tileSize = 1f;
    
    private DungeonTile[,] _grid;
    
    public int Width => width;
    public int Height => height;
    public float TileSize => tileSize;
    
    public event Action<DungeonTile> OnTileChanged;

    private void Awake()
    {
        GenerateGrid();
    }
    
    #region Grid Generation

    public void GenerateGrid()
    {
        _grid = new DungeonTile[width, height];

        for (var x = 0; x < width; x++)
        {
            for (var y = 0; y < height; y++)
            {
                var pos = new Vector2Int(x, y);

                var resourceType = GetRandomResourceType();
                
                // Start everything as blocked
                _grid[x, y] = new DungeonTile(pos, TileState.Blocked, resourceType);
            }
        }
    }

    private ResourceType GetRandomResourceType()
    {
        return UnityEngine.Random.value > 0.05f
            ? ResourceType.Stone
            : ResourceType.Wood;
    }
    
    #endregion
    
    #region Tile Access

    public DungeonTile GetTile(Vector2Int pos)
    {
        return !IsInside(pos) ? null : _grid[pos.x, pos.y];
    }

    public bool IsInside(Vector2Int pos)
    {
        return pos.x >= 0 && pos.x < width&&
               pos.y >= 0 && pos.y < height;
    }

    public bool IsEdge(Vector2Int pos)
    {
        return pos.x == 0 ||
               pos.x == width - 1 ||
               pos.y == 0 ||
               pos.y == height - 1;
    }
    
    #endregion
    
    #region Tile Modification

    public void SetTileState(Vector2Int pos, TileState newState)
    {
        var tile = GetTile(pos);
        if (tile == null)
            return;

        if (tile.state == newState)
            return;
        
        tile.state = newState;
        OnTileChanged?.Invoke(tile);
    }

    public void SetOccupier(Vector2Int pos, object occupier)
    {
        var tile = GetTile(pos);
        if (tile == null)
            return;
        
        tile.Occupier = occupier;
        tile.state = occupier == null ? TileState.Cleared : TileState.Occupied;
        
        OnTileChanged?.Invoke(tile);
    }
    
    #endregion
    
    #region Utility
    
    public Vector3 GridToWorld(Vector2Int gridPos)
    {
        return new Vector3(
            gridPos.x * tileSize,
            gridPos.y * tileSize,
            0f
        );
    }

    public Vector2Int WorldToGrid(Vector3 worldPos)
    {
        return new Vector2Int(
            Mathf.FloorToInt(worldPos.x / tileSize),
            Mathf.FloorToInt(worldPos.y / tileSize)
        );
    }
    
    #endregion
}