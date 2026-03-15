#if UNITY_EDITOR

using UnityEngine;

[ExecuteAlways]
public class GridDebugRenderer : MonoBehaviour
{
    [SerializeField] private bool showGrid = true;

    private void OnDrawGizmos()
    {
        if (!showGrid) return;
        if (!GridManager.Instance) return;
        
        var grid = GridManager.Instance.Grid;

        foreach (var pair in grid)
        {
            var cell = pair.Value;

            var pos = GridManager.Instance.CellToWorld(cell.GridPosition);

            if (!cell.IsCleared)
            {
                Gizmos.color = cell.ResourceType == ResourceType.Stone
                    ? Color.gray : new Color(0.5f, 0.3f, 0.1f);
            }
            else if (cell.Room)
            {
                Gizmos.color = Color.blue;
            }
            else
            {
                Gizmos.color = Color.green;
            }
            
            Gizmos.DrawWireCube(pos, Vector3.one);
        }
    }
}

#endif