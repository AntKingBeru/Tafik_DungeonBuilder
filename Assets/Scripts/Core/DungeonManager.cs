using UnityEngine;

public class DungeonManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private RoomInstance coreRoomPrefab;
    [SerializeField] private RoomInstance revivalRoomPrefab;
    [SerializeField] private Transform roomsParent; 
    [SerializeField] private Transform player;

    private void Start()
    {
        GenerateStartingDungeon();
    }

    private void GenerateStartingDungeon()
    {
        var dungeonWidth = GridManager.Instance.Width;
        
        var coreSize = coreRoomPrefab.Size;
        var revivalSize = revivalRoomPrefab.Size;
        
        // ----- CORE ROOM POSITION -----
        
        var coreX = (dungeonWidth - coreSize.x) / 2;
        const int coreY = 0;
        
        var coreOrigin = new Vector2Int(coreX, coreY);

        var coreRoom = SpawnRoom(coreRoomPrefab, coreOrigin);
        
        // ----- REVIVAL ROOM POSITION -----
        
        var revivalX = coreX - revivalSize.x;
        const int revivalY = 0;
        
        var revivalOrigin = new Vector2Int(revivalX, revivalY);

        SpawnRoom(revivalRoomPrefab, revivalOrigin);
        
        // ----- PLAYER Spawn -----
        
        MovePlayerToRoom(coreRoom);
    }

    private RoomInstance SpawnRoom(RoomInstance prefab, Vector2Int origin)
    {
        var size = prefab.Size;
        
        GridManager.Instance.ClearArea(origin, size);
        
        var worldPos = GridManager.Instance.CellToWorld(origin);

        var room = Instantiate(
            prefab,
            worldPos,
            Quaternion.identity,
            roomsParent
        );
        
        room.Initialize(origin);
        
        GridManager.Instance.RegisterRoom(room, origin, size);

        return room;
    }

    private void MovePlayerToRoom(RoomInstance room)
    {
        Vector3 spawnPosition;

        if (room.PlayerSpawnPoint)
        {
            spawnPosition = room.PlayerSpawnPoint.position;
        }
        else
        {
            // fallback if spawn point is missing
            var spawnCell = room.Origin + new Vector2Int(
                room.Size.x / 2,
                room.Size.y / 2
            );
            
            spawnPosition = GridManager.Instance.CellToWorld(spawnCell);
        }
        
        player.transform.position = spawnPosition;
    }
}