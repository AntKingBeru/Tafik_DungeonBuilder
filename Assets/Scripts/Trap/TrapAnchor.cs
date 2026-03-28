using UnityEngine;

public class TrapAnchor : MonoBehaviour
{
    public bool IsOccupied { get; private set; }

    public void PlaceTrap(GameObject trapPrefab, TrapData data)
    {
        if (IsOccupied)
            return;

        var trap = Instantiate(
            trapPrefab,
            transform.position,
            Quaternion.identity,
            transform
        );
        
        var trapComp = trap.GetComponent<Trap>();
        trapComp.SetData(data);

        IsOccupied = true;
        
        TrapManager.Instance.RegisterTrap();
    }
}