using System.Numerics;
using System.Collections.Generic;

namespace AI_Pathfinder
{
    class Program
    {
        static void Main(string[] args)
        {
            Map map = new Map();
            Vector2 startVector = new Vector2(0, 0);
            List<Vector2> pathToGoal = PathFinder.PathFind(map.MapDict, startVector);
            Dictionary<Vector2, Node> mapDict = map.GetMapWithPath(pathToGoal);
            map.PrintMap(mapDict);
        }
    }
}
