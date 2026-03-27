using UnityEngine;

[CreateAssetMenu(fileName = "RoomData", menuName = "Dungeon/Room Data")]
public class RoomData : ScriptableObject
{
    [Header("Basic Info")]
    public string roomName;
    public RoomType roomType;

    [Header("Placement Settings")]
    public Vector2Int size;
    public GameObject prefab;
    
    [Header("Cost")]
    public int stoneCost;
    public int woodCost;
}