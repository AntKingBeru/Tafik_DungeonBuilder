public abstract class Job
{
    public bool IsReserved { get; private set; }
    public Minion AssignedMinion { get; private set; }

    public void Reserve(Minion minion)
    {
        IsReserved = true;
        AssignedMinion = minion;
    }

    public void Complete()
    {
        JobManager.Instance.CompleteJob(this);
    }

    public abstract void Execute(Minion minion);
}