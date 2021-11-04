using System.Collections;
using System.Collections.Generic;

internal interface IGraph
{
    IEnumerable<object> GetNodes();
    IEnumerable<object> GetEdges(object idx);
}