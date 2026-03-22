using UnityEngine;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private DungeonGrid grid;
    [SerializeField] private Enemy enemyPrefab;
    [SerializeField] private EnemyTargeting targeting;
    
    [Header("Settings")]
    [SerializeField] private float spawnInterval = 5f;

    private float _timer;

    private void Update()
    {
        _timer += Time.deltaTime;

        if (_timer >= spawnInterval)
        {
            _timer = 0;
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        var edgeTiles = GetEdgeTiles();

        if (edgeTiles.Count == 0)
            return;

        var spawnTile = edgeTiles[Random.Range(0, edgeTiles.Count)];

        var worldPos = grid.GridToWorld(spawnTile.gridPosition);

        var enemy = Instantiate(
            enemyPrefab,
            worldPos,
            Quaternion.identity
        );

        var target = targeting.GetTarget();
        
        enemy.Initialize(target);
    }

    private List<DungeonTile> GetEdgeTiles()
    {
        List<DungeonTile> result = new();

        for (var x = 0; x < grid.Width; x++)
        {
            for (var y = 0; y < grid.Height; y++)
            {
                var pos = new Vector2Int(x, y);
                
                if (!grid.IsEdge(pos))
                    continue;

                var tile = grid.GetTile(pos);
                
                // Only spawn on cleared or occupied tiles
                if (tile != null && (tile.IsCleared || tile.IsOccupied))
                    result.Add(tile);
            }
        }
        
        return result;
    }
}