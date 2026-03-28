using UnityEngine;

public class AttackJob : Job
{
    private readonly Enemy _target;
    
    public AttackJob(Enemy target)
    {
        _target = target;
    }

    public override bool Execute(Minion minion)
    {
        if (!_target)
            return true; // Job is done
        
        var grid = GridManager.Instance.WorldToGrid(_target.transform.position);
        minion.Movement.MoveTo(grid);
        
        if (Vector2.Distance(minion.transform.position, _target.transform.position) < 1f)
            _target.TakeDamage(new DamageData(1, DamageType.Physical));

        return false;
    }
}