using System;
using System.Collections;
using System.Collections.Generic;

public class Path : IEnumerable<int>
{
    public int length;
    public readonly float cost;

    public Path(float cost)
    {
        this.cost = cost;
    }

    public IEnumerator<int> GetEnumerator()
    {
        throw new System.NotImplementedException();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        throw new System.NotImplementedException();
    }

    internal void Add(int idx)
    {
        throw new NotImplementedException();
    }
}