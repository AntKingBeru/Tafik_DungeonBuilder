using UnityEngine;

[CreateAssetMenu(fileName = "BuildMenuData", menuName = "Dungeon/Build Menu Data")]
public class BuildMenuData : ScriptableObject
{
    public RoomData[] rooms;
    public TrapData[] traps;
    public ScriptableObject[] upgrades;
}