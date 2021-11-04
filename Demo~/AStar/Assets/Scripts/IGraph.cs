using System.Collections;
using System.Collections.Generic;

public interface IGraph
{
    IEnumerable<Node> GetNodes();
    IEnumerable<Edge> GetEdges(int idx);
}