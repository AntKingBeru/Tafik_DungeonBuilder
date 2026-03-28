using UnityEngine;

[CreateAssetMenu(fileName = "RoomData", menuName = "Dungeon/Room Data")]
public class RoomData : ScriptableObject
{
    public string roomName;
    public RoomType roomType;
    public Vector2Int size;
    public GameObject prefab;
    
    public int stoneCost;
    public int woodCost;
}