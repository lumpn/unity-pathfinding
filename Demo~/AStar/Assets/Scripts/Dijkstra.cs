using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Dijkstra : MonoBehaviour
{
    internal class SearchNode
    {
        internal float cost;
        internal bool explored;
        internal SearchNode parent;
        internal int idx;
    }

    public Path Search(IGraph graph, int idx1, int idx2)
    {
        var searchNodes = graph.GetNodes().Select(p => new SearchNode()).ToArray();

        var frontier = new PriorityQueue<int>();
        frontier.Enqueue(idx1, 0);

        while (frontier.Count > 0)
        {
            var idx = frontier.Dequeue();
            var node = searchNodes[idx];

            if (idx == idx2)
            {
                return ReconstructPath(node);
            }

            if (node.explored) continue;
            node.explored = true;

            foreach (var edge in graph.GetEdges(idx))
            {
                var dest = edge.idx;
                var destNode = searchNodes[dest];
                if (!destNode.explored)
                {
                    var cost = node.cost + edge.cost;
                    destNode.cost = cost;
                    destNode.parent = node;
                    frontier.Enqueue(dest, cost);
                }
            }
        }

        return null;
    }

    private static Path ReconstructPath(SearchNode node)
    {
        var path = new Path(node.cost);
        for (var current = node; current != null; current = current.parent)
        {
            path.Add(current.idx);
        }
        return path;
    }
}
