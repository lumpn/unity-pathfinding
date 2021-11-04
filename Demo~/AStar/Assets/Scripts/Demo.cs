//----------------------------------------
// MIT License
// Copyright(c) 2021 Jonas Boetel
//----------------------------------------
using Lumpn.Graph;
using UnityEngine;

public class Demo : MonoBehaviour
{
    [ContextMenu(nameof(Start))]
    void Start()
    {
        var n1 = new Node("Start");
        var n2 = new Node("End");

        var graph = new Graph();
        var idx1 = graph.AddNode(n1);
        var idx2 = graph.AddNode(n2);
        graph.AddEdge(idx1, idx2, 5f);

        var algorithm = new Dijkstra();
        var path = algorithm.Search(graph, idx1, idx2);
        Debug.LogFormat("Path length {0}, cost {1}", path.length, path.cost);

        foreach (var idx in path)
        {
            var node = graph.GetNode(idx);
            Debug.LogFormat("Node {0}", node.name);
        }
    }

    private static float CalculateHeuristic(IGraph graph, int idx1, int idx2)
    {
        return 0f;
    }
}
