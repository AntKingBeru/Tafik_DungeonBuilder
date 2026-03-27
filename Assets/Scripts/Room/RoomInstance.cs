using UnityEngine;
using System.Linq;

public class RoomInstance : MonoBehaviour
{
    public RoomData Data { get; private set; }
    
    [SerializeField] private RoomSpawnPoint[] spawnPoints;
    
    public void Initialize(RoomData data)
    {
        Data = data;
    }

    public Transform GetSpawnPoint(SpawnPointType type)
    {
        var point = spawnPoints.FirstOrDefault(p => p.type == type);
        
        return !point ? null : point.transform;
    }
}