using UnityEngine;
using System.Collections.Generic;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance { get; private set; }
    
    private readonly List<Enemy> _enemies = new();

    private void Awake()
    {
        Instance = this;
    }

    public void Register(Enemy enemy)
    {
        _enemies.Add(enemy);
        JobManager.Instance.AddJob(new AttackJob(enemy));
    }

    public void UnRegister(Enemy enemy)
    {
        _enemies.Remove(enemy);
    }
}