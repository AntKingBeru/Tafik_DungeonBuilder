using UnityEngine;
using System;
using System.Linq;
using System.Collections.Generic;

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager Instance { get; private set; }
    
    private readonly Dictionary<ResourceType, int> _resources = new();
    private readonly Dictionary<ResourceType, int> _maxResources = new();

    public Action OnResourcesChanged;
    
    public int maxStone = 100;
    public int maxWood = 100;

    private void Awake()
    {
        Instance = this;
        
        _resources[ResourceType.Stone] = 0;
        _resources[ResourceType.Wood] = 0;

        _maxResources[ResourceType.Stone] = 100;
        _maxResources[ResourceType.Wood] = 100;
    }
    
    public int Get(ResourceType type) => _resources[type];

    public bool HasEnough(ResourceData[] cost)
    {
        return cost.All(c => _resources[c.type] >= c.amount);
    }

    public void Spend(ResourceData[] cost)
    {
        foreach (var c in cost)
            _resources[c.type] -= c.amount;
        
        OnResourcesChanged?.Invoke();
    }

    public void Add(ResourceType type, int amount)
    {
        _resources[type] = Mathf.Min(_resources[type] + amount, _maxResources[type]);
        OnResourcesChanged?.Invoke();
    }
}