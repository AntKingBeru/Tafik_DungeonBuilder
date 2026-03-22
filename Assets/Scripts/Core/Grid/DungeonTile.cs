using UnityEngine;

[System.Serializable]
public class DungeonTile
{
    public Vector2Int gridPosition;
    public TileState state;

    // What resource this tile gives when cleared
    public ResourceType resourceType;
    
    // Room reference (can be null)
    public object Occupier;

    public DungeonTile(Vector2Int pos, TileState initialState, ResourceType resourceType)
    {
        gridPosition = pos;
        state = initialState;
        this.resourceType = resourceType;
        Occupier = null;
    }
    
    public bool IsBlocked => state == TileState.Blocked;
    public bool IsCleared => state == TileState.Cleared;
    public bool IsOccupied => state == TileState.Occupied;
}