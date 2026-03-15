using UnityEngine;
using UnityEngine.InputSystem;

public class GridCursor : MonoBehaviour
{
    public static GridCursor Instance { get; private set; }
    public Vector2Int CurrentCell { get; private set; }
    public bool ClickedThisFrame { get; private set; }
    public CursorMode Mode { get; private set; }


    [Header("References")]
    [SerializeField] private Camera cam;
    [SerializeField] private SpriteRenderer sr;
    
    [Header("Input")]
    [SerializeField] private InputActionReference pointAction;
    [SerializeField] private InputActionReference clickAction;

    private void Awake()
    {
        Instance = this;
        
        SetMode(CursorMode.None);
    }

    private void OnEnable()
    {
        pointAction.action.Enable();
        clickAction.action.Enable();

        clickAction.action.performed += OnClick;
    }

    private void OnDisable()
    {
        clickAction.action.performed -= OnClick;
        
        pointAction.action.Disable();
        clickAction.action.Disable();
    }

    private void Update()
    {
        if (Mode == CursorMode.None)
            return;
        
        UpdateCursorPosition();
        ClickedThisFrame = false;
    }

    private void UpdateCursorPosition()
    {
        var mouseScreen = pointAction.action.ReadValue<Vector2>();
        
        var mouseWorld = cam.ScreenToWorldPoint(mouseScreen);
        mouseWorld.z = 0;
        
        var cell = GridManager.Instance.WorldToCell(mouseWorld);
        
        cell.x = Mathf.Clamp(cell.x, 0, GridManager.Instance.Width - 1);
        cell.y = Mathf.Clamp(cell.y, 0, GridManager.Instance.Height - 1);
        
        CurrentCell = GridManager.Instance.WorldToCell(mouseWorld);
        
        transform.position = GridManager.Instance.CellToWorld(cell);
    }

    private void OnClick(InputAction.CallbackContext context)
    {
        ClickedThisFrame = true;
    }

    public void SetMode(CursorMode newMode)
    {
        Mode = newMode;
        
        if (sr)
            sr.enabled = Mode != CursorMode.None;
    }
}