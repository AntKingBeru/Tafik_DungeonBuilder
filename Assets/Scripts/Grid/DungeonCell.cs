using UnityEngine;

public class DungeonCell
{
    public Vector2Int GridPosition { get; private set; }

    public ResourceType ResourceType { get; private set; }

    public bool IsCleared { get; private set; }

    public RoomInstance Room { get; set; }

    public DungeonCell(Vector2Int pos, ResourceType type)
    {
        GridPosition = pos;
        ResourceType = type;
        IsCleared = false;
        Room = null;
    }

    public void Clear()
    {
        IsCleared = true;
    }
}
