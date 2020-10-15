using System;
using Map;

namespace AI_Pathfinder
{
    class Program
    {
        static void Main(string[] args)
        {
            Map.Map mapGenerator = new Map.Map();

            Console.WriteLine(mapGenerator.GetMapString());

            Console.WriteLine(mapGenerator.GetChildString());
        }
    }
}
