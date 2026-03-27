using UnityEngine;

public class RevivalController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform spawnPoint;
    
    [Header("Minion Prefabs")]
    [SerializeField] private GameObject zombieMinionPrefab;
    [SerializeField] private GameObject skeletonMinionPrefab;
    
    private PlayerCarry _playerInRange;

    private void OnTriggerEnter2D(Collider2D other)
    {
        var carry = other.GetComponent<PlayerCarry>();
        if (carry)
            _playerInRange = carry;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        var carry = other.GetComponent<PlayerCarry>();
        if (carry)
            _playerInRange = null;   
    }

    public void TryRevive()
    {
        if (!_playerInRange)
            return;

        if (!_playerInRange.HasCorpse)
            return;

        var corpse = _playerInRange.GetCarriedCorpse();
        var type = corpse.GetCorpseType();
        
        SpawnMinion(type);
        
        Destroy(corpse.gameObject);
        _playerInRange.Drop();
    }

    private void SpawnMinion(CorpseType type)
    {
        var prefab = type switch
        {
            CorpseType.Zombie => zombieMinionPrefab,
            CorpseType.Skeleton => skeletonMinionPrefab,
            _ => zombieMinionPrefab
        };

        Instantiate(
            prefab,
            spawnPoint.position,
            Quaternion.identity
        );
    }
}