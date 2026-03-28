using UnityEngine;

public class EnemyAttackType : MonoBehaviour
{
    [SerializeField] private DamageType damageType;
    
    public DamageType GetDamageType() => damageType;
}