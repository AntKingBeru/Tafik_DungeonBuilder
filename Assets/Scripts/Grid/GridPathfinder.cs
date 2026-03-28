using UnityEngine;
using System.Collections.Generic;

public static class GridPathfinder
{
    private static readonly Vector2Int[] Directions =
    {
        Vector2Int.up,
        Vector2Int.down,
        Vector2Int.left,
        Vector2Int.right
    };
    
    public static List<Vector2Int> FindPath(Vector2Int start, Vector2Int target)
    {
        var openSet = new List<Vector2Int> { start };
        var cameFrom = new Dictionary<Vector2Int, Vector2Int>();

        var gScore = new Dictionary<Vector2Int, int>
        {
            [start] = 0
        };

        var fScore = new Dictionary<Vector2Int, int>
        {
            [start] = Heuristic(start, target)
        };

        while (openSet.Count > 0)
        {
            var current = GetLowestFScore(openSet, fScore);
            
            if (current == target)
                return ReconstructPath(cameFrom, current);
            
            openSet.Remove(current);

            foreach (var dir in Directions)
            {
                var neighbor = current + dir;
                
                if (!GridManager.Instance.IsWalkable(neighbor))
                    continue;
                
                var tentativeG  = gScore[current] + 1;

                if (!gScore.ContainsKey(neighbor) || tentativeG < gScore[neighbor])
                {
                    cameFrom[neighbor] = current;
                    gScore[neighbor] = tentativeG;
                    fScore[neighbor] = tentativeG + Heuristic(neighbor, target);
                    
                    if (!openSet.Contains(neighbor))
                        openSet.Add(neighbor);
                }
            }
        }
        
        return null;
    }

    private static int Heuristic(Vector2Int a, Vector2Int b)
    {
        return Mathf.Abs(a.x - b.x) + Mathf.Abs(a.y - b.y);
    }

    private static Vector2Int GetLowestFScore(List<Vector2Int> list, Dictionary<Vector2Int, int> fScore)
    {
        var best = list[0];
        var bestScore = fScore.GetValueOrDefault(best, int.MaxValue);

        foreach (var node in list)
        {
            var score = fScore.GetValueOrDefault(node, int.MaxValue);

            if (score < bestScore)
            {
                best = node;
                bestScore = score;
            }
        }
        
        return best;
    }

    private static List<Vector2Int> ReconstructPath(Dictionary<Vector2Int, Vector2Int> cameFrom, Vector2Int current)
    {
        var path = new List<Vector2Int> { current };
        
        while (cameFrom.ContainsKey(current))
        {
            current = cameFrom[current];
            path.Insert(0, current);
        }
        
        return path;
    }
}