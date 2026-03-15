using UnityEngine;
using UnityEngine.InputSystem;

public class RoomInstance : MonoBehaviour
{
    [SerializeField] private Vector2Int size;
    [SerializeField] private Transform playerSpawnPoint;

    public Vector2Int Size => size;
    public Vector2Int Origin { get; private set; }
    public Transform PlayerSpawnPoint => playerSpawnPoint;

    public void Initialize(Vector2Int originCell)
    {
        Origin = originCell;
        
        GridManager.Instance.RegisterRoom(this, Origin, size);
    }

    public void SetPreview(bool preview)
    {
        var renderers = GetComponentsInChildren<SpriteRenderer>();
        
        foreach (var r in renderers)
        {
            var c = r.color;
            c.a = preview ? 0.5f : 1f;
            r.color = c;
        }
    }
}