//----------------------------------------
// MIT License
// Copyright(c) 2021 Jonas Boetel
//----------------------------------------
using System.Collections.Generic;

namespace Lumpn.Graph
{
    public sealed class Dijkstra
    {
        private struct Node
        {
            public readonly int id;
            public int parent;
            public bool explored;

            public Node(int id)
            {
                this.id = id;
                this.parent = -1;
                this.explored = false;
            }
        }

        private struct HeapEntry
        {
            public readonly int nodeId;
            public readonly float cost;

            public HeapEntry(int nodeId, float cost)
            {
                this.nodeId = nodeId;
                this.cost = cost;
            }
        }

        private sealed class HeapEntryComparer : IComparer<HeapEntry>
        {
            private static readonly IComparer<float> costComparer = Comparer<float>.Default;

            public int Compare(HeapEntry x, HeapEntry y)
            {
                return costComparer.Compare(x.cost, y.cost);
            }
        }

        private static readonly HeapEntryComparer comparer = new HeapEntryComparer();

        public Path Search(IGraph graph, int startId, int destinationId)
        {
            var nodeCount = graph.nodeCount;
            var explored = new bool[nodeCount];
            var parents = new int[nodeCount];

            var queue = new Heap<HeapEntry>(comparer, graph.nodeCount);

            parents[startId] = -1;
            queue.Push(new HeapEntry(startId, 0f));

            while (queue.Count > 0)
            {
                var entry = queue.Pop();
                var nodeId = entry.nodeId;
                if (nodeId == destinationId)
                {
                    return ReconstructPath(nodeId, entry.cost, parents);
                }

                if (explored[nodeId]) continue;
                explored[nodeId] = true;

                var firstEdgeId = graph.GetFirstEdgeId(nodeId);
                var lastEdgeId = graph.GetLastEdgeId(nodeId);

                for (int edgeId = firstEdgeId; edgeId <= lastEdgeId; edgeId++)
                {
                    var edge = graph.GetEdge(edgeId);

                    var targetId = edge.target;
                    if (!explored[targetId])
                    {
                        var cost = entry.cost + edge.cost;
                        parents[targetId] = nodeId;
                        queue.Push(new HeapEntry(targetId, cost));
                    }
                }
            }

            return null;
        }

        private static Path ReconstructPath(int lastId, float cost, int[] parents)
        {
            var nodes = new List<int>();
            for (var id = lastId; id >= 0; id = parents[id])
            {
                nodes.Add(id);
            }
            nodes.Reverse();

            return new Path(cost, nodes);
        }
    }
}
