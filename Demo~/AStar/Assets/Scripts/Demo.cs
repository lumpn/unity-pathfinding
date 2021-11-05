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

    IEnumerator Start()
    {
        yield return new WaitForSeconds(1);
        Run();
        yield return new WaitForSeconds(1);

        Debug.Break();
    }

    [ContextMenu(nameof(Run))]
    public void Run()
    {
        var grid = new int[gridSize.x, gridSize.y];
        for (int x = 0; x < gridSize.x; x++)
        {
            for (int y = 0; y < gridSize.y; y++)
            {
                var node = CreateNode(x, y);
                grid[x, y] = node.id;
            }
        }

        // fully connected grid
        var graph = new Graph(nodes.Count);
        for (int x = 1; x < gridSize.x; x++)
        {
            graph.AddEdge(grid[x - 1, 0], grid[x, 0], 1 + Random.value);
        }
        for (int y = 1; y < gridSize.y; y++)
        {
            graph.AddEdge(grid[0, y - 1], grid[0, y], 1 + Random.value);
        }
        for (int x = 1; x < gridSize.x; x++)
        {
            for (int y = 1; y < gridSize.y; y++)
            {
                graph.AddEdge(grid[x - 1, y], grid[x, y], 1 + Random.value);
                graph.AddEdge(grid[x, y - 1], grid[x, y], 1 + Random.value);
            }
        }

        var start = grid[0, 0];
        var end = grid[gridSize.x - 1, gridSize.y - 1];

        var watch = new System.Diagnostics.Stopwatch();
        watch.Start();

        var algorithm = new Dijkstra();
        var path = algorithm.Search(graph, start, end);

        watch.Stop();
        Debug.LogFormat("Search took {0} ms", watch.ElapsedMilliseconds);

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
}
