using UnityEngine;
using System.Collections.Generic;

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager Instance { get; private set; }
    
    private readonly Dictionary<ResourceType, int> _resources = new();

    private void Awake()
    {
        Instance = this;

        _resources[ResourceType.Stone] = 0;
        _resources[ResourceType.Wood] = 0;
    }

    public void AddResource(ResourceType type, int amount)
    {
        _resources[type] += amount;
    }

    public bool SpendResources(ResourceType type, int amount)
    {
        if (_resources[type] < amount)
            return false;
        
        _resources[type] -= amount;
        
        return true;
    }

    public int GetResourceCount(ResourceType type)
    {
        return _resources[type];
    }
}