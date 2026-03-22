using UnityEngine;

public class TrapPlacer : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private DungeonGrid grid;
    [SerializeField] private GridCursor cursor;
    [SerializeField] private InputReader input;
    [SerializeField] private PlayerModeController modeController;
    
    [Header("Trap")]
    [SerializeField] private GameObject trapPrefab;

    private void OnEnable()
    {
        input.OnClick += HandleClick;
    }

    private void OnDisable()
    {
        input.OnClick -= HandleClick;
    }

    private void HandleClick()
    {
        if (modeController.CurrentMode != PlayerMode.Build)
            return;
        
        TryPlaceTrap(cursor.CurrentGridPos);
    }

    private void TryPlaceTrap(Vector2Int pos)
    {
        var tile = grid.GetTile(pos);

        if (tile is not { IsOccupied: true })
            return;

        if (tile.Occupier is not RoomInstance room)
            return;

        PlaceTrap(room, pos);
    }

    private void PlaceTrap(RoomInstance room, Vector2Int pos)
    {
        var worldPos = grid.GridToWorld(pos);

        var trapGo = Instantiate(
            trapPrefab,
            worldPos,
            Quaternion.identity
        );
        
        var trap = trapGo.GetComponent<Trap>();

        if (trap)
        {
            trap.Initialize(room);
            room.traps.Add(trap);
        }
    }
}