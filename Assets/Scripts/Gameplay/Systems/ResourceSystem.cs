using UnityEngine;
using System.Collections.Generic;

public class ResourceSystem : MonoBehaviour
{
    public static ResourceSystem Instance { get; private set; }
    
    private readonly Dictionary<ResourceType, int> _resources = new();

    private void Awake()
    {
        if (Instance && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        foreach (ResourceType type in System.Enum.GetValues(typeof(ResourceType)))
        {
            _resources[type] = 0;
        }
    }

    public void Add(ResourceType type, int amount)
    {
        _resources[type] += amount;
    }
    
    public int Get(ResourceType type)
    {
        return _resources.GetValueOrDefault(type, 0);
    }
}