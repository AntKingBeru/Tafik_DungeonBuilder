using UnityEngine;

public abstract class Job
{
    public int Priority { get; protected set; }
    public Vector2 Position { get; protected set; }

    public Minion ReservedBy { get; private set; }

    public bool IsReserved => ReservedBy;

    public bool TryReserve(Minion minion)
    {
        if (IsReserved)
            return false;
        
        ReservedBy = minion;
        return true;
    }

    public void Release()
    {
        ReservedBy = null;
    }

    public abstract bool IsValid();
    public abstract bool Execute(Minion minion);
}