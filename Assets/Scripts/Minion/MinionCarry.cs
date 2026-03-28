using UnityEngine;

public class MinionCarry : MonoBehaviour
{
    [SerializeField] private Transform carryAnchor;

    private GameObject _visual;
    
    public bool IsCarrying => _visual;

    public void PickUp(GameObject prefab)
    {
        if (_visual)
            return;

        _visual = Instantiate(
            prefab,
            carryAnchor
        );
        _visual.transform.localPosition = Vector3.zero;
    }

    public void Drop()
    {
        if (!_visual)
            return;
        
        Destroy(_visual);
        _visual = null;
    }
}