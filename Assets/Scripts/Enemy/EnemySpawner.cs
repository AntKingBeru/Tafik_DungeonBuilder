using UnityEngine;
using System.Collections;
using System.Linq;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner Instance { get; private set; }
    
    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private float timeBetweenWaves = 10f;
    [SerializeField] private int baseEnemiesPerWave = 3;
    [SerializeField] private int maxWaves = 10;

    private int _waveIndex;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        StartCoroutine(WaveLoop());
    }

    private IEnumerator WaveLoop()
    {
        while (_waveIndex < maxWaves)
        {
            yield return new WaitForSeconds(timeBetweenWaves);

            if (!CanSpawn())
                continue;

            yield return SpawnWave();
        }
    }

    private bool CanSpawn()
    {
        return GridManager.Instance.Rooms.Count >= 7
               && TrapManager.Instance.TotalTrapsPlaced >= 4;
    }

    private IEnumerator SpawnWave()
    {
        _waveIndex++;
        
        var count = baseEnemiesPerWave + _waveIndex * 2;
        
        yield return SpawnEnemies(count);
    }

    private IEnumerator SpawnEnemies(int count)
    {
        for (var i = 0; i < count; i++)
        {
            SpawnSingle();
            yield return new WaitForSeconds(0.5f);
        }
    }

    private void SpawnSingle()
    {
        var room = GetSpawnRoom();
        if (!room)
            return;

        var prefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];

        Instantiate(
            prefab,
            room.GetRandomPositionInside(),
            Quaternion.identity
        );
    }

    private RoomInstance GetSpawnRoom()
    {
        var valid = GridManager.Instance.Rooms
            .Where(r => r.Data.roomType == RoomType.Hallway)
            .ToList();

        return valid.Count == 0 ? null : valid[Random.Range(0, valid.Count)];
    }
}