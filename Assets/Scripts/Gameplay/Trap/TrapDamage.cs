using UnityEngine;

public class TrapDamage : MonoBehaviour
{
    [SerializeField] private float damagePerSecond = 10f;

    public void Apply(Enemy enemy)
    {
        enemy.TakeDamage(damagePerSecond * Time.deltaTime);
    }
}