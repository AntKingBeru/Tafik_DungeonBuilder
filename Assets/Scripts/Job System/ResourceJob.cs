using UnityEngine;

public class ResourceJob : Job
{
    private readonly ResourceNode _node;
    private readonly StorageRoom _storage;

    public ResourceJob(ResourceNode node, StorageRoom storage)
    {
        _node = node;
        _storage = storage;
    }

    public override void Execute(Minion minion)
    {
        if (!_node)
        {
            Complete();
            return;
        }
        
        var carry = minion.Carry;

        if (!carry.HasResource)
        {
            minion.MoveTo(_node.transform.position);

            if (Vector2.Distance(minion.transform.position, _node.transform.position) < 0.5f)
            {
                var amount = _node.Harvest(5);

                if (amount > 0)
                    carry.PickUpResource(_node.GetResourceType(), amount);
            }
        }
        else
        {
            minion.MoveTo(_storage.transform.position);

            if (Vector2.Distance(minion.transform.position, _storage.transform.position) < 0.5f)
            {
                var (type, amount) = carry.DropResource();
                _storage.Store(type, amount);

                Complete();
            }
        }
    }
}