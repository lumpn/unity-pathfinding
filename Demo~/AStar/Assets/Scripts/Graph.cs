using System.Collections.Generic;

public class Graph : IGraph
{
    private readonly List<Edge> emptyList = new List<Edge>();

    private readonly List<Node> nodes = new List<Node>();

    private readonly Dictionary<int, List<Edge>> edges = new Dictionary<int, List<Edge>>();

    public int nodeCount => nodes.Count;

    public int AddNode(Node node)
    {
        nodes.Add(node);
        return nodes.Count;
    }

    public void AddEdge(int sourceId, int targetId, float cost)
    {
        var nodeEdges = edges.GetOrAddNew(sourceId);
        nodeEdges.Add(new Edge(targetId, cost));
    }

    public Node GetNode(int id)
    {
        return nodes[id];
    }

    public IEnumerable<Edge> GetEdges(int nodeId)
    {
        return edges.GetOrDefault(nodeId, emptyList);
    }
}
