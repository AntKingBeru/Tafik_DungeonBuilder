using UnityEngine;

public class RoomPlacer : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private DungeonGrid grid;
    [SerializeField] private GridCursor cursor;
    [SerializeField] private InputReader input;
    [SerializeField] private PlayerModeController modeController;

    [Header("Runtime")]
    [SerializeField] private RoomBlueprint selectedRoom;

    private RoomGhost _currentGhost;

    private void OnEnable()
    {
        input.OnClick += HandleClick;
    }

    private void OnDisable()
    {
        input.OnClick -= HandleClick;
    }

    private void Update()
    {
        if (modeController.CurrentMode != PlayerMode.Build)
        {
            HideGhost();
            return;
        }

        if (!selectedRoom)
            return;

        UpdateGhost();
    }
    
    #region Ghost

    private void UpdateGhost()
    {
        if (!_currentGhost)
            CreateGhost();
        
        var origin = cursor.CurrentGridPos;

        var valid = CanPlaceRoom(origin, selectedRoom);
        
        _currentGhost.transform.position = grid.GridToWorld(origin);
        _currentGhost.SetValid(valid);
    }

    private void CreateGhost()
    {
        var go = new GameObject("RoomGhost");
        _currentGhost = go.AddComponent<RoomGhost>();
        
        var sr = go.AddComponent<SpriteRenderer>();
        _currentGhost.GetType()
            .GetField("spriteRenderer", System.Reflection.BindingFlags.NonPublic |
                                        System.Reflection.BindingFlags.Instance)
            ?.SetValue(_currentGhost, sr);
        
        sr.sortingOrder = 10;
        
        // TEMP: simple square
        sr.sprite = selectedRoom.prefab.GetComponentInChildren<SpriteRenderer>().sprite;
    }

    private void HideGhost()
    {
        if (_currentGhost)
            _currentGhost.gameObject.SetActive(false);
    }
    
    #endregion
    
    #region Click

    private void HandleClick()
    {
        if (modeController.CurrentMode != PlayerMode.Build)
            return;

        if (!selectedRoom)
            return;

        var origin = cursor.CurrentGridPos;

        if (!CanPlaceRoom(origin, selectedRoom))
            return;
        
        PlaceRoom(origin, selectedRoom);
    }
    
    #endregion
    
    #region Validation
    
    private bool CanPlaceRoom(Vector2Int origin, RoomBlueprint blueprint)
    {
        for (var x = 0; x < blueprint.size.x; x++)
        {
            for (var y = 0; y < blueprint.size.y; y++)
            {
                var pos = origin + new Vector2Int(x, y);

                var tile = grid.GetTile(pos);
                
                if (tile is not { IsCleared: true })
                    return false;
            }
        }

        return true;
    }
    
    #endregion
    
    #region Placement

    private void PlaceRoom(Vector2Int origin, RoomBlueprint blueprint)
    {
        var room = new RoomInstance(blueprint, origin);
        
        for (var x = 0; x < blueprint.size.x; x++)
        {
            for (var y = 0; y < blueprint.size.y; y++)
            {
                var pos = origin + new Vector2Int(x, y);

                var tile = grid.GetTile(pos);

                tile.Occupier = room;
                grid.SetTileState(pos, TileState.Occupied);
                
                room.tiles.Add(tile);
            }
        }
        
        // Spawn visual
        if (blueprint.prefab)
        {
            var worldPos = grid.GridToWorld(origin);
            room.view = Instantiate(
                blueprint.prefab,
                worldPos,
                Quaternion.identity
            );

            // Register Core automatically
            if (blueprint.isCore)
            {
                var target = room.view.transform;
                
                EnemyTargeting.Instance?.RegisterCore(target);
            }
        }
    }

    public void PlaceRoomFromInitializer(Vector2Int origin, RoomBlueprint blueprint)
    {
        if (!CanPlaceRoom(origin, blueprint))
            return;
        
        PlaceRoom(origin, blueprint);
    }
    
    #endregion
}