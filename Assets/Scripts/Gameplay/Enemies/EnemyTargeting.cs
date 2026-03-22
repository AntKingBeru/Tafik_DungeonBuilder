using UnityEngine;

public class EnemyTargeting : MonoBehaviour
{
    public static EnemyTargeting Instance { get; private set; }
    
    private Transform _coreTransform;

    private void Awake()
    {
        if (Instance && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void RegisterCore(Transform core)
    {
        _coreTransform = core;
    }

    public Transform GetTarget()
    {
        return _coreTransform;
    }
}