using UnityEngine;
using System.Collections.Generic;

public class MinionManager : MonoBehaviour
{
    public static MinionManager Instance { get; private set; }
    
    [SerializeField] private int baseMaxMinions = 5;

    private readonly List<Minion> _minions = new();
    
    private int _bonusFromBarracks;

    public IReadOnlyList<Minion> Minions => _minions;
    public int MaxMinions => baseMaxMinions + _bonusFromBarracks;

    private void Awake()
    {
        Instance = this;
    }
    
    public void AddBarracksBonus(int amount)
    {
        _bonusFromBarracks += amount;
    }

    public void Register(Minion m) => _minions.Add(m);
    public void Unregister(Minion m) => _minions.Remove(m);
}