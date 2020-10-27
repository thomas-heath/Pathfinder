using System.Numerics;
using System.Collections.Generic;

namespace Pathfinder
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Vector2> map = Map.GenerateMap(100, 100);
            Vector2 goalVertex = new Vector2(99, 99);
            Vector2 sourceVertex = new Vector2(0, 0);
            Graph graph = new Graph(map);
            List<Node> path = PathFinder.BFS(graph, sourceVertex, goalVertex);
            Map.PrintMap(map, path, goalVertex);
        } 
    }
}
