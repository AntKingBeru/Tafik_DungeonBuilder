using UnityEngine;

public class Corpse : MonoBehaviour
{
    public CorpseType Type { get; private set; }

    public void Initialize(CorpseType type)
    {
        Type = type;
    }

    public void OnPickedUp(Transform parent)
    {
        transform.SetParent(parent);
        transform.localPosition = Vector3.up * 0.5f;
    }
}