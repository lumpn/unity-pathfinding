//----------------------------------------
// MIT License
// Copyright(c) 2021 Jonas Boetel
//----------------------------------------
using System.Collections.Generic;
using UnityEngine.Profiling;

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
            var sampler1 = CustomSampler.Create("Allocate");
            var sampler2 = CustomSampler.Create("Push");
            var sampler3 = CustomSampler.Create("Pop");
            var sampler4 = CustomSampler.Create("Reconstruct");

            sampler1.Begin();
            var nodeCount = graph.nodeCount;
            var explored = new bool[nodeCount];
            var discovered = new bool[nodeCount];
            var parents = new int[nodeCount];
            sampler1.End();

            var undiscoveredQueue = new Heap<HeapEntry>(comparer, graph.nodeCount);
            var discoveredQueue = new Heap<HeapEntry>(comparer, graph.nodeCount);

            parents[startId] = -1;
            sampler2.Begin();
            undiscoveredQueue.Push(new HeapEntry(startId, 0f));
            discovered[startId] = true;
            sampler2.End();

            while (undiscoveredQueue.Count > 0 || discoveredQueue.Count > 0)
            {
                sampler3.Begin();
                var entry = Pop(undiscoveredQueue, discoveredQueue);
                sampler3.End();
                var nodeId = entry.nodeId;
                if (nodeId == destinationId)
                {
                    sampler4.Begin();
                    var path = ReconstructPath(nodeId, entry.cost, parents);
                    sampler4.End();
                    return path;
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

                        var queue = discovered[targetId] ? discoveredQueue : undiscoveredQueue;
                        sampler2.Begin();
                        queue.Push(new HeapEntry(targetId, cost));
                        discovered[targetId] = true;
                        sampler2.End();
                    }
                }
            }

            return null;
        }

        private static HeapEntry Pop(Heap<HeapEntry> heap1, Heap<HeapEntry> heap2)
        {
            if (heap1.Count <= 0)
            {
                return heap2.Pop();
            }
            if (heap2.Count <= 0)
            {
                return heap1.Pop();
            }

            var entry1 = heap1.Peek();
            var entry2 = heap2.Peek();
            var entry = (entry1.cost <= entry2.cost) ? heap1.Pop() : heap2.Pop();

            return entry;
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
