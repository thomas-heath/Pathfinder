using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Numerics;

namespace Map
{
    class Node
    {
        private readonly Vector2 _mapPos;
        private readonly bool _isGoal;
        private readonly ReadOnlyCollection<Node> _childNodes;

        public Node(int x, int y) => _mapPos = new Vector2(x, y);
        public Node(int x, int y, bool isGoal)
        {
            _mapPos = new Vector2(x, y);
            _isGoal = isGoal;
        }
        public Node(Node node, List<Node> childNodes)
        {
            _mapPos = node.MapPos();
            _isGoal = node.IsGoal();
            _childNodes = new ReadOnlyCollection<Node>(childNodes);
        }

        public Vector2 MapPos() => _mapPos;

        public IEnumerable<Node> GetChildren() => _childNodes;

        public bool IsGoal() => _isGoal;

        public string StringOfChildren()
        {
            string childString = "";
            foreach (var node1 in _childNodes)
            {
                childString = string.Concat(childString, "x");
            }
            return childString;
        }
    }
}