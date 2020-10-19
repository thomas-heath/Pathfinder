using System.Collections.Generic;
using System.Numerics;
using System;

namespace AI_Pathfinder
{
    static class PathFinder
    {
        public static List<Vector2> PathFind(Dictionary<Vector2, Node> map, Vector2 startVector)
        {
            //Map mapObject = new Map();
            LinkedList<KeyValuePair<List<Vector2>, Node>> frontier = new LinkedList<KeyValuePair<List<Vector2>, Node>>();
            List<Vector2> path = new List<Vector2>();
            frontier.AddLast(new KeyValuePair<List<Vector2>, Node>(path, map[startVector]));
            Node expandedNode = map[startVector];
            path.Add(expandedNode.MapPos);
            int expandedNodes = 0;
            //Dictionary<Vector2, Node> mapDict;

            while (!expandedNode.IsGoal)
            {
                //mapDict = mapObject.GetMapWithPath(path);
                //mapObject.PrintMap(mapDict);
                expandedNodes++;
                foreach (Vector2 childVector in expandedNode.ChildNodes)
                {
                    if (!path.Contains(childVector)) frontier.AddLast(new KeyValuePair<List<Vector2>, Node>(path, map[childVector]));
                }
                frontier.RemoveFirst();
                expandedNode = frontier.First.Value.Value;
                path = new List<Vector2>(frontier.First.Value.Key);
                path.Add(expandedNode.MapPos);
            }
            Console.WriteLine(expandedNodes + " nodes have been expanded.");
            return path;
        }
    }
}
