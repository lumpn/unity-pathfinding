//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class AStar : MonoBehaviour
//{

//    private static object SearchAStar(IGraph graph, int idx1, int idx2, System.Func<IGraph, int, int, float> heuristic)
//    {
//        var searchNodes = graph.GetNodes().Select(p => new SearchNode()).ToArray();

//        var openList = new PriorityQueue<int>();
//        openList.Enqueue(idx1);

//        while (openList.Count > 0)
//        {
//            var idx = openList.Dequeue();
//            var current = searchNodes[idx];
//            if (idx == idx2)
//            {
//                return ReconstructPath(current);
//            }

//            current.closed = true;

//            var edge = graph.GetEdges(idx);
//            foreach (var edge in edges)
//            {
//                var tentativeScore = current.g_score + edge.cost;
//                var dest = edge.index;
//                var destNode = searchNodes[dest];
//                if (tentativeScore < destNode.g_score)
//                {
//                    destNode.prev = current;
//                    destNode.g_score = tentativeScore;
//                    destNode.f_score = destNode.g_score + heuristic(graph, dest, idx2);
//                    if (explored.Add(dest))
//                    {
//                        openList.Enqueue(destNode, destNode.f_score);
//                    }
//                }
//            }
//        }

//        return null;
//    }
//}
