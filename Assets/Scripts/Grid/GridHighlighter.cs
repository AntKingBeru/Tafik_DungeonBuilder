using UnityEngine;
using UnityEngine.InputSystem;

public class GridHighlighter : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Camera cam;
    [SerializeField] private PlayerStateMachine playerState;
    [SerializeField] private SpriteRenderer highlightSprite;

    private void Update()
    {
        if (playerState.CurrentMode != PlayerMode.Clear)
        {
            highlightSprite.enabled = false;
            return;
        }
        
        var mouse = Mouse.current.position.ReadValue();

        var mouseWorld = cam.ScreenToWorldPoint(new Vector3(
            mouse.x,
            mouse.y,
            -cam.transform.position.z
        ));
        var gridPos = GridManager.Instance.WorldToGrid(mouseWorld);

        if (!GridManager.Instance.IsInsideGrid(gridPos))
        {
            highlightSprite.enabled = false;
            return;
        }
        
        highlightSprite.enabled = true;
        transform.position = GridManager.Instance.GridToWorld(gridPos);
    }
}