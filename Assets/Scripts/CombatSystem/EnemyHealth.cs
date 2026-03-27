using UnityEngine;
using System;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 10;

    private int _currentHealth;
    
    public Action<DamageData> OnDeath;

    private void Awake()
    {
        _currentHealth = maxHealth;
    }

    public void TakeDamage(DamageData damage)
    {
        _currentHealth -= damage.Amount;
        
        if (_currentHealth <= 0)
            OnDeath?.Invoke(damage);
    }
}