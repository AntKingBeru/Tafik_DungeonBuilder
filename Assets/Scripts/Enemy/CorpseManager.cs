using UnityEngine;

public class CorpseManager : MonoBehaviour
{
    public static CorpseManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public void SpawnCorpse(Vector3 pos, CorpseType type)
    {
        var corpse = Instantiate(
            /* prefab */,
            pos,
            Quaternion.identity
        );
        
        JobManager.Instance.AddJob(new HaulCorpseJob(corpse));
    }
}