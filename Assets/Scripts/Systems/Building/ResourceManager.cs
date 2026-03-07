using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager Instance;

    public int stone;
    public int wood;

    private void Awake()
    {
        Instance = this;
    }

    public bool CanAfford(RoomDefinition room)
    {
        return stone >= room.stoneCost && wood >= room.woodCost;
    }

    public void Spend(RoomDefinition room)
    {
        stone -= room.stoneCost;
        wood -= room.woodCost;
    }

    public void AddResource(SurfaceType type, int amount)
    {
        switch (type)
        {
            case SurfaceType.Stone:
                stone += amount;
                break;
            case SurfaceType.Wood:
                wood += amount;
                break;
            default:
                Debug.Log("Invalid resource type");
                break;
        }
    }
}