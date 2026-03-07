using UnityEngine;
using System.Collections.Generic;

public static class GridRectangleIterator
{
    public static IEnumerable<Vector2Int> Iterate(Vector2Int origin, int width, int height)
    {
        for (var x = 0; x < width; x++)
        {
            for (var y = 0; y < height; y++)
            {
                yield return new Vector2Int(origin.x + x, origin.y + y);
            }
        }
    }
}