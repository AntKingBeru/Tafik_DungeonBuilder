using UnityEngine;

public class DungeonCellView : MonoBehaviour
{
    [SerializeField] private SpriteRenderer sr;

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
            return;
        }

        sr.sprite = _cell.ResourceType switch
        {
            ResourceType.Stone => stoneSprite,
            ResourceType.Wood => woodSprite,
            _ => stoneSprite
        };
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