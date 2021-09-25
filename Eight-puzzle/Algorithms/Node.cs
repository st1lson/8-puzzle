using Eight_puzzle.Core;
using System.Collections.Generic;

namespace Eight_puzzle.Algorithms
{
    internal sealed class Node
    {
        public Cell[,] Board { get; }
        public int Depth { get; }
        public Node ParentNode { get; }
        public List<Node> Childs { get; }

        public Node(Cell[,] board, int depth = 0, Node parentNode = null)
        {
            Board = board;
            Childs = new List<Node>();
            Depth = depth;
            ParentNode = parentNode;
        }

        public void Expand()
        {
            (int row, int column) = FindEmpty();
            Cell[,] board = Board;
            MoveTop(board, row, column);
            MoveRight(board, row, column);
            MoveDown(board, row, column);
            MoveLeft(board, row, column);
        }

        private void MoveTop(Cell[,] board, int row, int column)
        {
            if (row > 0)
            {
                Swap(ref board,
                    row: row,
                    column: column,
                    secondRow: row - 1,
                    secondColumn: column);
                Childs.Add(new Node(board, Depth + 1, this));
            }
        }

        private void MoveRight(Cell[,] board, int row, int column)
        {
            if (column < 2)
            {
                Swap(ref board,
                    row: row,
                    column: column,
                    secondRow: row,
                    secondColumn: column + 1);
                Childs.Add(new Node(board, Depth + 1, this));
            }
        }

        private void MoveDown(Cell[,] board, int row, int column)
        {
            if (row < 2)
            {
                Swap(ref board,
                    row: row,
                    column: column,
                    secondRow: row + 1,
                    secondColumn: column);
                Childs.Add(new Node(board, Depth + 1, this));
            }
        }

        private void MoveLeft(Cell[,] board, int row, int column)
        {
            if (column > 0)
            {
                Swap(ref board,
                    row: row,
                    column: column,
                    secondRow: row,
                    secondColumn: column - 1);
                Childs.Add(new Node(board, Depth + 1, this));
            }
        }

        private (int, int) FindEmpty()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (Board[i, j].IsEmpty)
                    {
                        return (i, j);
                    }
                }
            }

            return (-1, -1);
        }

        private void Swap(ref Cell[,] board, int row = 0, int column = 0, int secondRow = 0, int secondColumn = 0)
        {
            Cell temp = board[row, column];
            board[row, column] = board[secondRow, secondColumn];
            board[secondRow, secondColumn] = temp;
        }
    }
}