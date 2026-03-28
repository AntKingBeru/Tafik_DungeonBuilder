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
        var hit = RaycastMouse();
        return hit.collider ? hit.collider.GetComponent<TrapAnchor>() : null;
    }

    public Corpse GetHoveredCorpse()
    {
        var hit = RaycastMouse();
        return hit.collider ? hit.collider.GetComponent<Corpse>() : null;
    }

    public RevivalDropZone GetHoveredDropZone()
    {
        var hit = RaycastMouse();
        return hit.collider ? hit.collider.GetComponent<RevivalDropZone>() : null;
    }

    private RaycastHit2D RaycastMouse()
    {
        var world = GetMouseWorldPosition();
        return Physics2D.Raycast(world, Vector2.zero);
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