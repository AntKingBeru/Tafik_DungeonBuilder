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

    public void SpawnCorpse(Vector3 pos, CorpseType type)
    {
        var prefab = type == CorpseType.Skeleton
            ? skeletonCorpsePrefab
            : zombieCorpsePrefab;

        var corpse = Instantiate(prefab, pos, Quaternion.identity)
            .GetComponent<Corpse>();

        _corpses.Add(corpse);
    }
}