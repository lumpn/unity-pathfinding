//----------------------------------------
// MIT License
// Copyright(c) 2021 Jonas Boetel
//----------------------------------------
using System.Collections.Generic;

namespace Lumpn.Pathfinding
{
    public interface IGraph
    {
        int nodeCount { get; }

        INode GetNode(int nodeId);

        IEnumerable<Edge> GetEdges(int nodeId);
    }
}
