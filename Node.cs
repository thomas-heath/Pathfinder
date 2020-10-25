using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Numerics;

namespace Pathfinder
{
    class Node
    {
        public Node(int x, int y, bool isGoal = false)
        {
            MapPos = new Vector2(x, y);
            IsGoal = isGoal;
            if (isGoal) StringRep = 'g';
            else StringRep = 'x';
        }
        public Node(Node node, List<Vector2> childNodes)
        {
            MapPos = node.MapPos;
            IsGoal = node.IsGoal;
            StringRep = node.StringRep;
            ChildNodes = new ReadOnlyCollection<Vector2>(childNodes);
        }
        public Node(Node node, bool onPath)
        {
            MapPos = node.MapPos;
            IsGoal = node.IsGoal;
            if (onPath) StringRep = 'p';
            else StringRep = node.StringRep;
            ChildNodes = new ReadOnlyCollection<Vector2>(ChildNodes);
        }
        public Vector2 MapPos { get; }
        public bool IsGoal { get; }
        public ReadOnlyCollection<Vector2> ChildNodes { get; } = new ReadOnlyCollection<Vector2>(new List<Vector2>());
        public char StringRep { get; }
    }
}