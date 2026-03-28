using UnityEngine;

public class ResourceJob : Job
{
    private readonly ResourceNode _node;
    private bool _collected;
    
    public ResourceJob(ResourceNode node)
    {
        _node = node;
    }

    public override bool Execute(Minion minion)
    {
        if (!_node)
            return true; // No node or job done

        if (!_collected)
        {
            var grid = GridManager.Instance.WorldToGrid(_node.transform.position);
            minion.Movement.MoveTo(grid);

            if (Vector2.Distance(minion.transform.position, _node.transform.position) < 0.5f)
            {
                _node.Harvest();
                _collected = true;
            }
        }
        else
        {
            var drop = StorageRoom.Instance.DropPoint.position;
            var grid = GridManager.Instance.WorldToGrid(drop);
            
            minion.Movement.MoveTo(grid);

            if (Vector2.Distance(minion.transform.position, drop) < 0.5f)
            {
                StorageRoom.Instance.Store(1);
                return true;
            }
        }

        return false;
    }
}