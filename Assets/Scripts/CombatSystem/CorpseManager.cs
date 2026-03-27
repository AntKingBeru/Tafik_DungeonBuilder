using UnityEngine;

public class CorpseManager : MonoBehaviour
{
    public static CorpseManager Instance { get; private set; }
    
    [SerializeField] private GameObject zombieCorpsePrefab;
    [SerializeField] private GameObject skeletonCorpsePrefab;

    private void Awake()
    {
        Instance = this;
    }

    public void SpawnCorpse(Vector3 position, CorpseType type)
    {
        var prefab = type switch
        {
            CorpseType.Zombie => zombieCorpsePrefab,
            CorpseType.Skeleton => skeletonCorpsePrefab,
            _ => zombieCorpsePrefab
        };

        var corpse = Instantiate(
            prefab,
            position,
            Quaternion.identity
        );
        
        var corpseComp = corpse.GetComponent<Corpse>();
        if (corpseComp)
            corpseComp.Initialize(type);
    }
}