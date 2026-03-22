using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class TrapTrigger : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TrapDamage trapDamage;

    private void OnTriggerStay2D(Collider2D other)
    {
        var enemy = other.GetComponent<Enemy>();
        if (!enemy)
            return;

        trapDamage.Apply(enemy);
    }
}