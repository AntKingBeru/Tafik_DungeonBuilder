using UnityEngine;

public class Combat : MonoBehaviour
{
    [SerializeField] private float attackCooldown = 1f;
    [SerializeField] private int damage = 1;
    [SerializeField] private DamageType damageType;

    private float _timer;
    
    public bool CanAttack => _timer <= 0f;

    private void Update()
    {
        if (_timer > 0)
            _timer -= Time.deltaTime;
    }

    public void TryAttack(IDamageable target)
    {
        if (target == null || !CanAttack)
            return;
        
        target.TakeDamage(new DamageData(damage, damageType));
        _timer = attackCooldown;
    }
}