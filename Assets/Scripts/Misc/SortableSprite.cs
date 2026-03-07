using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SortableSprite : MonoBehaviour
{
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Transform sortPoint;

    private void Awake()
    {
        if (!sortPoint) sortPoint = transform;
    }

    private void OnEnable()
    { 
        if (SpriteSortManager.Instance) SpriteSortManager.Instance.Register(this);
    }

    private void OnDisable()
    {
        if (SpriteSortManager.Instance) SpriteSortManager.Instance.Unregister(this);
    }

    public float GetSortY()
    {
        return sortPoint ? sortPoint.position.y : transform.position.y;
    }

    public SpriteRenderer GetSpriteRenderer()
    {
        return sr;
    }
}