using UnityEngine;

public abstract class Trap : MonoBehaviour
{
    protected TrapData Data;

    public virtual void Initialize(TrapData data)
    {
        Data = data;
    }
}