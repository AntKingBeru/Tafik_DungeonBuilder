using UnityEngine;
using System.Collections.Generic;

public class DungeonGridView : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private DungeonGrid grid;
    [SerializeField] private TileView tilePrefab;
    
    private readonly Dictionary<Vector2Int, TileView> _tileViews = new();

    private void Start()
    {
        GenerateVisuals();

        grid.OnTileChanged += UpdateTileView;
    }

    private void OnDestroy()
    {
        if (grid)
            grid.OnTileChanged -= UpdateTileView;
    }
    
    #region Grid Visual Generation

    private void GenerateVisuals()
    {
        for (var x = 0; x < grid.Width; x++)
        {
            for (var y = 0; y < grid.Height; y++)
            {
                var pos = new Vector2Int(x, y);
                
                var tile = grid.GetTile(pos);
                
                var worldPos = grid.GridToWorld(pos);

                var view = Instantiate(
                    tilePrefab,
                    worldPos,
                    Quaternion.identity,
                    transform
                );
                
                view.Initialize(pos);
                view.SetState(tile.state, tile.resourceType);
                
                _tileViews.Add(pos, view);
            }
        }
    }
    
    #endregion
    
    #region Visual On Change

    private void UpdateTileView(DungeonTile tile)
    {
        if (_tileViews.TryGetValue(tile.gridPosition, out var view))
            view.SetState(tile.state, tile.resourceType);
    }
    
    #endregion
    
    #region Access

    public TileView GetTileView(Vector2Int pos)
    {
        _tileViews.TryGetValue(pos, out var view);
        return view;
    }
    
    #endregion
}