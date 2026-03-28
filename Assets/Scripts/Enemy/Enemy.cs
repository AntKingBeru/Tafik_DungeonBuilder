using UnityEngine;

[RequireComponent(typeof(EnemyHealth))]
[RequireComponent(typeof(EnemyMovement))]
[RequireComponent(typeof(Combat))]
public class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] private EnemyHealth health;
    [SerializeField] private EnemyMovement movement;
    [SerializeField] private Combat combat;

    private Minion _currentTarget;

    private void Awake()
    {
        health.OnDeath += HandleDeath;
        EnemyManager.Instance.Register(this);
    }

    private void OnDestroy()
    {
        health.OnDeath -= HandleDeath;
    }

    private void OnEnable()
    {
        EnemyManager.Instance.Register(this);
    }

    private void OnDisable()
    {
        EnemyManager.Instance.UnRegister(this);
    }

    private void Update()
    {
        HandleTargeting();
    }

    public void TakeDamage(DamageData damage)
    {
        health.TakeDamage(damage);
    }

    private void HandleTargeting()
    {
        if (_currentTarget)
        {
            combat.TryAttack(_currentTarget);
            movement.MoveTo(GridManager.Instance.WorldToGrid(_currentTarget.transform.position));
            return;
        }

        var minion = FindClosestMinion();

        if (minion)
        {
            _currentTarget = minion;
            return;
        }

        var core = GridManager.Instance.GetCoreRoom();

        if (core)
            movement.MoveTo(GridManager.Instance.WorldToGrid(core.GetCenterWorld()));
    }

    private void HandleDeath(DamageData killingBlow)
    {
        SpawnCorpse(killingBlow);
        Destroy(gameObject);
    }

    private Minion FindClosestMinion()
    {
        var minions = MinionManager.Instance.Minions;

        var bestDist = 999f;
        Minion best = null;

        foreach (var m in minions)
        {
            if (!m)
                continue;

            var dist = Vector2.Distance(transform.position, m.transform.position);

            if (dist < 3f && dist < bestDist)
            {
                bestDist = dist;
                best = m;
            }
        }

        return best;
    }

    private void SpawnCorpse(DamageData killingBlow)
    {
        var corpseType = killingBlow.type == DamageType.Magical
            ? CorpseType.Skeleton
            : CorpseType.Zombie;
        
        CorpseManager.Instance.SpawnCorpse(transform.position, corpseType);
    }
}