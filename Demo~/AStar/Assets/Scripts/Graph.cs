using System;
using System.Collections;
using System.Collections.Generic;

public class Graph : IGraph
{
    public Graph()
    {
    }

    internal int AddNode(Node n1)
    {
        throw new NotImplementedException();
    }

    internal void AddEdge(int idx1, int idx2, float v)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Node> GetNodes()
    {
        throw new NotImplementedException();
    }

    internal Node GetNode(int idx)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Edge> GetEdges(int idx)
    {
        throw new NotImplementedException();
    }
}