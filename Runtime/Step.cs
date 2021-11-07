//----------------------------------------
// MIT License
// Copyright(c) 2021 Jonas Boetel
//----------------------------------------
namespace Lumpn.Pathfinding
{
    internal struct Step
    {
        public readonly int nodeId;
        public readonly int parentId;
        public readonly float cost;
        public readonly float estimate;

        public Step(int nodeId, int parentId, float cost, float heuristic)
        {
            this.nodeId = nodeId;
            this.parentId = parentId;
            this.cost = cost;
            this.estimate = cost + heuristic;
        }
    }
}
