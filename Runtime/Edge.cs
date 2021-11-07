//----------------------------------------
// MIT License
// Copyright(c) 2021 Jonas Boetel
//----------------------------------------
namespace Lumpn.Pathfinding
{
    public struct Edge
    {
        public readonly int targetNodeId;
        public readonly float cost;

        public Edge(int targetNodeId, float cost)
        {
            this.targetNodeId = targetNodeId;
            this.cost = cost;
        }
    }
}
