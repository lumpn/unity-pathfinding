//----------------------------------------
// MIT License
// Copyright(c) 2021 Jonas Boetel
//----------------------------------------
using System.Collections.Generic;

namespace Lumpn.Graph
{
    public sealed class AStar
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
            public readonly float f_score;
            public readonly float g_score;

            public HeapEntry(int nodeId, float g_score, float heuristic)
            {
                this.nodeId = nodeId;
                this.g_score = g_score;
                this.f_score = g_score + heuristic;
            }
        }

        private sealed class HeapEntryComparer : IComparer<HeapEntry>
        {
            private static readonly IComparer<float> costComparer = Comparer<float>.Default;

            public int Compare(HeapEntry x, HeapEntry y)
            {
                return costComparer.Compare(x.f_score, y.f_score);
            }
        }

        private static readonly HeapEntryComparer comparer = new HeapEntryComparer();

        public delegate float CalculateHeuristic(IGraph graph, int startId, int destinationId);

        public Path Search(IGraph graph, int startId, int destinationId, CalculateHeuristic calculateHeuristic)
        {
            var nodeCount = graph.nodeCount;
            var explored = new bool[nodeCount];
            var parents = new int[nodeCount];

            var queue = new Heap<HeapEntry>(comparer, graph.nodeCount);

            parents[startId] = -1;
            queue.Push(new HeapEntry(startId, 0f, calculateHeuristic(graph, startId, destinationId)));

            while (queue.Count > 0)
            {
                var entry = queue.Pop();
                var nodeId = entry.nodeId;
                if (nodeId == destinationId)
                {
                    var path = ReconstructPath(nodeId, entry.g_score, parents);
                    return path;
                }

                if (explored[nodeId]) continue;
                explored[nodeId] = true;

                foreach (var edge in graph.GetEdges(nodeId))
                {
                    var targetId = edge.target;
                    if (!explored[targetId])
                    {
                        var cost = entry.g_score + edge.cost;

                        parents[targetId] = nodeId;
                        queue.Push(new HeapEntry(targetId, cost, calculateHeuristic(graph, targetId, destinationId)));
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
