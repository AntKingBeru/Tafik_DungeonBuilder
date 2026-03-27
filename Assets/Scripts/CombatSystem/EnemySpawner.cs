using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> enemyPrefabs;
    [SerializeField] private int spawnCount = 3;

    private void Start()
    {
        SpawnEnemies();
    }

    private void SpawnEnemies()
    {
        var edgeRooms = GetFurthestRooms();

        if (edgeRooms.Count == 0)
            return;

        for (var i = 0; i < spawnCount; i++)
        {
            var room = edgeRooms[Random.Range(0, edgeRooms.Count)];
            var spawnPos = room.GetRandomPositionInside();

            var prefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Count)];
            Instantiate(
                prefab,
                spawnPos,
                Quaternion.identity
            );
        }
    }

    private List<RoomInstance> GetFurthestRooms()
    {
        var rooms = GridManager.Instance.Rooms;

        if (rooms.Count == 0)
            return new List<RoomInstance>();

        var dungeonCenter = new Vector2(
            GridManager.Instance.Width / 2f,
            GridManager.Instance.Height / 2f
        );

        var maxDistance = 0f;
        var distances = new Dictionary<RoomInstance, float>();

        foreach (var room in rooms)
        {
            var center = room.GetCenter();
            var dist = Vector2.Distance(center, dungeonCenter);

            distances[room] = dist;
            
            if (dist > maxDistance)
                maxDistance = dist;
        }

        const float tolerance = 0.5f;

        return (from pair in distances where pair.Value >= maxDistance - tolerance select pair.Key).ToList();
    }
}