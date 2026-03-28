using UnityEngine;

public class TrapAnchor : MonoBehaviour
{
    private bool _isOccupied;
    
    public bool IsOccupied => _isOccupied;

    public void PlaceTrap(GameObject trapPrefab, TrapData data)
    {
        if (_isOccupied)
            return;

        var trap = Instantiate(
            trapPrefab,
            transform.position,
            Quaternion.identity,
            transform
        );
        
        var trapComp = trap.GetComponent<Trap>();
        if (trapComp)
            trapComp.Initialize(data);
        
        _isOccupied = true;
    }
}