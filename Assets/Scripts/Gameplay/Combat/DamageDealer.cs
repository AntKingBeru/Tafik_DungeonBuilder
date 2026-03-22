using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [SerializeField] private float damagePerSecond = 5;

    public void DealDamage(Health target)
    {
        target.TakeDamage(damagePerSecond * Time.deltaTime);
    }
}