//----------------------------------------
// MIT License
// Copyright(c) 2021 Jonas Boetel
//----------------------------------------
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Lumpn.Pathfinding.Demo
{
    public class Demo : MonoBehaviour
    {
        [SerializeField] private Vector2Int gridSize;

        private readonly List<Vector3> trace = new List<Vector3>();

        void OnDrawGizmos()
        {
            if (trace.Count > 0)
            {
                var prev = trace[0];
                foreach (var pos in trace)
                {
                    Gizmos.DrawLine(prev, pos);
                    prev = pos;
                }
            }
        }

        void Start()
        {
            Run();
        }

        [ContextMenu(nameof(Run))]
        public void Run()
        {
            var graph = new Graph();

            // create nodes
            var grid = new int[gridSize.x, gridSize.y];
            for (int x = 0; x < gridSize.x; x++)
            {
                for (int y = 0; y < gridSize.y; y++)
                {
                    var node = new Node($"{x}, {y}", new Vector3(x, y, 0));
                    var id = graph.AddNode(node);
                    grid[x, y] = id;
                }
            }

            // connect grid
            for (int x = 1; x < gridSize.x; x++)
            {
                graph.AddEdge(grid[x - 1, 0], grid[x, 0], 1 + Random.value);
            }
            for (int y = 1; y < gridSize.y; y++)
            {
                graph.AddEdge(grid[0, y - 1], grid[0, y], 1 + Random.value);
            }
            for (int x = 1; x < gridSize.x; x++)
            {
                for (int y = 1; y < gridSize.y; y++)
                {
                    graph.AddEdge(grid[x - 1, y], grid[x, y], 1 + Random.value);
                    graph.AddEdge(grid[x, y - 1], grid[x, y], 1 + Random.value);
                }
            }

            var startId = grid[0, 0];
            var destinationId = grid[gridSize.x - 1, gridSize.y - 1];

            // run search
            var search = new AStarSearch(graph.nodeCount);
            var path1 = Run(search, graph, startId, destinationId, NoHeuristic);
            var path2 = Run(search, graph, startId, destinationId, DistanceHeuristic);
            var path3 = Run(search, graph, startId, destinationId, GreedyHeuristic);
            var path = path3;

            // print path
            trace.Clear();
            trace.AddRange(path.Cast<Node>().Select(p => p.position));
            Debug.LogFormat("Path length {0}, cost {1}", path.length, path.cost);
        }

        private static float NoHeuristic(IGraph graph, INode a, INode b)
        {
            return 0f;
        }

        private static float DistanceHeuristic(IGraph graph, INode a, INode b)
        {
            var u = (Node)a;
            var v = (Node)b;
            return Vector3.Distance(u.position, v.position);
        }

        private static float GreedyHeuristic(IGraph graph, INode a, INode b)
        {
            return 2 * DistanceHeuristic(graph, a, b);
        }

        private static Path Run(AStarSearch search, IGraph graph, int startId, int destinationId, Heuristic heuristic)
        {
            var watch = new System.Diagnostics.Stopwatch();
            watch.Start();

            var path = search.FindPath(graph, startId, destinationId, heuristic);

            watch.Stop();
            Debug.LogFormat("Search took {0} ms", watch.ElapsedMilliseconds);

            return path;
        }
    }
}
