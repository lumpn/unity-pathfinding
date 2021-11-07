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

        public int Compare(Node x, Node y)
        {
            return costComparer.Compare(x.cost, y.cost);
        }
    }
}
