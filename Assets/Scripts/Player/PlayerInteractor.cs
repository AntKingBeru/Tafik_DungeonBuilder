using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteractor : MonoBehaviour
{
    [Header("Interaction")]
    [SerializeField] private float interactionRange = 2f;
    
    [Header("References")]
    [SerializeField] private Camera cam;

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
}