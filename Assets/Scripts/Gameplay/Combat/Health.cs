using UnityEngine;
using System;

public class Health : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100f;

    private float _currentHealth;
    
    public event Action<float> OnHealthChanged;
    public event Action OnDeath;
    
    private void Awake()
    {
        _currentHealth = maxHealth;
    }

    public void TakeDamage(float amount)
    {
        if (amount <= 0f)
            return;
        
        _currentHealth -= amount;
        OnHealthChanged?.Invoke(_currentHealth);

        if (_currentHealth <= 0f)
            Die();
    }

    private void Die()
    {
        OnDeath?.Invoke();
    }
    
    public float GetHealth() => _currentHealth;
    public float GetMaxHealth() => maxHealth;
}