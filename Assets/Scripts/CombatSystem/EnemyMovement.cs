using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f;

    private Vector2Int _currentGridPos;
    private Vector3 _targetWorldPos;
    private bool _isMoving;

    private void Start()
    {
        _currentGridPos = GridManager.Instance.WorldToGrid(transform.position);
        _targetWorldPos = transform.position;
    }

    private void Update()
    {
        if (_isMoving)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                _targetWorldPos,
                moveSpeed * Time.deltaTime
            );

            if (Vector3.Distance(transform.position, _targetWorldPos) < 0.01f)
            {
                transform.position = _targetWorldPos;
                _isMoving = false;
            }
        }
        else
            TryMoveRandom();
    }

    private void TryMoveRandom()
    {
        var directions = new[]
        {
            Vector2Int.up,
            Vector2Int.down,
            Vector2Int.left,
            Vector2Int.right
        };
        
        var dir = directions[Random.Range(0, directions.Length)];
        var next = _currentGridPos + dir;

        if (!GridManager.Instance.IsInsideGrid(next))
            return;
        
        var cell = GridManager.Instance.GetCell(next);

        if (cell == null)
            return;

        if (cell.Type != CellType.Room)
            return;
        
        MoveTo(next);
    }

    private void MoveTo(Vector2Int gridPos)
    {
        _currentGridPos = gridPos;
        _targetWorldPos = GridManager.Instance.GridToWorld(gridPos);
        _isMoving = true;
    }
}