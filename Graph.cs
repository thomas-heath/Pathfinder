using System.Collections.Generic;
using System.Numerics;

namespace Pathfinder
{
    class Graph
    {
        public Graph(List<Vector2> map)
        {
            Nodes = GenerateNodes(map);
            Adj = GenerateAdj(Nodes);
        }

        private List<Node> GenerateNodes(List<Vector2> map)
        {
            List<Node> nodes = new List<Node>();
            foreach (Vector2 vertex in map) nodes.Add(new Node(vertex));
            return nodes;
        }

        private Dictionary<Node, List<Node>> GenerateAdj(List<Node> nodes)
        {
            Dictionary<Node, List<Node>> adj = new Dictionary<Node, List<Node>>();
            List<Vector2> neighbourVectDiffs = VectDiffs();
            Vector2 nodeDiff;
            List<Node> list;
            foreach (Node node1 in nodes)
            {
                list = new List<Node>();
                list.Add(node1);
                foreach(Node node2 in nodes)
                {
                    nodeDiff = Vector2.Abs(node1.MapPos - node2.MapPos);
                    if (neighbourVectDiffs.Contains(nodeDiff)) list.Add(node2);
                }
                adj.Add(node1, list);
            }
            return adj;
        }

        private List<Vector2> VectDiffs()
        {
            List<Vector2> neighbourVectDiffs = new List<Vector2>(3);
            neighbourVectDiffs.Add(new Vector2(0, 1));
            neighbourVectDiffs.Add(new Vector2(1, 0));
            neighbourVectDiffs.Add(new Vector2(1, 1));

            return neighbourVectDiffs;
        }

        public List<Node> Nodes { get; }
        public Dictionary<Node, List<Node>> Adj { get; }
    }
}