using System.Numerics;
using System.Collections.Generic;
using System;

namespace Pathfinder
{
    static class Map
    {
        public static List<Vector2> GenerateMap(int x, int y)
        {
            List<Vector2> map = new List<Vector2>();

            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    map.Add(new Vector2(i, j));
                }
            }

            return map;
        }

        public static void PrintMap(List<Vector2> map, List<Node> path, Vector2 goal)
        {
            string mapString = "";
            float prevX = 0;
            string vertexString = "x";

            foreach (Vector2 vertex in map)
            {
                if (vertexOnPath(vertex, path)) vertexString = "p";
                if (vertex == goal) vertexString = "g";
                if (vertex.X == prevX)
                {
                    mapString = String.Concat(mapString, vertexString);
                }
                else
                {
                    mapString = String.Concat(mapString, "\n");
                    mapString = String.Concat(mapString, vertexString);
                }
                prevX = vertex.X;
                vertexString = "x";
            }

            Console.WriteLine(mapString);

            bool vertexOnPath(Vector2 vertex, List<Node> path)
            {
                foreach(Node node in path)
                {
                    if (node.MapPos == vertex)
                    {
                        return true;
                    }
                }
                return false;
            }
        }
    }
}