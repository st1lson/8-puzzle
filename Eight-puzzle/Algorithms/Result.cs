using Eight_puzzle.Enums;
using System;

namespace Eight_puzzle.Algorithms
{
    internal class Result
    {
        public Node Node { get; set; }
        public State State { get; set; }
        public int Depth { get; set; }
        public int PathCost { get; set; }

        public Result() { }
        public Result(Node node, State state)
        {
            Node = node;
            Depth = node.Depth;
            PathCost = node.PathCost;
            State = state;
        }
    }
}