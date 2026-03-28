using UnityEngine;
using System;

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager Instance { get; private set; }
    
    public int Stone { get; private set; }
    public int Wood { get; private set; }

    public event Action<int, int> OnResourceChanged;
    
    public int maxStone = 100;
    public int maxWood = 100;

    private void Awake()
    {
        Instance = this;
    }
    
    public void AddStone(int amount)
    {
        Stone = Mathf.Min(Stone + amount, maxStone);
        OnResourceChanged?.Invoke(Stone, Wood);
    }

    public void AddWood(int amount)
    {
        Wood = Mathf.Min(Wood + amount, maxWood);
        OnResourceChanged?.Invoke(Stone, Wood);
    }

    public bool CanAfford(int stone, int wood)
    {
        return Stone >= stone && Wood >= wood;
    }

    public bool TrySpend(int stone, int wood)
    {
        if (!CanAfford(stone, wood))
            return false;

        Stone -= stone;
        Wood -= wood;

        OnResourceChanged?.Invoke(Stone, Wood);
        return true;
    }
}