//----------------------------------------
// MIT License
// Copyright(c) 2021 Jonas Boetel
//----------------------------------------
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Lumpn.Graph
{
    public sealed class Graph : IGraph
    {
        private struct CacheEntry
        {
            public int start;
            public int count;
        }

        private readonly List<Edge> emptyList = new List<Edge>();

        private readonly Dictionary<int, List<Edge>> edges = new Dictionary<int, List<Edge>>();

        private Edge[] edgeCache;
        private CacheEntry[] edgeMap;

        public int nodeCount { get; private set; }

        public Graph(int nodeCount)
        {
            this.nodeCount = nodeCount;
        }

        public void Process()
        {
            var numEdges = edges.Values.Sum(p => p.Count);

            edgeCache = new Edge[numEdges];
            edgeMap = new CacheEntry[nodeCount];

            var floatComparer = Comparer<float>.Default;

            int edgeId = 0;
            for (int i = 0; i < nodeCount; i++)
            {
                var nodeEdges = edges.GetOrDefault(i, emptyList);
                nodeEdges.Sort((a, b) => floatComparer.Compare(a.cost, b.cost));

                edgeMap[i] = new CacheEntry
                {
                    start = edgeId,
                    count = nodeEdges.Count,
                };
                foreach (var edge in nodeEdges)
                {
                    edgeCache[edgeId] = edge;
                    edgeId++;
                }
            }
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

        public int GetFirstEdgeId(int nodeId)
        {
            return edgeMap[nodeId].start;
        }

        public int GetLastEdgeId(int nodeId)
        {
            var entry = edgeMap[nodeId];
            return entry.start + entry.count - 1;
        }

        public Edge GetEdge(int edgeId)
        {
            return edgeCache[edgeId];
        }
    }
}
