using UnityEngine;
using System.Collections.Generic;

public class GridManager : MonoBehaviour
{
    public static GridManager Instance { get; private set; }

    [Header("Dungeon Settings")]
    [SerializeField] private int width = 50;
    [SerializeField] private int height = 50;
    [SerializeField] private float woodChance = 0.25f;
    
    [Header("Tile View")]
    [SerializeField] private DungeonCellView cellPrefab;
    [SerializeField] private Transform tilesParent;

    private readonly Dictionary<Vector2Int, DungeonCell> _grid = new();
    private readonly Dictionary<Vector2Int, DungeonCellView> _views = new();
    
    public int Width => width;
    public int Height => height;
    
    public IReadOnlyDictionary<Vector2Int, DungeonCell> Grid => _grid;

    private void Awake()
    {
        if (Instance && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        
        Instance = this;
        
        GenerateGrid();
        SpawnTileVisuals();
    }
    
    #region Grid Generation

    private void GenerateGrid()
    {
        _grid.Clear();
        
        for (var x = 0; x < width; x++)
        {
            for (var y = 0; y < height; y++)
            {
                var pos = new Vector2Int(x, y);

                var type = 
                    Random.value < woodChance
                    ? ResourceType.Wood
                    : ResourceType.Stone;
                
                _grid[pos] = new DungeonCell(pos, type);
            }
        }
    }

    private void SpawnTileVisuals()
    {
        foreach (var pair in _grid)
        {
            var cell = pair.Value;
            
            var world = CellToWorld(cell.GridPosition);

            var view = Instantiate(
                cellPrefab,
                world,
                Quaternion.identity,
                tilesParent
            );
            
            view.Initialize(cell);
            
            _views[cell.GridPosition] = view;
        }
    }
    
    #endregion
    
    #region Cell Access
    
    public DungeonCell GetCell(Vector2Int pos)
    {
        _grid.TryGetValue(pos, out var cell);
        return cell;
    }

    public bool IsInsideGrid(Vector2Int pos)
    {
        return pos.x >= 0 &&
               pos.y >= 0 &&
               pos.x < width &&
               pos.y < height;
    }
    
    #endregion
    
    #region Clearing System
    
    public bool ClearCell(Vector2Int pos)
    {
        var cell = GetCell(pos);
        
        if (cell == null)
            return false;

        if (cell.IsCleared)
            return false;
        
        cell.Clear();
        
        ResourceManager.Instance.AddResource(cell.ResourceType, 1);
        
        if (_views.TryGetValue(pos, out var view))
            view.UpdateVisual();
        
        return true;
    }
    
    public void ClearArea(Vector2Int origin, Vector2Int size)
    {
        for (var x = 0; x < size.x; x++)
        {
            for (var y = 0; y < size.y; y++)
            {
                var pos = origin + new Vector2Int(x, y);
                ClearCell(pos);
            }
        }
    }
    
    #endregion
    
    #region Room Placement
    
    public bool CanPlaceRoom(Vector2Int origin, Vector2Int size)
    {
        for (var x = 0; x < size.x; x++)
        {
            for (var y = 0; y < size.y; y++)
            {
                var pos = origin + new Vector2Int(x, y);

                if (!IsInsideGrid(pos))
                    return false;
                
                var cell = GetCell(pos);
                
                if (!cell.IsCleared)
                    return false;
                if (cell.Room)
                    return false;
            }
        }

        return true;
    }
    
    public void RegisterRoom(RoomInstance room, Vector2Int origin, Vector2Int size)
    {
        for (var x = 0; x < size.x; x++)
        {
            for (var y = 0; y < size.y; y++)
            {
                var pos = origin + new Vector2Int(x, y);
                
                var cell = GetCell(pos);

                if (cell != null)
                    cell.Room = room;

                if (_views.TryGetValue(pos, out var view))
                {
                    var col = view.GetComponent<BoxCollider2D>();
                    if (col)
                        col.enabled = false;
                }
            }
        }
    }
    
    #endregion
    
    #region Highlight System

    public void HighlightCell(Vector2Int pos, Color color)
    {
        if (_views.TryGetValue(pos, out var view))
            view.SetHighlight(color);
    }

    public void ClearHighlight(Vector2Int pos)
    {
        if (_views.TryGetValue(pos, out var view))
            view.ClearHighlight();
    }
    
    #endregion
    
    #region Grid <-> World
    
    public Vector3 CellToWorld(Vector2Int pos)
    {
        return new Vector3(
            pos.x + 0.5f,
            pos.y + 0.5f,
            0f
        );
    }
    
    public Vector2Int WorldToCell(Vector3 world)
    {
        return new Vector2Int(
            Mathf.FloorToInt(world.x),
            Mathf.FloorToInt(world.y)
        );
    }
    
    #endregion
}