using UnityEngine;
using System.Collections.Generic;

public class GridManager : MonoBehaviour
{
    public static GridManager Instance { get; private set; }

    [Header("Grid Settings")]
    [SerializeField] private int width = 25;
    [SerializeField] private int height = 25;
    [SerializeField] private float cellSize = 1f;
    
    [Header("References")]
    [SerializeField] private Transform player;
    
    [Header("Starter Rooms")]
    [SerializeField] private RoomData coreRoom;
    [SerializeField] private RoomData revivalRoom;
    [SerializeField] private RoomData barracksRoom;
    [SerializeField] private RoomData storageRoom;
    
    [Header("Prefabs")]
    [SerializeField] private GameObject clearedCellPrefab;
    [SerializeField] private GameObject stoneCellPrefab;
    [SerializeField] private GameObject woodCellPrefab;
    
    [Header("Cleared Sprites")]
    [SerializeField] private Sprite[] clearedSprites;
    
    [Header("Edge Sprites")]
    [SerializeField] private Sprite topEdge;
    [SerializeField] private Sprite bottomEdge;
    [SerializeField] private Sprite leftEdge;
    [SerializeField] private Sprite rightEdge;
    [SerializeField] private Sprite topLeftCorner;
    [SerializeField] private Sprite topRightCorner;
    [SerializeField] private Sprite bottomLeftCorner;
    [SerializeField] private Sprite bottomRightCorner;
    
    [Header("Parents")]
    [SerializeField] private Transform cellsParent;
    [SerializeField] private Transform roomsParent;
    
    private readonly Dictionary<Vector2Int, GridCell> _grid = new();
    private readonly List<RoomInstance> _rooms = new();
    
    public int Width => width;
    public int Height => height;
    public float CellSize => cellSize;
    public IReadOnlyList<RoomInstance> Rooms => _rooms;

    private void Awake()
    {
        Instance = this;
        GenerateGrid();
        GenerateStarterRooms();
    }
    
    #region Grid Generation

    private void GenerateGrid()
    {
        for (var x = 0; x < width; x++)
        {
            for (var y = 0; y < height; y++)
            {
                var pos = new Vector2Int(x, y);

                var type = Random.value < 0.05f
                    ? CellType.Wood
                    : CellType.Stone;
                
                var cell = new GridCell(pos, type);
                _grid[pos] = cell;

                SpawnCellVisual(cell);
            }
        }
    }

    private void GenerateStarterRooms()
    {
        var centerX = width / 2;
        var centerY = height / 2;
        
        var coreCenter = new Vector2Int(centerX, centerY);
        
        var coreInstance = PlaceRoomInternal(coreCenter, coreRoom);

        var spacing = coreRoom.size.x;

        PlaceRoomInternal(coreCenter + new Vector2Int(-spacing, 0), revivalRoom);
        PlaceRoomInternal(coreCenter + new Vector2Int(spacing, 0), barracksRoom);
        PlaceRoomInternal(coreCenter + new Vector2Int(spacing * 2, 0), storageRoom);
        
        SpawnPlayer(coreInstance);
    }

    private void SpawnCellVisual(GridCell cell)
    {
        var prefab = cell.Type == CellType.Stone
            ? stoneCellPrefab
            : woodCellPrefab;
        
        var obj = Instantiate(
            prefab,
            GridToWorld(cell.Position),
            Quaternion.identity,
            cellsParent
        );
        
        cell.OccupiedObject = obj;
    }

    private void SpawnPlayer(RoomInstance core)
    {
        if (!core)
            return;

        var spawn = core.GetSpawnPoint(SpawnPointType.Player);

        if (!spawn)
            return;
        
        player.position = spawn.position;
    }

    public RoomInstance GetCoreRoom()
    {
        return _rooms.Find(r => r.Data == coreRoom);
    }
    
    #endregion
    
    #region Position
    
    public Vector2Int WorldToGrid(Vector2 worldPos)
    {
        return new Vector2Int(
            Mathf.FloorToInt(worldPos.x / cellSize),
            Mathf.FloorToInt(worldPos.y / cellSize)
        );
    }

    public Vector2 GridToWorld(Vector2Int gridPos)
    {
        return new Vector2(
            gridPos.x * cellSize + cellSize * 0.5f,
            gridPos.y * cellSize + cellSize * 0.5f
        );
    }
    
    #endregion
    
    #region Cell

    public GridCell GetCell(Vector2Int pos)
    {
        return _grid.GetValueOrDefault(pos);
    }

    public bool IsInsideGrid(Vector2Int pos)
    {
        return pos.x >= 0 && pos.y >= 0 && pos.x < width && pos.y < height;
    }

    private void PrepareArea(Vector2Int origin, Vector2Int size)
    {
        for (var x = 0; x < size.x; x++)
        {
            for (var y = 0; y < size.y; y++)
            {
                var pos = origin + new Vector2Int(x, y);
                
                if (!IsInsideGrid(pos))
                    continue;
                
                var cell = GetCell(pos);
                
                if (cell == null)
                    continue;
                
                if (cell.OccupiedObject)
                    Destroy(cell.OccupiedObject);
                
                var cleared = Instantiate(
                    clearedCellPrefab,
                    GridToWorld(pos),
                    Quaternion.identity,
                    cellsParent
                );
                
                cell.Type = CellType.Cleared;
                cell.IsOccupied = false;
                cell.OccupiedObject = cleared;
            }
        }
    }

