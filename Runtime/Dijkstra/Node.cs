//----------------------------------------
// MIT License
// Copyright(c) 2021 Jonas Boetel
//----------------------------------------

namespace Lumpn.Pathfinding.Dijkstra
{
    internal struct Node
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
}
