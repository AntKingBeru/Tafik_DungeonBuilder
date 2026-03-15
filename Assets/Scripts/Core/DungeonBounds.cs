using UnityEngine;

public class DungeonBounds : MonoBehaviour
{
    [SerializeField] private BoxCollider2D left;
    [SerializeField] private BoxCollider2D right;
    [SerializeField] private BoxCollider2D bottom;
    [SerializeField] private BoxCollider2D top;

    private void Start()
    {
        var width = GridManager.Instance.Width;
        var height = GridManager.Instance.Height;
        
        left.offset = new Vector2(-0.5f, height * 0.5f);
        left.size = new Vector2(1f, height);
        
        right.offset = new Vector2(width - 0.5f, height * 0.5f);
        right.size = new Vector2(1f, height);
        
        bottom.offset = new Vector2(width * 0.5f, -0.5f);
        bottom.size = new Vector2(width, 1f);
        
        top.offset = new Vector2(width * 0.5f, height - 0.5f);
        top.size = new Vector2(width, 1f);
    }
}