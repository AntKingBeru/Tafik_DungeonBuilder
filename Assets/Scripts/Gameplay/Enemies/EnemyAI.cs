using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float speed = 2f;
    [SerializeField] private float attackRange = 0.5f;
    
    [Header("References")]
    [SerializeField] private DamageDealer damageDealer;
    
    private Transform _target;
    private Health _targetHealth;
    
    public void SetTarget(Transform target)
    {
        _target = target;
        
        if (target)
            _targetHealth = target.GetComponent<Health>();
    }

    private void Update()
    {
        if (!_target)
            return;
        
        var distance = Vector3.Distance(transform.position, _target.position);

        if (distance <= attackRange)
            Attack();
        else 
            MoveTowardsTarget();
    }

    private void MoveTowardsTarget()
    {
        var direction = (_target.position - transform.position).normalized;
        direction.z = 0;
        
        transform.position += direction * speed * Time.deltaTime;
        
        // Face direction
        if (direction != Vector3.zero)
        {
            var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }

    private void Attack()
    {
        if (!_targetHealth)
            return;
        
        damageDealer.DealDamage(_targetHealth);
    }
}