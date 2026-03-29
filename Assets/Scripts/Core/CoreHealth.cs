using UnityEngine;
using System;

public class CoreHealth : MonoBehaviour, IDamageable
{
    [SerializeField] private int maxHealth = 100;
    
    private int _currentHealth;
    
    public event Action<int, int> OnHealthChanged;
    public event Action OnDeath;

    private void Awake()
    {
        _currentHealth = maxHealth;
        OnHealthChanged?.Invoke(_currentHealth, maxHealth);
    }

    public void TakeDamage(DamageData damage)
    {
        _currentHealth -= damage.amount;
        _currentHealth = Mathf.Max(_currentHealth, 0);
        
        OnHealthChanged?.Invoke(_currentHealth, maxHealth);

        if (_currentHealth <= 0)
        {
            OnDeath?.Invoke();
            HandleDeath();
        }
    }

    private void HandleDeath()
    {
        Debug.Log("Game Over");
        Time.timeScale = 0f;
    }

    public float GetHealthNormalized()
    {
        return (float) _currentHealth / maxHealth;
    }
}