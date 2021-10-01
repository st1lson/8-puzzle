using System.Text;

namespace Eight_puzzle.Core
{
    internal sealed class Cell
    {
        public int Value { get; set; }
        public bool IsEmpty { get; set; }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append($"Value --> {Value}\n");
            stringBuilder.Append($"Is empty? {IsEmpty}\n");
            return stringBuilder.ToString();
        }
    }
}