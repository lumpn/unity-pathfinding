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

        private readonly Heap<Step> queue;

        /// <summary>
        /// Index of the previous node on the shortest path to each node.
        /// We reserve two values, 0 for unexplored, 1 for no parent.
        /// All node ids are offset by 2 to account for the reserved values.
        /// </summary>
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

            var destination = graph.GetNode(destinationId);
            queue.Push(CreateStep(graph, startId, -1, 0f, heuristic, destination));

            while (queue.Count > 0)
            {
                var step = queue.Pop();
                var nodeId = step.nodeId;

                if (parents[nodeId] > 0) continue;
                parents[nodeId] = step.parentId + 2;

                if (nodeId == destinationId)
                {
                    queue.Clear();
                    return ReconstructPath(graph, nodeId, step.cost, parents);
                }

                var nodeCost = step.cost;
                foreach (var edge in graph.GetEdges(nodeId))
                {
                    var targetId = edge.targetNodeId;
                    if (parents[targetId] > 0) continue;

                    var cost = nodeCost + edge.cost;
                    queue.Push(CreateStep(graph, targetId, nodeId, cost, heuristic, destination));
                }
            }

            return Path.invalid;
        }

        private static Step CreateStep(IGraph graph, int nodeId, int parentId, float cost, Heuristic heuristic, INode destination)
        {
            var node = graph.GetNode(nodeId);
            var heuristicValue = heuristic(graph, node, destination);
            return new Step(nodeId, parentId, cost, heuristicValue);
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
