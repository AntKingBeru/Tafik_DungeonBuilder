using UnityEngine;

public class RevivalRoom : MonoBehaviour
{
    [SerializeField] private Minion zombiePrefab;
    [SerializeField] private Minion skeletonPrefab;

    public void Revive(Corpse corpse)
    {
        var prefab = corpse.Type == CorpseType.Skeleton
            ? skeletonPrefab
            : zombiePrefab;

        Instantiate(prefab, transform.position, Quaternion.identity);
    }
}