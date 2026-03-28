using UnityEngine;

public class BuildTracker : MonoBehaviour
{
    public static BuildTracker Instance { get; private set; }

    private int _roomsPlaced;
    private int _trapsPlaced;
    
    public bool CanSpawnEnemies => _roomsPlaced >= 3 && _trapsPlaced >= 2;

    private void Awake()
    {
        Instance = this;
    }

    public void RegisterRoomPlaced()
    {
        _roomsPlaced++;
    }
    
    public void RegisterTrapPlaced()
    {
        _trapsPlaced++;
    }
}