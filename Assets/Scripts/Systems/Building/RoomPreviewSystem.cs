using UnityEngine;

public class RoomPreviewSystem : MonoBehaviour
{
    [SerializeField] private DungeonGrid dungeonGrid;
    [SerializeField] private GameObject previewObject;

    public void UpdatePreview(Vector2Int origin, Vector2Int size, bool canPlace)
    {
        var worldPos = dungeonGrid.GridToWorld(origin);
        
        previewObject.transform.position = worldPos;
        
        if (!previewObject.activeSelf) previewObject.SetActive(true);

        UpdateColor(canPlace);
    }

    public void RotatePreview()
    {
        previewObject.transform.rotation = Quaternion.Euler(0f, 90, 0f);
    }

    public void ConfirmPlacement()
    {
        // optional visual feedback
    }

    public void HidePreview()
    {
        previewObject.SetActive(false);
    }

    private void UpdateColor(bool canPlace)
    {
        var render = previewObject.GetComponentInChildren<SpriteRenderer>();

        if (!render) return;
        
        render.material.color = canPlace ? Color.green : Color.red;
    }
}