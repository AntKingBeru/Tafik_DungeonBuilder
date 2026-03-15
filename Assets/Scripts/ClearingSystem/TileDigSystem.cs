using UnityEngine;

public class TileDigSystem : MonoBehaviour
{
    private void Update()
    {
        if (GridCursor.Instance.Mode != CursorMode.Dig)
            return;
        if (!GridCursor.Instance.ClickedThisFrame)
            return;

        var cellPos = GridCursor.Instance.CurrentCell;
        
        var cell = GridManager.Instance.GetCell(cellPos);

        if (cell == null || cell.IsCleared)
            return;

        cell.Clear();
    }
}