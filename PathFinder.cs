using System.Collections.Generic;
using System.Numerics;
using FibonacciHeap;

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

            return BuildPath(graph, source, goal);
        }

        public static List<Node> Dijkstras(Graph graph, Vector2 sourceVect, Vector2 goalVect)
        {
            Node source = null;
            Node goal = null;
            Node expandedNode;
            List<Node> finalizedSet = new List<Node>();
            FibonacciHeap<Node, float> prioQueue = new FibonacciHeap<Node, float>(0);

            foreach (Node node in graph.Nodes)
            {
                node.Distance = 1f / 0f;
                node.SetPredecessor(graph.Nodes, null);
                if (node.MapPos == sourceVect)
                {
                    source = node;
                }
                else
                {
                    prioQueue.Insert(new FibonacciHeapNode<Node, float>(node, node.Distance));
                }
                if (node.MapPos == goalVect) goal = node;
            }
            source.Distance = 0;
            prioQueue.Insert(new FibonacciHeapNode<Node, float>(source, source.Distance));

            while (prioQueue.Size() != 0)
            {
                expandedNode = prioQueue.RemoveMin().Data;
                finalizedSet.Add(expandedNode);
                foreach (Node node in graph.Adj[expandedNode])
                {
                    if (node.Distance > expandedNode.Distance + 1)
                    {
                        node.Distance = expandedNode.Distance + 1;
                        node.SetPredecessor(graph.Nodes, expandedNode);
                    }
                }
            }

            return BuildPath(graph, source, goal);
        }

        private static void InitializeSingleSource(Graph graph, Node source)
        {
            foreach (Node node in graph.Nodes)
            {
                node.Distance = 1f / 0f;
                node.SetPredecessor(graph.Nodes, null);
            }
            source.Distance = 0;
        }

        private static List<Node> BuildPath(Graph graph, Node source, Node goal)
        {
            List<Node> path = new List<Node>();
            
            if (goal == source)
            {
                path.Add(source);
                return path;
            } 
            else if (goal.Predecessor == null)
            {
                throw new System.ArgumentException("No path from source to goal", "goal");
            }
            else
            {
                path = BuildPath(graph, source, goal.Predecessor);
                path.Add(goal);
                return path;
            }
        }
    }
}
