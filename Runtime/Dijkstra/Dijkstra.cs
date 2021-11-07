//----------------------------------------
// MIT License
// Copyright(c) 2021 Jonas Boetel
//----------------------------------------
using System.Collections.Generic;

namespace Lumpn.Pathfinding
{
    public sealed class Dijkstra : ISearch
    {
        private struct Node
        {
            public readonly int id;
            public readonly int parentId;
            public readonly float cost;

            public Node(int nodeId, int parentId, float cost)
            {
                this.id = nodeId;
                this.parentId = parentId;
                this.cost = cost;
            }
        }

        private sealed class NodeComparer : IComparer<Node>
        {
            private static readonly IComparer<float> costComparer = Comparer<float>.Default;

            public int Compare(Node x, Node y)
            {
                return costComparer.Compare(x.cost, y.cost);
            }
        }

        private static readonly NodeComparer comparer = new NodeComparer();

        public Path Search(IGraph graph, int startId, int destinationId)
        {
            var nodeCount = graph.nodeCount;
            var explored = new bool[nodeCount];
            var parents = new int[nodeCount];

            var queue = new Heap<Node>(comparer, graph.nodeCount);

            queue.Push(new Node(startId, -1, 0f));

            while (queue.Count > 0)
            {
                var entry = queue.Pop();
                var nodeId = entry.id;

                if (explored[nodeId]) continue;
                explored[nodeId] = true;
                parents[nodeId] = entry.parentId;

                if (nodeId == destinationId)
                {
                    var path = ReconstructPath(nodeId, entry.cost, parents);
                    return path;
                }

                foreach (var edge in graph.GetEdges(nodeId))
                {
                    var targetId = edge.targetNodeId;
                    if (!explored[targetId])
                    {
                        var cost = entry.cost + edge.cost;
                        queue.Push(new Node(targetId, nodeId, cost));
                    }
                }
            }

            return Path.invalid;
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
