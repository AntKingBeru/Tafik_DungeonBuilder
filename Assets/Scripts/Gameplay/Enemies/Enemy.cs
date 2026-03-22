using UnityEngine;
using System;

[RequireComponent(typeof(EnemyAI))]
public class Enemy : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] private float maxHealth = 10f;

    [Header("References")]
    [SerializeField] private EnemyAI ai;

    private float _currentHealth;
    
    public event Action<Enemy> OnDeath;
    
    private void Awake()
    {
        _currentHealth = maxHealth;
    }

    public void Initialize(Transform target)
    {
        ai.SetTarget(target);
    }

    public void TakeDamage(float amount)
    {
        _currentHealth -= amount;

        if (_currentHealth <= 0f)
        {
            Die();
        }
    }

    private void Die()
    {
        OnDeath?.Invoke(this);
        Destroy(gameObject);
    }
}
