using System.Numerics;
using System.Collections.Generic;
using System;

namespace Pathfinder
{
    class Map
    {
        public Dictionary<Vector2, Node> MapDict { get; }
        private readonly Vector2 mapDimensions = new Vector2(10, 10);
        public Map()
        {
            List<Node> map = GenerateMap();
            map = CalculateChildren(map);
            MapDict = BuildDictionary(map);
            PrintMap(MapDict);
        }

        private List<Node> GenerateMap()
        {
            List<Node> map = new List<Node>();

            for (int x = 0; x < mapDimensions.X; x++)
            {
                for (int y = 0; y < mapDimensions.Y; y++)
                {
                    if (x == mapDimensions.X - 1 && y == 7)
                    {
                        map.Add(new Node(x, y, true));
                    }
                    else
                    {
                        map.Add(new Node(x, y));
                    }
                }
            }
            return map;
        }

        private List<Node> CalculateChildren(List<Node> nodes)
        {
            List<Vector2> neighbourVectDiffs = VectDiffs();
            List<Vector2> childNodes;
            Vector2 nodeDiff;
            Node node1;
            Node node2;

            for (int i = 0; i < nodes.Count; i++)
            {
                node1 = nodes[i];
                childNodes = new List<Vector2>(8);

                for (int j = 0; j < nodes.Count; j++)
                {
                    node2 = nodes[j];
                    nodeDiff = Vector2.Abs(node1.MapPos - node2.MapPos);
                    if (neighbourVectDiffs.Contains(nodeDiff)) childNodes.Add(node2.MapPos);
                }

                nodes[i] = new Node(node1, childNodes);
            }

            return nodes;
        }

        private List<Vector2> VectDiffs()
        {
            List<Vector2> neighbourVectDiffs = new List<Vector2>(3);
            neighbourVectDiffs.Add(new Vector2(0, 1));
            neighbourVectDiffs.Add(new Vector2(1, 0));
            neighbourVectDiffs.Add(new Vector2(1, 1));

            return neighbourVectDiffs;
        }

        private Dictionary<Vector2, Node> BuildDictionary(List<Node> map)
        {
            Dictionary<Vector2, Node> dictionary = new Dictionary<Vector2, Node>();

            foreach (Node node in map)
            {
                dictionary.Add(node.MapPos, node);
            }

            return dictionary;
        }

        public Dictionary<Vector2, Node> GetMapWithPath(List<Vector2> path)
        {
            Dictionary<Vector2, Node> map = new Dictionary<Vector2, Node>(MapDict);
            foreach (Vector2 nodePos in path)
            {
                map[nodePos] = new Node(MapDict[nodePos], true);
            }
            return map;
        }

        public void PrintMap(Dictionary<Vector2, Node> map)
        {
            string mapString = "";
            Node node;
            for (int i = 0; i < mapDimensions.X; i++)
            {
                for (int j = 0; j < mapDimensions.Y; j++)
                {
                    node = map[new Vector2(i, j)];
                    mapString = string.Concat(mapString, node.StringRep);
                }
                mapString = string.Concat(mapString, "\n");
            }
            Console.WriteLine(mapString);
        }
    }
}