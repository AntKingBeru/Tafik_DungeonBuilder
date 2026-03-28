using UnityEngine;
using System.Collections.Generic;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance { get; private set; }
    
    private readonly List<Enemy> _enemies = new();
    
    public IReadOnlyList<Enemy> Enemies => _enemies;

    private void Awake()
    {
        Instance = this;
    }

    public void Register(Enemy enemy)
    {
        _enemies.Add(enemy);
    }

    public void UnRegister(Enemy enemy)
    {
        _enemies.Remove(enemy);
    }
}