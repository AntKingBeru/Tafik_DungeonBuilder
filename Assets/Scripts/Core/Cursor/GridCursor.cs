using UnityEngine;

public class GridCursor : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private InputReader input;
    [SerializeField] private DungeonGrid grid;
    [SerializeField] private DungeonGridView gridView;
    [SerializeField] private PlayerModeController modeController;
    [SerializeField] private Camera mainCamera;

    private TileView _currentTileView;
    
    public Vector2Int CurrentGridPos { get; private set; }

    private void Update()
    {
        UpdateCursorPosition();
    }

    private void UpdateCursorPosition()
    {
        var worldPos = ScreenToWorld(input.MousePosition);
        
        var gridPos = grid.WorldToGrid(worldPos);

        if (!grid.IsInside(gridPos))
        {
            ClearHighlight();
            return;
        }

        if (gridPos == CurrentGridPos)
            return;

        CurrentGridPos = gridPos;

        UpdateHighlight(gridPos);
    }

    private Vector3 ScreenToWorld(Vector2 screenPos)
    {
        var world = mainCamera.ScreenToWorldPoint(screenPos);
        world.z = 0;
        return world;
    }

    private void UpdateHighlight(Vector2Int pos)
    {
        ClearHighlight();

        var tile = grid.GetTile(pos);
        if (tile == null)
            return;

        if (!ShouldHighlight(tile))
            return;
        
        _currentTileView = gridView.GetTileView(pos);
        
        if (_currentTileView)
            _currentTileView.SetHighlight(true);
    }

    private bool ShouldHighlight(DungeonTile tile)
    {
        return modeController.CurrentMode switch
        {
            PlayerMode.Clear => tile.IsBlocked,
            PlayerMode.Build => tile.IsCleared,
            _ => false
        };
    }

    private void ClearHighlight()
    {
        if (_currentTileView)
        {
            _currentTileView.SetHighlight(false);
            _currentTileView = null;
        }
    }
}