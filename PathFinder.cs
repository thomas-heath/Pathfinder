using System.Collections.Generic;
using System.Numerics;
using System;

namespace Pathfinder
{
    static class PathFinder
    {
        public static List<Vector2> PathChecking(Dictionary<Vector2, Node> map, Vector2 startVector)
        {
            LinkedList<KeyValuePair<List<Vector2>, Node>> frontier = new LinkedList<KeyValuePair<List<Vector2>, Node>>();
            List<Vector2> path = new List<Vector2>();
            frontier.AddLast(new KeyValuePair<List<Vector2>, Node>(path, map[startVector]));
            Node expandedNode;
            int nodeCount = 0;
            ExpandNode(map[startVector]);

            while (!ChildIsGoal(expandedNode, map))
            {
                foreach (Vector2 childVector in expandedNode.ChildNodes)
                {
                    if (!path.Contains(childVector)) frontier.AddLast(new KeyValuePair<List<Vector2>, Node>(path, map[childVector]));
                }
                frontier.RemoveFirst();
                path = new List<Vector2>(frontier.First.Value.Key);
                ExpandNode(frontier.First.Value.Value);
            }
            Console.WriteLine(nodeCount + " nodes have been expanded.");
            return path;

            void ExpandNode(Node node)
            {
                expandedNode = node;
                path.Add(expandedNode.MapPos);
                nodeCount++;
            }
        }

        public static List<Vector2> CycleChecking(Dictionary<Vector2, Node> map, Vector2 startVector)
        {
            LinkedList<KeyValuePair<List<Vector2>, Node>> frontier = new LinkedList<KeyValuePair<List<Vector2>, Node>>();
            List<Vector2> path = new List<Vector2>();
            List<Node> expandedNodes = new List<Node>();
            frontier.AddLast(new KeyValuePair<List<Vector2>, Node>(path, map[startVector]));
            Node expandedNode;
            int nodeCount = 0;
            ExpandNode(map[startVector]);

            while (!ChildIsGoal(expandedNode, map))
            {
                foreach (Vector2 childVector in expandedNode.ChildNodes)
                {
                    if (!expandedNodes.Contains(map[childVector])) frontier.AddLast(new KeyValuePair<List<Vector2>, Node>(path, map[childVector]));
                }
                frontier.RemoveFirst();
                path = new List<Vector2>(frontier.First.Value.Key);
                ExpandNode(frontier.First.Value.Value);
            }
            Console.WriteLine(nodeCount + " nodes have been expanded.");
            return path;

            void ExpandNode(Node node)
            {
                expandedNode = node;
                expandedNodes.Add(expandedNode);
                path.Add(expandedNode.MapPos);
                nodeCount++;
            }
        }

        public static List<Vector2> UnifCostCycleChecking(Dictionary<Vector2, Node> map, Vector2 startVector)
        {
            LinkedList<KeyValuePair<List<Vector2>, Node>> frontier = new LinkedList<KeyValuePair<List<Vector2>, Node>>();
            List<Vector2> path = new List<Vector2>();
            List<Node> expandedNodes = new List<Node>();
            frontier.AddLast(new KeyValuePair<List<Vector2>, Node>(path, map[startVector]));
            Node expandedNode;
            int nodeCount = 0;
            ExpandNode(map[startVector]);

            while (!ChildIsGoal(expandedNode, map))
            {
                foreach (Vector2 childVector in expandedNode.ChildNodes)
                {
                    if (!expandedNodes.Contains(map[childVector])) frontier.AddLast(new KeyValuePair<List<Vector2>, Node>(path, map[childVector]));
                }
                frontier.RemoveFirst();
                SortFrontier();
                path = new List<Vector2>(frontier.First.Value.Key);
                ExpandNode(frontier.First.Value.Value);
            }
            Console.WriteLine(nodeCount + " nodes have been expanded.");
            return path;

            void ExpandNode(Node node)
            {
                expandedNode = node;
                expandedNodes.Add(expandedNode);
                path.Add(expandedNode.MapPos);
                nodeCount++;
            }

            void SortFrontier()
            {
                int k = frontier.First.Value.Key.Count;
                foreach (var node in frontier)
                {
                    if (node.Key.Count > k) k = node.Key.Count;
                }

                int[] count = new int[k + 1];
                Populate(count);
                foreach (var node in frontier)
                {
                    count[node.Key.Count] += 1;
                }

                int total = 0;
                int temp;
                for (int i = 0; i <= k; i++)
                {
                    temp = total;
                    total = count[i] + total;
                    count[i] = temp;
                }

                List<KeyValuePair<List<Vector2>, Node>> sortedNodes = new List<KeyValuePair<List<Vector2>, Node>>(frontier);
                foreach (var node in frontier)
                {
                    sortedNodes[count[node.Key.Count]] = node;
                    count[node.Key.Count] += 1;
                }

                frontier = new LinkedList<KeyValuePair<List<Vector2>, Node>>(sortedNodes);
            }

            void Populate(int[] array)
            {
                for (int i = 0; i < array.Length; i++) array[i] = 0;
            }
        }

        private static bool ChildIsGoal(Node node, Dictionary<Vector2, Node> map)
        {
            foreach (Vector2 childVector in node.ChildNodes)
            {
                if (map[childVector].IsGoal) return true;
            }
            return false;
        }
    }
}
