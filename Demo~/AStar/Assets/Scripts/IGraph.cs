using System.Collections.Generic;

public interface IGraph
{
    int nodeCount { get; }

    IEnumerable<Edge> GetEdges(int idx);
}
