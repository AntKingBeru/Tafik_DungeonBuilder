using UnityEngine;
using System.Collections.Generic;

public class MinionManager : MonoBehaviour
{
    public static MinionManager Instance { get; private set; }

    public int maxMinions = 10;

    private int _currentCount;

    private void Awake()
    {
        Instance = this;
    }

    public bool CanSpawn()
    {
        return _currentCount < maxMinions;
    }

    public void Register()
    {
        _currentCount++;
    }

    public void Unregister()
    {
        _currentCount--;
    }
}