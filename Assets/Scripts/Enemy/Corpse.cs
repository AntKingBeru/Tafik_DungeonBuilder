using UnityEngine;

public class Corpse : MonoBehaviour
{
    [SerializeField] private CorpseType type;

    public CorpseType Type => type;

    public void PickUp()
    {
        gameObject.SetActive(false);
    }
}