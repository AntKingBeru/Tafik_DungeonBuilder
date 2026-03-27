using UnityEngine;
using System;

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager Instance { get; private set; }
    
    public int Stone { get; private set; }
    public int Wood { get; private set; }

    public event Action<int, int> OnResourceChanged;

    private void Awake()
    {
        Instance = this;
    }
    
    public void AddStone(int amount)
    {
        Stone += amount;
        Notify();
    }

    public void AddWood(int amount)
    {
        Wood += amount;
        Notify();
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

        Notify();
        return true;
    }
    
    private void Notify()
    {
        OnResourceChanged?.Invoke(Stone, Wood);
    }
}