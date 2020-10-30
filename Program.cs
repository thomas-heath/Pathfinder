using System.Numerics;
using System.Collections.Generic;
using System;
using System.Diagnostics;

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
            var watch = System.Diagnostics.Stopwatch.StartNew();
            List<Node> path = PathFinder.Dijkstras(graph, sourceVertex, goalVertex);
            watch.Stop();
            Console.WriteLine(watch.ElapsedMilliseconds + "\n");
            Map.PrintMap(map, path, goalVertex);
            Console.WriteLine("\n");
            watch = System.Diagnostics.Stopwatch.StartNew();
            path = PathFinder.BFS(graph, sourceVertex, goalVertex);
            watch.Stop();
            Console.WriteLine(watch.ElapsedMilliseconds + "\n");
            Map.PrintMap(map, path, goalVertex);
        } 
    }
}
