//----------------------------------------
// MIT License
// Copyright(c) 2021 Jonas Boetel
//----------------------------------------
using System.Collections;
using System.Collections.Generic;

namespace Lumpn.Graph
{
    public sealed class Path : IEnumerable<int>
    {
        private readonly List<int> nodes = new List<int>();
        public readonly float cost;

        public int length => nodes.Count;

        public Path(float cost, IEnumerable<int> nodes)
        {
            this.cost = cost;
            this.nodes.AddRange(nodes);
        }

        public IEnumerator<int> GetEnumerator()
        {
            return nodes.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return nodes.GetEnumerator();
        }
    }
}
