using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteractor : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Camera cam;

    public Vector2Int GetHoveredGridPosition()
    {
        var world = GetMouseWorldPosition();
        return GridManager.Instance.WorldToGrid(world);
    }

    public TrapAnchor GetHoveredAnchor()
    {
        var col = RaycastMouse();
        return col ? col.GetComponent<TrapAnchor>() : null;
    }

    public Corpse GetHoveredCorpse()
    {
        var col = RaycastMouse();
        return col ? col.GetComponent<Corpse>() : null;
    }

    public RevivalDropZone GetHoveredDropZone()
    {
        var col = RaycastMouse();
        return col ? col.GetComponent<RevivalDropZone>() : null;
    }

    private Collider2D RaycastMouse()
    {
        var world = GetMouseWorldPosition();
        return Physics2D.OverlapPoint(world);
    }

    private Vector3 GetMouseWorldPosition()
    {
        var mouse = Mouse.current.position.ReadValue();

        return cam.ScreenToWorldPoint(new Vector3(
            mouse.x,
            mouse.y,
            -cam.transform.position.z
        ));
    }
}