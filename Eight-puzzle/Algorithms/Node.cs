using System.Collections.Generic;
using System.Linq;
using Eight_puzzle.Enums;

namespace Eight_puzzle.Algorithms
{
    internal class Node
    {
        public int[,] Board { get; }
        public int Depth { get; }
        public Node ParentNode { get; }
        public List<Node> Childs { get; set; }
        public int PathCost { get; internal set; }

        public Node(int[,] board)
        {
            Board = board;
            ParentNode = null;
            Depth = 0;
            PathCost = Depth + Heuristic(board);
            Childs = new List<Node>();
        }
        public Node(Node parent, int[,] board)
        {
            Board = board;
            ParentNode = parent;
            Depth = parent.Depth + 1;
            PathCost = Depth + Heuristic(board);
            Childs = new List<Node>();
        }

        public void Expand()
        {
            (int row, int column) = FindEmpty(Board);
            List<Move> moves = FindWays(row, column);
            foreach (Move move in moves)
            {
                int[,] board = (int[,])Board.Clone();
                switch (move)
                {
                    case Move.Left:
                        Swap(ref board, row, column, row, column + 1);
                        break;
                    case Move.Right:
                        Swap(ref board, row, column, row, column - 1);
                        break;
                    case Move.Top:
                        Swap(ref board, row, column, row + 1, column);
                        break;
                    case Move.Down:
                        Swap(ref board, row, column, row - 1, column);
                        break;

                }

                if (ParentNode is null || IsNotStepBack(board, ParentNode.Board))
                {
                    Childs.Add(new Node(this, board));
                }
            }
        }

        public List<Node> PathToSolution()
        {
            List<Node> nodes = new List<Node>();
            Node node = this;
            while (node.ParentNode is not null)
            {
                nodes.Add(node);
                node = node.ParentNode;
            }

            nodes.Reverse();
            return nodes;
        }

        public static int Heuristic(int[,] board)
        {
            int heuristic = 0;
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    if (board[i, j] != i * 3 + j)
                    {
                        heuristic++;
                    }
                }
            }

            return heuristic;
        }

        public static bool GoalTest(int[,] board)
        {
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    if (board[i, j] != i * 3 + j)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public List<Node> GetChilds() => Childs;

        private static (int, int) FindEmpty(int[,] board)
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (board[i, j] == 0)
                    {
                        return (i, j);
                    }
                }
            }

            return (-1, -1);
        }

        private static List<Move> FindWays(int row, int column)
        {
            List<Move> moves = new();
            if (row <= 1)
            {
                moves.Add(Move.Top);
            }
            if (column >= 1)
            {
                moves.Add(Move.Right);
            }
            if (row >= 1)
            {
                moves.Add(Move.Down);
            }
            if (column <= 1)
            {
                moves.Add(Move.Left);
            }

            return moves;
        }

        private static bool IsNotStepBack(int[,] childBoard, int[,] parentBoard)
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (childBoard[i, j] != parentBoard[i, j])
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private static void Swap(ref int[,] board, int row = 0, int column = 0, int secondRow = 0, int secondColumn = 0)
        {
            int temp = board[row, column];
            board[row, column] = board[secondRow, secondColumn];
            board[secondRow, secondColumn] = temp;
        }
    }
}