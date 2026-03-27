using UnityEngine;

public class PlayerCarry : MonoBehaviour
{
    [SerializeField] private Transform carryAnchor;

    private Corpse _carriedCorpse;
    
    public bool HasCorpse => _carriedCorpse;

    public void PickUp(Corpse corpse)
    {
        if (HasCorpse)
            return;
        
        _carriedCorpse = corpse;
        
        corpse.transform.SetParent(carryAnchor);
        corpse.transform.localPosition = Vector3.zero;

        corpse.OnPickedUp();
    }

    public void Drop()
    {
        if (!HasCorpse)
            return;

        if (_carriedCorpse)
        {
            _carriedCorpse.transform.SetParent(null);

            var gridPos = GridManager.Instance.WorldToGrid(transform.position);
            var worldPos = GridManager.Instance.GridToWorld(gridPos);

            _carriedCorpse.transform.position = worldPos;
            _carriedCorpse.OnDropped();
        }

        _carriedCorpse = null;
    }
    
    public Corpse GetCarriedCorpse() => _carriedCorpse;
}