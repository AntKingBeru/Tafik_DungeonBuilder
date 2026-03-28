using UnityEngine;
using System.Collections.Generic;

public class MinionMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1.5f;

    private Queue<Vector2Int> _path;
    private Vector3 _targetWorld;
    
    public bool IsMoving { get; private set; }

    public void MoveTo(Vector2Int targetGrid)
    {
        var start = GridManager.Instance.WorldToGrid(transform.position);

        var path = GridPathfinder.Instance.FindPath(start, targetGrid);

        if (path == null || path.Count == 0)
            return;

        _path = new Queue<Vector2Int>(path);
        MoveNext();
    }

    private void MoveNext()
    {
        if (_path == null || _path.Count == 0)
        {
            IsMoving = false;
            return;
        }

        var nextGrid = _path.Dequeue();
        _targetWorld = GridManager.Instance.GridToWorld(nextGrid);
        IsMoving = true;
    }

    private void Update()
    {
        if (!IsMoving)
            return;

        transform.position = Vector3.MoveTowards(
            transform.position,
            _targetWorld,
            moveSpeed * Time.deltaTime
        );

        if (Vector3.Distance(transform.position, _targetWorld) < 0.05f)
        {
            transform.position = _targetWorld;
            MoveNext();
        }
    }
}