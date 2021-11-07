//----------------------------------------
// MIT License
// Copyright(c) 2021 Jonas Boetel
//----------------------------------------
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Lumpn.Pathfinding
{
    public sealed class Path : IEnumerable<INode>
    {
        public static readonly Path invalid = new Path(-1f, Enumerable.Empty<INode>());

        private readonly List<INode> nodes = new List<INode>();
        public readonly float cost;

        public int length => nodes.Count;

        public Path(float cost, IEnumerable<INode> nodes)
        {
            this.cost = cost;
            this.nodes.AddRange(nodes);
        }

        public IEnumerator<INode> GetEnumerator()
        {
            return nodes.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return nodes.GetEnumerator();
        }
    }
}
