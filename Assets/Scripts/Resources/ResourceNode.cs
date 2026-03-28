using UnityEngine;

public class ResourceNode : MonoBehaviour
{
    private void Start()
    {
        JobManager.Instance.AddJob(new ResourceJob(this));
    }

    public void Harvest()
    {
        Destroy(gameObject);
    }
}