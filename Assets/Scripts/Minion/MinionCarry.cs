using UnityEngine;

public class MinionCarry : MonoBehaviour
{
    private Corpse _corpse;
    private ResourceType _type;
    private int _amount;

    public bool HasCorpse => _corpse;
    public bool HasResource => _amount > 0;

    public void PickUp(Corpse corpse)
    {
        _corpse = corpse;
        corpse.PickUp();
    }

    public Corpse Drop()
    {
        var c = _corpse;
        _corpse = null;
        return c;
    }

    public void PickUpResource(ResourceType type, int amount)
    {
        _type = type;
        _amount = amount;
    }

    public (ResourceType, int) DropResource()
    {
        var result = (_type, _amount);
        _amount = 0;
        return result;
    }
}