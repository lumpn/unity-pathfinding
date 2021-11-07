//----------------------------------------
// MIT License
// Copyright(c) 2021 Jonas Boetel
//----------------------------------------
namespace Lumpn.Pathfinding.AStar
{
    internal struct Node
    {
        public readonly int id;
        public readonly int parentId;
        public readonly float cost;
        public readonly float estimate;

        public Node(int nodeId, int parentId, float cost, float heuristic)
        {
            this.id = nodeId;
            this.parentId = parentId;
            this.cost = cost;
            this.estimate = cost + heuristic;
        }
    }
}
