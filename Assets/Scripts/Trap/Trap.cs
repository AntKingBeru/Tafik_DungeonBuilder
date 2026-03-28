using UnityEngine;
using System.Collections.Generic;

public class Trap : MonoBehaviour
{
    [SerializeField] private TrapData data;
    [SerializeField] private Combat combat;
    [SerializeField] private LayerMask enemyLayer;
    
    private readonly List<Enemy> _targets = new();
    private readonly Collider2D[] _results = new Collider2D[32];
    
    private ContactFilter2D _filter;

    private void Awake()
    {
        _filter = new ContactFilter2D
        {
            useLayerMask = true,
            layerMask = enemyLayer,
            useTriggers = true
        };
    }

    private void Update()
    {
        DetectEnemies();
        AttackAll();
    }
    
    public void SetData(TrapData d)
    {
        data = d;
    }

    private void DetectEnemies()
    {
        _targets.Clear();

        var count = Physics2D.OverlapCircle(
            transform.position,
            data.range,
            _filter,
            _results
        );

        for (var i = 0; i < count; i++)
        {
            var enemy = _results[i].attachedRigidbody?.GetComponent<Enemy>();

            if (enemy)
                _targets.Add(enemy);
        }
    }

    private void AttackAll()
    {
        foreach (var enemy in _targets)
            combat.TryAttack(enemy);
    }
    
    #if UNITY_EDITOR

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, data.range);
    }
    
    #endif
}