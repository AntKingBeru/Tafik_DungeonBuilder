using UnityEngine;

public class HaulCorpseJob : Job
{
    private readonly Corpse _corpse;
    private readonly RevivalDropZone _dropZone;

    public HaulCorpseJob(Corpse corpse, RevivalDropZone dropZone)
    {
        _corpse = corpse;
        _dropZone = dropZone;
    }

    public override void Execute(Minion minion)
    {
        if (!_corpse)
        {
            Complete();
            return;
        }

        if (!minion.Carry.HasCorpse)
        {
            minion.MoveTo(_corpse.transform.position);

            if (Vector2.Distance(minion.transform.position, _corpse.transform.position) < 0.5f)
            {
                minion.Carry.PickUp(_corpse);
            }
        }
        else
        {
            minion.MoveTo(_dropZone.transform.position);

            if (Vector2.Distance(minion.transform.position, _dropZone.transform.position) < 0.5f)
            {
                _dropZone.ReceiveCorpse(minion.Carry.Drop());
                Complete();
            }
        }
    } 
}