using UnityEngine;

public class DungeonInitializer : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private DungeonGrid grid;
    [SerializeField] private RoomPlacer roomPlacer;
    
    [Header("Initial Rooms")]
    [SerializeField] private InitialRoomConfig[] initialRooms;

    private void Start()
    {
        Invoke(nameof(GenerateInitialDungeon), 0.1f);
    }

    private void GenerateInitialDungeon()
    {
        foreach (var config in initialRooms)
        {
            PrepareArea(config);
            PlaceRoom(config);
        }
    }
    
    #region Prepare Area

    private void PrepareArea(InitialRoomConfig config)
    {
        var size = config.blueprint.size;
        var origin = config.position;

        for (var x = 0; x < size.x; x++)
        {
            for (var y = 0; y < size.y; y++)
            {
                var pos = origin + new Vector2Int(x, y);
                
                var tile = grid.GetTile(pos);
                if (tile == null)
                    continue;
                
                grid.SetTileState(pos, TileState.Cleared);
            }
        }
    }
    
    #endregion
    
    #region Place Room

    private void PlaceRoom(InitialRoomConfig config)
    {
        roomPlacer.PlaceRoomFromInitializer(
            config.position,
            config.blueprint
        );
    }
    
    #endregion
}