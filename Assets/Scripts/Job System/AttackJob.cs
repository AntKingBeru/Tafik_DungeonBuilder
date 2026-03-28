public class AttackJob : Job
{
    private readonly Enemy _target;

    public AttackJob(Enemy target)
    {
        _target = target;
    }

    public override void Execute(Minion minion)
    {
        if (!_target)
        {
            Complete();
            return;
        }

        minion.Attack(_target);
    }
}