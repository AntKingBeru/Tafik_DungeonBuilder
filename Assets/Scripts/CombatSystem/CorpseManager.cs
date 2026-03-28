using UnityEngine;
using System.Collections.Generic;

public class CorpseManager : MonoBehaviour
{
    public static CorpseManager Instance { get; private set; }
    
    [SerializeField] private GameObject zombieCorpsePrefab;
    [SerializeField] private GameObject skeletonCorpsePrefab;
    
    private readonly List<Corpse> _corpses = new();

    private void Awake()
    {
        Instance = this;
    }

    public void SpawnCorpse(Vector2 position, CorpseType type)
    {
        var prefab = type switch
        {
            CorpseType.Zombie => zombieCorpsePrefab,
            CorpseType.Skeleton => skeletonCorpsePrefab,
            _ => zombieCorpsePrefab
        };

        var obj = Instantiate(
            prefab,
            position,
            Quaternion.identity
        );
        
        var corpse = obj.GetComponent<Corpse>();
        corpse.Initialize(type);
        
        _corpses.Add(corpse);
    }

    public Corpse GetClosestCorpse(Vector2 position)
    {
        Corpse best = null;
        var bestDistance = float.MaxValue;

        foreach (var corpse in _corpses)
        {
            if (!corpse)
                continue;

            var dist = Vector2.Distance(
                position,
                corpse.transform.position
            );

            if (dist < bestDistance)
            {
                best = corpse;
                bestDistance = dist;
            }
        }

        return best;
    }
    
    public void RemoveCorpse(Corpse corpse)
    {
        _corpses.Remove(corpse);
    }
}