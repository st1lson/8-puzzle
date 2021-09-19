namespace Eight_puzzle.Core
{
    internal sealed class Position
    {
        public int XPos { get; set; }
        public int YPos { get; set; }

        public override string ToString()
        {
            return $"({XPos}, {YPos})";
        }
    }
}