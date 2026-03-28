using UnityEngine;

public abstract class Job
{
    public int Priority { get; protected set; }
    public Vector2 Position { get; protected set; }

    public abstract bool Execute(Minion minion);
}