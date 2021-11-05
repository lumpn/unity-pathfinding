//----------------------------------------
// MIT License
// Copyright(c) 2021 Jonas Boetel
//----------------------------------------
using System.Collections.Generic;

namespace Lumpn.Graph
{
    public interface IGraph
    {
        int nodeCount { get; }

        IEnumerable<Edge> GetEdges(int nodeId);
    }
}