    public bool IsWalkable(Vector2Int pos)
    {
        var cell = GetCell(pos);
        return cell is { Type: CellType.Room };
    }
    
    #endregion
    
    #region Validation
    
    public bool CanPlaceRoom(Vector2Int origin, Vector2Int size, bool ignoreBuildable = false)
    {
        for (var x = 0; x < size.x; x++)
        {
            for (var y = 0; y < size.y; y++)
            {
                var pos = origin + new Vector2Int(x, y);

                if (!IsInsideGrid(pos))
                    return false;

                var cell = GetCell(pos);
                
                if (cell == null || !cell.IsBuildable())
                    return false;

                if (!ignoreBuildable && !cell.IsBuildable())
                    return false;
            }
        }

        return true;
    }
    
    #endregion
    
    #region Place Room
    
    public void PlaceRoom(Vector2Int center, Vector2Int size, RoomData data)
    {
        var origin = center - new Vector2Int(
            (size.x - 1) / 2,
            (size.y - 1) / 2
        );
        
        if (!CanPlaceRoom(origin, size))
            return;

        var room = Instantiate(
            data.prefab,
            GridToWorld(origin),
            Quaternion.identity,
            roomsParent
        );
        
        var instance = room.GetComponent<RoomInstance>();
        if (instance)
        {
            instance.Initialize(data, center, size);
            _rooms.Add(instance);
            BuildTracker.Instance.RegisterRoomPlaced();
        }

        for (var x = 0; x < size.x; x++)
        {
            for (var y = 0; y < size.y; y++)
            {
                var pos = origin + new Vector2Int(x, y);
                var cell = GetCell(pos);

                cell.IsOccupied = true;
                cell.Type = CellType.Room;
                cell.OccupiedObject = room;
            }
        }
    }

    private RoomInstance PlaceRoomInternal(Vector2Int center, RoomData data)
    {
        var size = data.size;
        
        var origin = center - new Vector2Int(
            (size.x - 1) / 2,
            (size.y - 1) / 2
        );

        Debug.Log($"Trying to place {data.name} ar {center}");
        
        PrepareArea(origin, size);
        
        if (!CanPlaceRoom(origin, size))
        {
            Debug.Log("FAILED PLACEMENT");
            return null;
        }
        
        var roomGo = Instantiate(
            data.prefab,
            GridToWorld(origin),
            Quaternion.identity,
            roomsParent
        );
        
        var instance = roomGo.GetComponent<RoomInstance>();
        if (instance)
        {
            instance.Initialize(data, center, size);
            _rooms.Add(instance);
        }

        for (var x = 0; x < size.x; x++)
        {
            for (var y = 0; y < size.y; y++)
            {
                var pos = origin + new Vector2Int(x, y);
                var cell = GetCell(pos);
                
                cell.IsOccupied = true;
                cell.Type = CellType.Room;
                cell.OccupiedObject = roomGo;
            }
        }
        
        return instance;
    }
    
    #endregion
    
    #region Clear

    public void ClearCell(Vector2Int pos)
    {
        var cell = GetCell(pos);
        
        if (cell == null)
            return;

        if (cell.Type != CellType.Stone && cell.Type != CellType.Wood)
            return;

        switch (cell.Type)
        {
            case CellType.Stone:
                ResourceManager.Instance.Add(ResourceType.Stone, 1);
                break;
            case CellType.Wood:
                ResourceManager.Instance.Add(ResourceType.Wood, 2);
                break;
        }

        if (cell.OccupiedObject)
            Destroy(cell.OccupiedObject);

        var cleared = Instantiate(
            clearedCellPrefab,
            GridToWorld(pos),
            Quaternion.identity,
            cellsParent
        );
        
        var sr = cleared.GetComponent<SpriteRenderer>();

        var edgeSprite = GetEdgeSprite(pos);
        
        if (edgeSprite)
            sr.sprite = edgeSprite;
        else
            sr.sprite = clearedSprites[Random.Range(0, clearedSprites.Length)];

        cell.Type = CellType.Cleared;
        cell.OccupiedObject = cleared;
    }

    private Sprite GetEdgeSprite(Vector2Int pos)
    {
        var isLeft = pos.x == 0;
        var isRight = pos.x == width - 1;
        var isBottom = pos.y == 0;
        var isTop = pos.y == height - 1;
        
        if (isTop && isLeft)
            return topLeftCorner;
        if (isTop && isRight)
            return topRightCorner;
        if (isBottom && isLeft)
            return bottomLeftCorner;
        if (isBottom && isRight)
            return bottomRightCorner;
        
        if (isTop)
            return topEdge;
        if (isBottom)
            return bottomEdge;
        if (isLeft)
            return leftEdge;
        if (isRight)
            return rightEdge;
        
        return null;
    }

    #endregion
}