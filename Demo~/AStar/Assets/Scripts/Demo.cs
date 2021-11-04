//----------------------------------------
// MIT License
// Copyright(c) 2021 Jonas Boetel
//----------------------------------------
using System.Collections;
using System.Collections.Generic;
using Lumpn.Graph;
using UnityEngine;

public class Demo : MonoBehaviour
{
    [SerializeField] private Vector2Int gridSize;
    [SerializeField] private GameObject horizontalEdgePrefab;
    [SerializeField] private GameObject verticalEdgePrefab;
    [SerializeField, Range(0, 1)] private float edgeProbability;

    private readonly List<Node> nodes = new List<Node>();

    public Node CreateNode(int x, int y)
    {
        var id = nodes.Count;
        var node = new Node(id, $"{x}, {y}", new Vector3(x, y, 0));
        nodes.Add(node);
        return node;
    }

    public Node GetNode(int id)
    {
        return nodes[id];
    }

    [ContextMenu(nameof(Start))]
    void Start()
    {
        var grid = new Node[gridSize.x, gridSize.y];
        for (int x = 0; x < gridSize.x; x++)
        {
            for (int y = 0; y < gridSize.y; y++)
            {
                var node = CreateNode(x, y);
                grid[x, y] = node;
            }
        }

        var gridGo = new GameObject("Grid")
        {
            hideFlags = HideFlags.DontSave
        };

        var graph = new Graph(nodes.Count);
        for (int x = 0; x < gridSize.x; x++)
        {
            for (int y = 0; y < gridSize.y; y++)
            {
                if (x > 0 && Random.value < edgeProbability)
                {
                    graph.AddEdge(grid[x - 1, y].id, grid[x, y].id, 1f);

                    //var position = new Vector3(x - 1, y, 0);
                    //Object.Instantiate(horizontalEdgePrefab, position, Quaternion.identity, gridGo.transform);
                }
                if (y > 0 && Random.value < edgeProbability)
                {
                    graph.AddEdge(grid[x, y - 1].id, grid[x, y].id, 1f);

                    //var position = new Vector3(x, y - 1, 0);
                    //Object.Instantiate(verticalEdgePrefab, position, Quaternion.identity, gridGo.transform);
                }
            }
        }

        graph.Process();

        var start = grid[0, 0];
        var end = grid[gridSize.x - 1, gridSize.y - 1];

        var stopwatch = new System.Diagnostics.Stopwatch();
        stopwatch.Start();

        var algorithm = new Dijkstra();
        var path = algorithm.Search(graph, start.id, end.id);

        stopwatch.Stop();

        Debug.LogFormat("Search took {0} ms", stopwatch.ElapsedMilliseconds);

        if (path == null)
        {
            Debug.Log("No path");
            return;
        }

        Debug.LogFormat("Path length {0}, cost {1}", path.length, path.cost);

        var pathGo = new GameObject("Path");
        foreach (var id in path)
        {
            var node = GetNode(id);

            var sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            sphere.transform.SetParent(pathGo.transform, false);
            sphere.transform.localPosition = node.position;
        }
    }

    private static float CalculateHeuristic(IGraph graph, int idx1, int idx2)
    {
        return 0f;
    }
}
