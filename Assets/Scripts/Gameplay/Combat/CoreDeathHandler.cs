using UnityEngine;

public class CoreDeathHandler : MonoBehaviour
{
    [SerializeField] private Health health;

    private void OnEnable()
    {
        health.OnDeath += HandleDeath;
    }

    private void OnDisable()
    {
        health.OnDeath -= HandleDeath;
    }

    private void HandleDeath()
    {
        // TODO: pause game, show death screen
    }
}