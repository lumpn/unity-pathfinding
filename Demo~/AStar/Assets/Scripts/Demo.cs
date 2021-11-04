//----------------------------------------
// MIT License
// Copyright(c) 2021 Jonas Boetel
//----------------------------------------
using System.Collections;
using System.Collections.Generic;
using Lumpn.Graph;
using UnityEngine;
using UnityEngine.Profiling;

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

    IEnumerator Start()
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

        var start = grid[0, 0];
        var end = grid[gridSize.x - 1, gridSize.y - 1];

        var sampler = CustomSampler.Create("Search", false);
        Debug.Assert(sampler.isValid);

        var recorder = sampler.GetRecorder();
        Debug.Assert(recorder.isValid);

        recorder.enabled = true;
        recorder.CollectFromAllThreads();

        yield return null;

        sampler.Begin(this);

        var algorithm = new Dijkstra();
        var path = algorithm.Search(graph, start.id, end.id);

        sampler.End();

        yield return null;

        Debug.Assert(sampler.isValid);
        Debug.Assert(recorder.isValid);
        Debug.Assert(recorder.sampleBlockCount > 0);

        Debug.LogFormat("Search took {0} ms", recorder.elapsedNanoseconds / 1000);

        if (path == null)
        {
            Debug.Log("No path");
            yield break;
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
