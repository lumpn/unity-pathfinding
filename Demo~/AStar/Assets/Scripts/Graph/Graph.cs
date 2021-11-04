//----------------------------------------
// MIT License
// Copyright(c) 2021 Jonas Boetel
//----------------------------------------
using System.Collections.Generic;

namespace Lumpn.Graph
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

        public void AddEdge(int sourceId, int targetId, float cost)
        {
            var nodeEdges = edges.GetOrAddNew(sourceId);
            nodeEdges.Add(new Edge(targetId, cost));
        }

        public IEnumerable<Edge> GetEdges(int nodeId)
        {
            return edges.GetOrDefault(nodeId, emptyList);
        }
    }
}
