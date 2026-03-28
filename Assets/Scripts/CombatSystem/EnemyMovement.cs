using UnityEngine;
using System.Collections.Generic;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float repathInterval = 2f;

    private Vector2Int _currentGridPos;
    private Vector3 _targetWorldPos;
    private bool _isMoving;
    
    private List<Vector2Int> _currentPath;
    private int _pathIndex;
    
    private float _repathTimer;

    private void Start()
    {
        _currentGridPos = GridManager.Instance.WorldToGrid(transform.position);
        _targetWorldPos = transform.position;
    }

    private void Update()
    {
        if (_isMoving)
        {
            MoveAlongPath();
        }
        else
            HandlePathing();
    }

    private void HandlePathing()
    {
        _repathTimer -= Time.deltaTime;

        if (_repathTimer <= 0f)
        {
            _repathTimer = repathInterval;

            var target = GetRandomRoomPosition();
            
            _currentPath = GridPathfinder.FindPath(
                _currentGridPos,
                target
            );
            
            _pathIndex = 0;
        }

        if (_currentPath is { Count: > 1 })
        {
            _pathIndex = 1;
            MoveTo(_currentPath[_pathIndex]);
        }
    }

    private void MoveAlongPath()
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
            
            _currentGridPos = GridManager.Instance.WorldToGrid(_targetWorldPos);
            
            _pathIndex++;
            
            if (_currentPath != null && _pathIndex < _currentPath.Count)
                MoveTo(_currentPath[_pathIndex]);
        }
    }

    private void MoveTo(Vector2Int gridPos)
    {
        _targetWorldPos = GridManager.Instance.GridToWorld(gridPos);
        _isMoving = true;
    }

    private Vector2Int GetRandomRoomPosition()
    {
        var rooms = GridManager.Instance.Rooms;
        
        if (rooms.Count == 0)
            return _currentGridPos;
        
        var room = rooms[Random.Range(0, rooms.Count)];
        
        return room.GetRandomCellPosition();
    }
}