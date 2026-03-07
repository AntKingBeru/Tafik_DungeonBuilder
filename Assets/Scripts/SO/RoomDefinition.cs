using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Dungeon/RoomDefinition")]
public class RoomDefinition : ScriptableObject
{
    public string roomName;

    public Vector2Int size;
    
    public int stoneCost; 
    public int woodCost;
    
    public GameObject roomPrefab;
    
    public bool requiredClearedTiles = true; 
    public bool canPlaceTraps;
}