using UnityEngine;

public class ClearSystem : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private InputReader input;
    [SerializeField] private GridCursor cursor;
    [SerializeField] private DungeonGrid grid;
    [SerializeField] private PlayerModeController modeController;

    [Header("Settings")]
    [SerializeField] private int resourcePerTile = 1;

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
        if (modeController.CurrentMode != PlayerMode.Clear)
            return;

        TryClear(cursor.CurrentGridPos);
    }

    private void TryClear(Vector2Int pos)
    {
        var tile = grid.GetTile(pos);

        if (tile is not { IsBlocked: true })
            return;
        
        ResourceSystem.Instance.Add(tile.resourceType, resourcePerTile);
        
        grid.SetTileState(pos, TileState.Cleared);
    }
}