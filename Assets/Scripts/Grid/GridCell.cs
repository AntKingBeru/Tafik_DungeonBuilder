using UnityEngine;

public class GridCell
{
    public Vector2Int Position;
    public CellType Type;

    public bool IsOccupied;
    public GameObject OccupiedObject;

    public GridCell(Vector2Int position, CellType type)
    {
        Position = position;
        Type = type;
        IsOccupied = false;
        OccupiedObject = null;
    }

    public bool IsBuildable()
    {
        return Type == CellType.Cleared && !IsOccupied;
    }
}