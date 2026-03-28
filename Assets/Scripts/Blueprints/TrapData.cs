using UnityEngine;

[CreateAssetMenu(fileName = "TrapData", menuName = "Dungeon/Trap Data")]
public class TrapData : ScriptableObject
{
    public string trapName;
    public GameObject prefab;
    
    public int damage;
    public float cooldown;
    public DamageType damageType;
    
    public int stoneCost;
    public int woodCost;
}