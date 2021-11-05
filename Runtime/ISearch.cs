//----------------------------------------
// MIT License
// Copyright(c) 2021 Jonas Boetel
//----------------------------------------
namespace Lumpn.Graph
{
    public interface ISearch
    {
        Path Search(IGraph graph, int startId, int destinationId);
    }
}
