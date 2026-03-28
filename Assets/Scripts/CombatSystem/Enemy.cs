using UnityEngine;

[RequireComponent(typeof(EnemyHealth))]
[RequireComponent(typeof(EnemyMovement))]
public class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] private EnemyHealth health;

    private void Awake()
    {
        health.OnDeath += HandleDeath;
    }

    private void OnDestroy()
    {
        health.OnDeath -= HandleDeath;
    }

    public void TakeDamage(DamageData damage)
    {
        health.TakeDamage(damage);
    }

    private void HandleDeath(DamageData killingBlow)
    {
        SpawnCorpse(killingBlow);
        Destroy(gameObject);
    }

    private void SpawnCorpse(DamageData killingBlow)
    {
        var corpseType = GetCorpseType(killingBlow.Type);
        
        var gridPos = GridManager.Instance.WorldToGrid(transform.position);
        var worldPos = GridManager.Instance.GridToWorld(gridPos);

        CorpseManager.Instance.SpawnCorpse(
            worldPos,
            corpseType
        );
    }

    private CorpseType GetCorpseType(DamageType type)
    {
        return type switch
        {
            DamageType.Physical => CorpseType.Zombie,
            DamageType.Magical => CorpseType.Skeleton,
            _ => CorpseType.Zombie
        };
    }
}