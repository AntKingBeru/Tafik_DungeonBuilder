using UnityEngine;

public class StorageRoom : MonoBehaviour
{
    public void Store(ResourceType type, int amount)
    {
        ResourceManager.Instance.Add(type, amount);
    }
}