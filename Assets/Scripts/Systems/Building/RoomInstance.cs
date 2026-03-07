using UnityEngine;

public class RoomInstance : MonoBehaviour
{
    public RoomDefinition definition;
    public Vector2Int origin;

    public void Initialize(RoomDefinition def, Vector2Int pos)
    {
        definition = def;
        origin = pos;
    }
}