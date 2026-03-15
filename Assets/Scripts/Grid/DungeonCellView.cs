using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(BoxCollider2D))]
public class DungeonCellView : MonoBehaviour
{
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private BoxCollider2D col;

    [Header("Sprites")]
    [SerializeField] private Sprite stoneSprite;
    [SerializeField] private Sprite woodSprite;
    [SerializeField] private Sprite clearedSprite;

    private DungeonCell _cell;

    public void Initialize(DungeonCell cellData)
    {
        _cell = cellData;
        UpdateVisual();
    }

    public void UpdateVisual()
    {
        if (_cell == null)
            return;
        
        if (_cell.IsCleared)
        {
            sr.sprite = clearedSprite;
            
            if (col)
                col.enabled = false;
        }
        else
        {
            sr.sprite = _cell.ResourceType == ResourceType.Stone
                ? stoneSprite
                : woodSprite;
            
            if (col)
                col.enabled = true;
        }
    }

    public void SetHighlight(Color color)
    {
        sr.color = color;
    }

    public void ClearHighlight()
    {
        sr.color = Color.white;
    }
}