using UnityEngine;

public class RoomSpawnPoint : MonoBehaviour
{
    [SerializeField] private SpawnPointType type;

    public SpawnPointType Type => type;
}