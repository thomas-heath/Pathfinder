using System.Collections.Generic;
using System.Numerics;
using System;

namespace Pathfinder
{
    static class PathFinder
    {
        public static List<Node> BFS(Graph graph, Vector2 sourceVect, Vector2 goalVect)
        {
            Node source = null;
            Node goal = null;
            foreach (Node node in graph.Nodes)
            {
                if (node.MapPos == sourceVect) source = node;
                if (node.MapPos == goalVect) goal = node;
                node.SetColour("w");
                node.Distance = 1f / 0f;
                node.SetPredecessor(graph.Nodes, null);
            }
            if (source == null) throw new System.ArgumentException("Source Vector must equal the map position of a node in the graph", "sourceVect");

            source.SetColour("g");
            source.Distance = 0f;
            source.SetPredecessor(graph.Nodes, null);

            Node expandedNode;

            LinkedList<Node> frontier = new LinkedList<Node>();
            frontier.AddLast(source);

            while (frontier.Count != 0)
            {
                expandedNode = frontier.First.Value;
                frontier.RemoveFirst();
                
                foreach (Node node in graph.Adj[expandedNode])
                {
                    if (node.Colour == "w")
                    {
                        node.SetColour("g");
                        node.Distance = expandedNode.Distance + 1;
                        node.SetPredecessor(graph.Nodes, expandedNode);
                        frontier.AddLast(node);
                    }
                }
                expandedNode.SetColour("b");
            }

            List<Node> path = new List<Node>();
            BuildPath(graph, source, goal);
            return path;

            void BuildPath(Graph graph, Node source, Node goal)
            {
                if (goal == source)
                {
                    path.Add(source);
                } 
                else if (goal.Predecessor == null)
                {
                    throw new System.ArgumentException("No path from source to goal", "goal");
                }
                else
                {
                    BuildPath(graph, source, goal.Predecessor);
                    path.Add(goal);
                }
            }
        }
    }
}
