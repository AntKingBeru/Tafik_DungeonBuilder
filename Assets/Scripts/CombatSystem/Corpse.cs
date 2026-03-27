using UnityEngine;

public class Corpse : MonoBehaviour
{
    private CorpseType _type;
    private bool _isCarried;

    public void Initialize(CorpseType type)
    {
        _type = type;
    }

    public void OnPickedUp()
    {
        _isCarried = true;
        
        var col = GetComponent<Collider2D>();
        if (col)
            col.enabled = false;
    }

    public void OnDropped()
    {
        _isCarried = false;
        
        var col = GetComponent<Collider2D>();
        if (col)
            col.enabled = true;
    }
    
    public CorpseType GetCorpseType() => _type;
    public bool IsCarried() => _isCarried;
}