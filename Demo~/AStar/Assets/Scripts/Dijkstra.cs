using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Lumpn.Graph;
using UnityEngine;

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
            var nodes = Enumerable.Range(0, graph.nodeCount).Select(id => new Node(id)).ToArray();

            var frontier = new Heap<HeapEntry>(comparer, graph.nodeCount);
            frontier.Push(new HeapEntry(startId, 0f));

            while (frontier.Count > 0)
            {
                var entry = frontier.Pop();
                var id = entry.nodeId;
                if (id == destinationId)
                {
                    return ReconstructPath(nodes, id);
                }

                var node = nodes[id];
                if (node.explored) continue;
                node.explored = true;
                nodes[id] = node;

                foreach (var edge in graph.GetEdges(id))
                {
                    var targetId = edge.target;
                    var targetNode = nodes[targetId];
                    if (!targetNode.explored)
                    {
                        var cost = entry.cost + edge.cost;
                        targetNode.parent = id;
                        nodes[targetId] = targetNode;
                        frontier.Push(new HeapEntry(targetId, cost));
                    }
                }
            }

            return null;
        }

        private static Path ReconstructPath(Node[] nodes, int id)
        {
            var path = new Path(node.cost);
            for (var current = node.id; current >= 0; current = current.parent)
            {
                path.Add(current.idx);
            }
            return path;
        }
    }
}
