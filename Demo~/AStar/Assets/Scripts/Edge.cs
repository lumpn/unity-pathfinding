public struct Edge
{
    public readonly int target;
    public readonly float cost;

    public Edge(int target, float cost)
    {
        this.target = target;
        this.cost = cost;
    }
}
