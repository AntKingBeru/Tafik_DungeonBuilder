using UnityEngine;

[CreateAssetMenu(menuName = "Dungeon/Room Blueprint")]
public class RoomBlueprint : ScriptableObject
{
    public string id;
    public Vector2Int size;

    public GameObject prefab;

    [Header("Optional")]
    public bool hasTrapAnchors;
    public bool isCore;
}