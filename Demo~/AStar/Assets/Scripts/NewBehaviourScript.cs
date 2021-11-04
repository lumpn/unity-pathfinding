using UnityEngine;
using System.Linq;
using System.Diagnostics.Contracts;

public class NewBehaviourScript : MonoBehaviour
{
    void Start()
    {
        var n1 = new Node("Start");
        var n2 = new Node("End");

        var graph = new Graph();
        var idx1 = graph.AddNode(n1);
        var idx2 = graph.AddNode(n2);
        graph.AddEdge(idx1, idx2, 5f);

        var path = SearchDijkstra(graph, idx1, idx2);
        Debug.LogFormat("Path length {0}, cost {1}", path.length, path.cost);

        foreach (var node in path)
        {
            Debug.LogFormat("Node {0}", node.position);
        }
    }

    private static float CalculateHeuristic(IGraph graph, int idx1, int idx2)
    {
        return 0f;
    }

}
