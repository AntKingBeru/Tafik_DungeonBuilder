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

        corpse.OnPickedUp(carryAnchor);
    }

    public Corpse Drop()
    {
        if (!HasCorpse)
            return null;
        
        var corpse = _carriedCorpse;

        corpse.transform.SetParent(null);
        _carriedCorpse = null;

        return corpse;
    }
}