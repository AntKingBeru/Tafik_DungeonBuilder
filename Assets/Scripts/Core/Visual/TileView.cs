using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class TileView : MonoBehaviour
{
    [Header("Sprites")]
    [SerializeField] private Sprite stoneSprite;
    [SerializeField] private Sprite woodSprite;
    [SerializeField] private Sprite clearedSprite;
    
    [Header("Highlight")]
    [SerializeField] private GameObject highlightObject;
    
    [Header("References")]
    [SerializeField] private SpriteRenderer spriteRenderer;

    public Vector2Int GridPosition { get; private set; }

    private void Awake()
    {
        if (highlightObject)
            highlightObject.SetActive(false);
    }

    public void Initialize(Vector2Int pos)
    {
        GridPosition = pos;
    }

    public void SetState(TileState state, ResourceType resourceType)
    {
        spriteRenderer.sprite = state switch
        {
            TileState.Blocked => GetBlockedSprite(resourceType),
            TileState.Cleared => clearedSprite,
            _ => spriteRenderer.sprite
        };
    }

    private Sprite GetBlockedSprite(ResourceType type)
    {
        return type switch
        {
            ResourceType.Stone => stoneSprite,
            ResourceType.Wood => woodSprite,
            _ => stoneSprite
        };
    }

    public void SetHighlight(bool value)
    {
        if (highlightObject)
            highlightObject.SetActive(value);
    }
}