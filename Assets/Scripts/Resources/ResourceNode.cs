using UnityEngine;

public class ResourceNode : MonoBehaviour
{
    [SerializeField] private ResourceType type;
    [SerializeField] private int amount = 10;

    public bool HasResources => amount > 0;

    public int Harvest(int value)
    {
        var taken = Mathf.Min(value, amount);
        amount -= taken;
        return taken;
    }

    public ResourceType GetResourceType() => type;
}