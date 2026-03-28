using UnityEngine;

public class HaulCorpseJob : Job
{
    private readonly Corpse _corpse;
    private bool _pickedUp;
    
    public HaulCorpseJob(Corpse corpse)
    {
        _corpse = corpse;
        Priority = 70;
        Position = corpse.transform.position;
    }

    public override bool Execute(Minion minion)
    {
        if (!_corpse)
            return true; // Either no corpse or job done
        
        var carry = minion.GetComponent<MinionCarry>();

        if (!_pickedUp)
        {
            var grid = GridManager.Instance.WorldToGrid(_corpse.transform.position);
            minion.Movement.MoveTo(grid);

            if (Vector2.Distance(minion.transform.position, _corpse.transform.position) < 0.5f)
            {
                carry.PickUp(_corpse.VisualPrefab);
                Object.Destroy(_corpse.gameObject);
                _pickedUp = true;
            }
        }
        else
        {
            var drop = RevivalRoom.Instance.DropPoint.position;
            var grid = GridManager.Instance.WorldToGrid(drop);
            
            minion.Movement.MoveTo(grid);
            
            if (Vector2.Distance(minion.transform.position, drop) < 0.5f)
            {
                carry.Drop();
                RevivalRoom.Instance.ProcessCorpse(_corpse.Type);
                return true;
            }
        }

        return false;
    }
    
    public override bool IsValid() => _corpse;   
}