using UnityEngine;

public class BuildSystem : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Camera mainCamera;
    [SerializeField] private DungeonGrid dungeonGrid;
    [SerializeField] private RoomPreviewSystem previewSystem;
    [SerializeField] private PlayerControls input;
    
    [Header("Room Selection")]
    [SerializeField] private GameObject selectedRoomPrefab;
    [SerializeField] private Vector2Int selectedRoomSize = new Vector2Int(3, 3);

    private void OnEnable()
    {
        input.Builder.Enable();
    }

    private void OnDisable()
    {
        input.Builder.Disable();
    }

    private void Update()
    {
        HandlePreview();
        HandlePlacement();
        HandleCancel();
        HandleRotation();
    }

    private void HandlePreview()
    {
        if (!TryGetMouseGridPosition(out var gridPos)) return;
        
        var canPlace = dungeonGrid.CanPlaceRoom(gridPos, selectedRoomSize);
        
        previewSystem.UpdatePreview(gridPos, selectedRoomSize, canPlace);
    }

    private void HandlePlacement()
    {
        if (!input.Builder.Click.WasPressedThisFrame()) return;
        
        if (!TryGetMouseGridPosition(out var gridPos)) return;
            
        TryPlaceRoom(gridPos);
    }

    private void HandleCancel()
    {
        if (input.Builder.Cancel.WasPressedThisFrame()) previewSystem.HidePreview();
    }

    private void HandleRotation()
    {
        if (input.Builder.Rotation.WasReleasedThisFrame()) previewSystem.RotatePreview();
    }

    private bool TryGetMouseGridPosition(out Vector2Int gridPos)
    {
        gridPos = default;

        var mousePos = input.Builder.Point.ReadValue<Vector2>();
        
        var ray = mainCamera.ScreenPointToRay(mousePos);
        
        if (!Physics.Raycast(ray, out var hit)) return false;
        
        gridPos = dungeonGrid.WorldToGrid(hit.point);
        
        return true;
    }

    private void TryPlaceRoom(Vector2Int gridPos)
    {
        if (!dungeonGrid.CanPlaceRoom(gridPos, selectedRoomSize)) return;
        
        dungeonGrid.PlaceRoom(gridPos, selectedRoomSize, selectedRoomPrefab);
        
        previewSystem.ConfirmPlacement();
    }
}