using UnityEngine;

[CreateAssetMenu(fileName = "TrapData", menuName = "Dungeon/Trap Data")]
public class TrapData : ScriptableObject
{
    [Header("Basic Info")]
    public string trapName;
    public int damage;
    public float range;
    public float attackRate;
    public DamageType damageType;

    [Header("Placement Settings")]
    public GameObject prefab;
    
    [Header("Cost")]
    public int stoneCost;
    public int woodCost;
}