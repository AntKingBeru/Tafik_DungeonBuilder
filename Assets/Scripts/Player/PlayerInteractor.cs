using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteractor : MonoBehaviour
{
    [Header("Interaction")]
    [SerializeField] private float interactionRange = 2f;
    
    [Header("References")]
    [SerializeField] private Camera cam;

    private Corpse _hoveredCorpse;
    
    private void Update()
    {
        DetectCorpse();
    }

    public Vector2Int GetHoveredGridPosition()
    {
        var mouse = Mouse.current.position.ReadValue();

        var mouseWorld = cam.ScreenToWorldPoint(new Vector3(
            mouse.x,
            mouse.y,
            -cam.transform.position.z
        ));
        var world2D = new Vector2(mouseWorld.x, mouseWorld.y);
        
        return GridManager.Instance.WorldToGrid(world2D);
    }

    private void DetectCorpse()
    {
        var mouse = Mouse.current.position.ReadValue();

        var world = cam.ScreenToWorldPoint(new Vector3(
            mouse.x,
            mouse.y,
            -cam.transform.position.z
        ));
        
        var hit = Physics2D.Raycast(world, Vector2.zero);

        _hoveredCorpse = hit.collider ? hit.collider.GetComponent<Corpse>() : null;
    }
    
    public Corpse GetHoveredCorpse() => _hoveredCorpse;
}