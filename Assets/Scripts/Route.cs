using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Route : MonoBehaviour
{
    public GameObject routeObject;
    public GameObject targetObject;

    // Tilemap
    private Tilemap _tilemap;
    private bool[,] _graph;
    private Vector2Int _startPosition;
    private Vector2Int _endPosition;
    private List<Vector2Int> _path = new List<Vector2Int>();
    private List<Vector3> _pathWorldPosition = new List<Vector3>();
    
    public List<Vector3> Path { get { return _pathWorldPosition; } }

    private void Awake()
    {
        _tilemap = routeObject.GetComponent<Tilemap>();
        SetGraph();

        _startPosition = (Vector2Int)_tilemap.WorldToCell(transform.position);
        _endPosition = (Vector2Int)_tilemap.WorldToCell(targetObject.transform.position);

        Vector2Int origin = (Vector2Int)_tilemap.origin;
        Vector3 originLocalPosition = _tilemap.GetCellCenterLocal((Vector3Int)origin);
        _startPosition -= origin;
        _endPosition -= origin;
        
        FindShortestPath(_startPosition, _endPosition, ref _graph, out _path);

        foreach (Vector2Int position in _path)
        {
            Vector3 worldPosition = _tilemap.CellToWorld((Vector3Int)position) + originLocalPosition;
            _pathWorldPosition.Add(worldPosition);
        }
    }
    
    private void SetGraph()
    {
        // Get all tiles in a tilemap
        // https://gamedev.stackexchange.com/a/150949
        BoundsInt bounds = _tilemap.cellBounds;
        TileBase[] tiles = _tilemap.GetTilesBlock(bounds);
        
        _graph = new bool[bounds.size.y, bounds.size.x];

        for (int y = 0; y < bounds.size.y; y ++)
        {

            for (int x = 0; x < bounds.size.x; x ++)
            {
                TileBase tile = tiles[x + y * bounds.size.x];
                _graph[y, x] = tile != null;
            }
        }
    }

    private int ManhattanDisatnace(Vector2Int current, Vector2Int goal)
    {
        return Mathf.Abs(current.x - goal.x) + Mathf.Abs(current.y - goal.y);
    }

    private void FindShortestPath (
        Vector2Int start,
        Vector2Int end,
        ref bool[,] graph,
        out List<Vector2Int> shortestPath
        )
    {
        shortestPath = new List<Vector2Int>();
        Vector2Int[] directions =
        {
            Vector2Int.up,
            Vector2Int.left,
            Vector2Int.right,
            Vector2Int.down,
        };

        int height = graph.GetLength(0);
        int width = graph.GetLength(1);

        if (start.x < 0 || start.x > width - 1 || start.y < 0 || start.y > height - 1)
        {
            Debug.LogWarning("The start position is out of range");
            return;
        }
        
        if (end.x < 0 || end.x > width - 1 || end.y < 0 || end.y > height - 1)
        {
            Debug.LogWarning("The end position is out of range");
            return;
        }

        if (!graph[start.y, start.x])
        {
            Debug.LogWarning("The start position is wrong");
            return;
        }

        if (!graph[end.y, end.x])
        {
            Debug.LogWarning("The end position is wrong");
            return;
        }

        int[,] distances = new int[height, width];
        bool[,] visited = new bool[height, width];
        Vector2Int[,] prevNode = new Vector2Int[height, width];
        PriorityQueue<int, Vector2Int> nodes = new PriorityQueue<int, Vector2Int>(
            (a, b) => a.Priority < b.Priority
        );

        for (int y = 0; y < height; y ++)
        {
            for (int x = 0; x < width; x ++)
            {
                distances[y, x] = Int32.MaxValue;
                visited[y, x] = false;
                prevNode[y, x] = new Vector2Int(-1, -1);
            }
        }

        nodes.Push(0, start);
        distances[start.y, start.x] = 0;

        while (!nodes.IsEmpty())
        {
            var node = nodes.Pop();
            var position = node.Value;
            
            if (position == end) break;
            visited[position.y, position.x] = true;

            foreach (var direction in directions)
            {
                var nextPosition = position + direction;
                if (nextPosition.x < 0 || nextPosition.x > width - 1 ||
                    nextPosition.y < 0 || nextPosition.y > height - 1) continue;
                if (!graph[nextPosition.y, nextPosition.x]) continue;

                // nextDistance : 출발 지점에 nextPosition을 거쳐서 목표 지점까지 갈때의 최단 거리
                int nextDistance = node.Priority + ManhattanDisatnace(nextPosition, end);

                if (distances[nextPosition.y, nextPosition.x] > nextDistance)
                {
                    distances[nextPosition.y, nextPosition.x] = nextDistance;
                    prevNode[nextPosition.y, nextPosition.x] = position;
                }

                if (!visited[nextPosition.y, nextPosition.x])
                    nodes.Push(distances[nextPosition.y, nextPosition.x], nextPosition);
            }
        }
        
        Vector2Int current = end;

        while (current != start)
        {
            shortestPath.Add(current);
            current = prevNode[current.y, current.x];

            if (current.x == -1 && current.y == -1) break; // 오류: 일어나면 안되는 일
        }
        
        shortestPath.Reverse();
    }
}
