using UnityEngine;

public class RevivalRoom : MonoBehaviour
{
    public static RevivalRoom Instance { get; private set; }
    
    [Header("References")]
    [SerializeField] private Transform dropOffPoint;
    [SerializeField] private Transform spawnPoint;
    
    [Header("Minion Prefab")]
    [SerializeField] private GameObject zombiePrefab;
    [SerializeField] private GameObject skeletonPrefab;

    private void Awake()
    {
        Instance = this;
    }

    public Vector2 GetDropOffPosition()
    {
        return dropOffPoint.position;
    }

    public void ReceiveCorpse(Corpse corpse)
    {
        if (!corpse)
            return;
        
        var type = corpse.Type;
        
        Destroy(corpse.gameObject);
        
        SpawnMinion(type, spawnPoint.position);
    }

    private void SpawnMinion(CorpseType type, Vector2 position)
    {
        var prefab = type switch
        {
            CorpseType.Zombie => zombiePrefab,
            CorpseType.Skeleton => skeletonPrefab,
            _ => zombiePrefab
        };
        
        var obj = Instantiate(
            prefab,
            position,
            Quaternion.identity
        );

        var minion = obj.GetComponent<Minion>();
        if (minion)
            minion.Initialize(type);
    }
}