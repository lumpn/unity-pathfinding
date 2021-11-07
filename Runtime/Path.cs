//----------------------------------------
// MIT License
// Copyright(c) 2021 Jonas Boetel
//----------------------------------------
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Lumpn.Pathfinding
{
    public sealed class Path : IEnumerable<int>
    {
        public static readonly Path invalid = new Path(-1f, Enumerable.Empty<int>());

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
