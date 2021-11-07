//----------------------------------------
// MIT License
// Copyright(c) 2021 Jonas Boetel
//----------------------------------------
using System.Collections.Generic;

namespace Lumpn.Pathfinding
{
    public sealed class Graph : IGraph
    {
        private readonly List<Edge> emptyList = new List<Edge>();

        private readonly Dictionary<int, List<Edge>> edges = new Dictionary<int, List<Edge>>();

        public int nodeCount { get; private set; }

        public Graph(int nodeCount)
        {
            this.nodeCount = nodeCount;
        }

        public void AddEdge(int sourceNodeId, int targetNodeId, float cost)
        {
            var nodeEdges = edges.GetOrAddNew(sourceNodeId);
            nodeEdges.Add(new Edge(targetNodeId, cost));
        }

        public IEnumerable<Edge> GetEdges(int nodeId)
        {
            return edges.GetOrDefault(nodeId, emptyList);
        }
    }
}
