using System.Collections.Generic;
using System.Numerics;

namespace Pathfinder
{
    class Node
    {
        public Node(Vector2 mapPos) => MapPos = mapPos;
        public void SetColour(string colour)
        {
            string[] colourArray = {"white", "w", "grey", "gray", "g", "black", "b"};
            List<string> colours = new List<string>(colourArray);

            if (colours.Contains(colour.ToLower())) 
            {
                if(colour.ToLower() == "white" || colour.ToLower() == "w") Colour = "w";
                if(colour.ToLower() == "grey" || colour.ToLower() == "gray" || colour.ToLower() == "g") Colour = "g";
                if(colour.ToLower() == "black" || colour.ToLower() == "b") Colour = "b";
            }
            else
            {
                throw new System.ArgumentException("Parameter must be a string representing either White, Grey or Black", "colour");
            }
        }

        public void SetPredecessor(List<Node> nodes, Node node)
        {
            if (!nodes.Contains(node) && node != null)
            {
                throw new System.ArgumentException("Predecessor node must be a node that exists in the graph", "node");
            }
            if (this == node)
            {
                throw new System.ArgumentException("Predecessor node must be distinct from the node its being assigned to", "node");
            }
            Predecessor = node;
        }

        public Vector2 MapPos { get; }
        public string Colour { get; private set; }
        public Node Predecessor { get; private set; }
        public float Distance { get; set; }
    }
}