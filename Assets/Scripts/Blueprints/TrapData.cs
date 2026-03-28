using UnityEngine;

[CreateAssetMenu(fileName = "TrapData", menuName = "Dungeon/Trap Data")]
public class TrapData : ScriptableObject
{
    public string trapName;
    public GameObject prefab;
    
    public int damage;
    public float range;
    public float cooldown;
    public DamageType damageType;
    
    public ResourceData[] cost;
    
    public ResourceData[] Cost => cost;
}