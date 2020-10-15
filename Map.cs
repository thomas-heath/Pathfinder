using System.Windows;
using System.Numerics;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Map
{
    class Map
    {
        private readonly List<Vector2> _neighbourVectDiffs = new List<Vector2>(3);
        private readonly ReadOnlyCollection<Node> _map;
        private string _mapString = "";
        private string _childString = "";
        public Map()
        {
            List<Node> map = GenerateMap();
            map = CalculateChildren(map);
            _map = new ReadOnlyCollection<Node>(map);
            PrintNeighbours();
        }

        public IEnumerable<Node> GetMap() => _map;
        public string GetMapString() => _mapString;
        public string GetChildString() => _childString;

        private List<Node> GenerateMap()
        {
            List<Node> map = new List<Node>();

            for (int x = 0; x < 10; x++)
            {
                for (int y = 0; y < 10; y++)
                {
                    map.Add(new Node(x, y));
                    _mapString = string.Concat(_mapString, "x");
                }
                _mapString = string.Concat(_mapString, "\n");
            }

            return map;
        }

        private List<Node> CalculateChildren(List<Node> nodes)
        {
            SetVectDiffs();
            List<Node> childNodes;
            Vector2 nodeDiff;
            Node node1;
            Node node2;

            for (int i = 0; i < nodes.Count; i++)
            {
                node1 = nodes[i];
                childNodes = new List<Node>(8);

                for (int j = 0; j < nodes.Count; j++)
                {
                    node2 = nodes[j];
                    nodeDiff = Vector2.Abs(node1.MapPos() - node2.MapPos());
                    if (_neighbourVectDiffs.Contains(nodeDiff)) childNodes.Add(node2);
                }

                nodes[i] = new Node(node1, childNodes);
            }

            return nodes;
        }

        private void SetVectDiffs()
        {
            _neighbourVectDiffs.Add(new Vector2(0, 1));
            _neighbourVectDiffs.Add(new Vector2(1, 0));
            _neighbourVectDiffs.Add(new Vector2(1, 1));
        }

        private void PrintNeighbours()
        {
            foreach (var node1 in _map)
            {
                _childString = string.Concat(_childString, node1.StringOfChildren());
                _childString = string.Concat(_childString, "\n");
            }
        }
    }
}