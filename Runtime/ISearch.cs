//----------------------------------------
// MIT License
// Copyright(c) 2021 Jonas Boetel
//----------------------------------------
namespace Lumpn.Pathfinding
{
    public interface ISearch
    {
        Path Search(IGraph graph, int startNodeId, int destinationNodeId);
    }
}
