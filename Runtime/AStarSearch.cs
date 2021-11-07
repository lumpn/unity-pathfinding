//----------------------------------------
// MIT License
// Copyright(c) 2021 Jonas Boetel
//----------------------------------------
using System.Collections.Generic;
using System;

namespace Lumpn.Pathfinding
{
    public sealed class AStarSearch
    {
        private static readonly StepComparer comparer = new StepComparer();

        public delegate float Heuristic(IGraph graph, int startId, int destinationId);

        private readonly Heap<Step> queue;
        private int[] parents;

        public AStarSearch(int initialCapacity)
        {
            this.queue = new Heap<Step>(comparer, initialCapacity);
        }

        public Path FindPath(IGraph graph, int startId, int destinationId, Heuristic heuristic)
        {
            var nodeCount = graph.nodeCount;
            Array.Resize(ref parents, nodeCount);
            Array.Clear(parents, 0, nodeCount);

            queue.Push(new Step(startId, -1, 0f, heuristic(graph, startId, destinationId)));

            while (queue.Count > 0)
            {
                var step = queue.Pop();
                var nodeId = step.nodeId;

                if (parents[nodeId] > 0) continue;
                parents[nodeId] = step.parentId + 2;

                var nodeCost = step.cost;

                if (nodeId == destinationId)
                {
                    queue.Clear();
                    return ReconstructPath(graph, nodeId, nodeCost, parents);
                }

                foreach (var edge in graph.GetEdges(nodeId))
                {
                    var targetId = edge.targetNodeId;
                    if (parents[targetId] <= 0)
                    {
                        var cost = nodeCost + edge.cost;
                        queue.Push(new Step(targetId, nodeId, cost, heuristic(graph, targetId, destinationId)));
                    }
                }
            }

            return Path.invalid;
        }

        private static Path ReconstructPath(IGraph graph, int lastNodeId, float cost, int[] parents)
        {
            var nodes = new List<INode>();
            for (var nodeId = lastNodeId; nodeId >= 0; nodeId = parents[nodeId] - 2)
            {
                var node = graph.GetNode(nodeId);
                nodes.Add(node);
            }
            nodes.Reverse();

            return new Path(cost, nodes);
        }
    }
}
