using System.Text;

namespace Eight_puzzle.Core
{
    internal sealed class Cell
    {
        public int Value { get; set; }
        public Position Position { get; set; }
        public bool IsEmpty { get; set; }
        public bool IsRight { get; set; }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append($"Value --> {Value}\n");
            stringBuilder.Append("Position:\n");
            stringBuilder.Append($"X --> {Position.XPos}\n");
            stringBuilder.Append($"Y --> {Position.YPos}\n");
            stringBuilder.Append($"Is position right? {IsRight}\n");
            return stringBuilder.ToString();
        }
    }
}