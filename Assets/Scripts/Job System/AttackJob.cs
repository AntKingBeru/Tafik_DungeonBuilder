using UnityEngine;

public class AttackJob : Job
{
    private readonly Enemy _target;
    
    public AttackJob(Enemy target)
    {
        _target = target;
        Priority = 100;
        Position = target.transform.position;
    }

    public override bool Execute(Minion minion)
    {
        if (!_target)
            return true; // Job is done
        
        var grid = GridManager.Instance.WorldToGrid(_target.transform.position);
        minion.Movement.MoveTo(grid);

        if (Vector2.Distance(minion.transform.position, _target.transform.position) < 1f)
        {
            var combat = minion.GetComponent<Combat>();
            combat.TryAttack(_target.GetComponent<IDamageable>());
        }

        return false;
    }
    
    public override bool IsValid() => _target;
}