using UnityEngine;

public class RoomGhost : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    
    [Header("Colors")]
    [SerializeField] private Color validColor = new Color(0, 1, 0, 0.5f);
    [SerializeField] private Color invalidColor = new Color(1, 0, 0, 0.5f);
    
    public void SetValid(bool valid)
    {
        spriteRenderer.color = valid ? validColor : invalidColor;
    }
}