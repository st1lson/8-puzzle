using Eight_puzzle.Enums;

namespace Eight_puzzle.Algorithms
{
    internal class Result
    {
        public Node Node { get; }
        public State State { get; }
        public int Depth { get; }
        public int PathCost { get; }
        
        public Result(Node node, State state)
        {
            Node = node;
            Depth = node.Depth;
            PathCost = node.PathCost;
            State = state;
        }
    }
}