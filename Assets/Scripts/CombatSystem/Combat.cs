using UnityEngine;

public class Combat : MonoBehaviour
{
    [SerializeField] private float attackCooldown = 1f;
    [SerializeField] private int damage = 1;
    [SerializeField] private DamageType damageType;

    private float _timer;

    public void TryAttack(IDamageable target)
    {
        if (target == null)
            return;
        
        _timer -= Time.deltaTime;

        if (_timer > 0)
            return;
        
        target.TakeDamage(new DamageData(damage, damageType));
        _timer = attackCooldown;
    }
}