//----------------------------------------
// MIT License
// Copyright(c) 2021 Jonas Boetel
//----------------------------------------
using System.Collections.Generic;

namespace Lumpn.Pathfinding
{
    public sealed class Graph : IGraph
    {
        private static readonly List<Edge> emptyList = new List<Edge>();

        private readonly List<INode> nodes = new List<INode>();
        private readonly Dictionary<int, List<Edge>> edges = new Dictionary<int, List<Edge>>();

        public int nodeCount { get { return nodes.Count; } }

        public int AddNode(INode node)
        {
            var id = nodes.Count;
            nodes.Add(node);
            return id;
        }

        public INode GetNode(int nodeId)
        {
            return nodes[nodeId];
        }

        public IEnumerable<Edge> GetEdges(int nodeId)
        {
            return edges.GetOrDefault(nodeId, emptyList);
        }

        public void AddEdge(int sourceNodeId, int targetNodeId, float cost)
        {
            var nodeEdges = edges.GetOrAddNew(sourceNodeId);
            nodeEdges.Add(new Edge(targetNodeId, cost));
        }
    }
}
