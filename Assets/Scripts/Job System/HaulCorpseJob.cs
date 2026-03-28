using UnityEngine;

public class HaulCorpseJob : Job
{
    private readonly Corpse _corpse;
    
    public HaulCorpseJob(Corpse corpse)
    {
        _corpse = corpse;
    }

    public override bool Execute(Minion minion)
    {
        if (!_corpse)
            return true; // Either no corpse or job done
        
        var grid = GridManager.Instance.WorldToGrid(_corpse.transform.position);
        minion.Movement.MoveTo(grid);

        if (Vector2.Distance(minion.transform.position, _corpse.transform.position) < 0.5f)
        {
            RevivalRoom.Instance.ProcessCorpse(_corpse.Type);
            Object.Destroy(_corpse.gameObject);
            return true;
        }

        return false;
    }
}