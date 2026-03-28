using UnityEngine;

public class TrapManager : MonoBehaviour
{
    public static TrapManager Instance { get; private set; }

    public int TotalTrapsPlaced { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public void RegisterTrap()
    {
        TotalTrapsPlaced++;
    }
}