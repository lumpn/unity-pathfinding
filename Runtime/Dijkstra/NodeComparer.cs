//----------------------------------------
// MIT License
// Copyright(c) 2021 Jonas Boetel
//----------------------------------------
using System.Collections.Generic;

namespace Lumpn.Pathfinding.Dijkstra
{
    internal sealed class NodeComparer : IComparer<Node>
    {
        private static readonly IComparer<float> costComparer = Comparer<float>.Default;

        public int Compare(Node a, Node b)
        {
            return costComparer.Compare(a.cost, b.cost);
        }
    }
}
