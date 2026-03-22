using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class RoomInstance
{
    public RoomBlueprint blueprint;
    public Vector2Int origin;
    public List<DungeonTile> tiles;
    public List<Trap> traps = new();

    public GameObject view;
    
    public RoomInstance(RoomBlueprint blueprint, Vector2Int origin)
    {
        this.blueprint = blueprint;
        this.origin = origin;
        tiles = new List<DungeonTile>();
    }
}