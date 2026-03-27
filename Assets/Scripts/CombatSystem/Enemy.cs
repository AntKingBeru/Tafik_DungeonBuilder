using UnityEngine;

[RequireComponent(typeof(EnemyHealth))]
[RequireComponent(typeof(EnemyMovement))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyHealth health;

    private void Awake()
    {
        health.OnDeath += HandleDeath;
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

        CorpseManager.Instance.SpawnCorpse(
            GridManager.Instance.GridToWorld(
                GridManager.Instance.WorldToGrid(transform.position)
                ),
            corpseType
        );
    }

    private CorpseType GetCorpseType(DamageType damageType)
    {
        return damageType switch
        {
            DamageType.Physical => CorpseType.Zombie,
            DamageType.Magical => CorpseType.Skeleton,
            _ => CorpseType.Zombie
        };
    }
}