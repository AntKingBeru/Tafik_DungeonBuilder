using UnityEngine;
using System.Collections.Generic;

public class SpriteSortManager : MonoBehaviour
{
    public static SpriteSortManager Instance;
    
    private readonly List<SortableSprite> _sprites = new();

    private void Awake()
    {
        Instance = this;
    }

    public void Register(SortableSprite sprite)
    {
        if (!_sprites.Contains(sprite)) _sprites.Add(sprite);
    }
    
    public void Unregister(SortableSprite sprite)
    {
        _sprites.Remove(sprite);
    }

    private void LateUpdate()
    {
        foreach (var sprite in _sprites)
        {
            if (!sprite) continue;
            
            var sr = sprite.GetSpriteRenderer();
            
            sr.sortingOrder = Mathf.RoundToInt(-sprite.GetSortY() * 100);
        }
    }
}