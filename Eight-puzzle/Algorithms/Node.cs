using System;
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
        public int PathCost { get; internal set; }

        public Node(Cell[,] board, int depth = 0, Node parentNode = null, int pathCost = 0)
        {
            Board = board;
            Childs = new List<Node>();
            Depth = depth;
            ParentNode = parentNode;
            PathCost = pathCost;
        }

        public void Expand()
        {
            SetEmpty();
            (int row, int column) = FindEmpty();
            MoveTop(row, column);
            MoveRight(row, column);
            MoveDown(row, column);
            MoveLeft(row, column);
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

        public int Heuristic()
        {
            int heuristic = 0;
            for (int i = 0; i < Board.GetLength(0); i++)
            {
                for (int j = 0; j < Board.GetLength(1); j++)
                {
                    if (Board[i, j].Value != 0 && Board[i, j].Value != i * j + j + 1)
                    {
                        heuristic++;
                    }
                }
            }

            return heuristic;
        }

        public bool GoalTest()
        {
            for (int i = 0; i < Board.GetLength(0); i++)
            {
                for (int j = 0; j < Board.GetLength(1); j++)
                {
                    if (Board[i, j].Value == i * 3 + j + 1)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public List<Node> GetChilds() => Childs;

        private void MoveTop(int row, int column)
        {
            if (row > 0)
            {
                Cell[,] board = new Cell[3, 3];
                Array.Copy(Board, board, Board.Length);
                Swap(ref board,
                    row: row,
                    column: column,
                    secondRow: row - 1,
                    secondColumn: column);
                Childs.Add(new Node(board, Depth + 1, this));
            }
        }

        private void MoveRight(int row, int column)
        {
            if (column < 2)
            {
                Cell[,] board = new Cell[3, 3];
                Array.Copy(Board, board, Board.Length);
                Swap(ref board,
                    row: row,
                    column: column,
                    secondRow: row,
                    secondColumn: column + 1);
                Childs.Add(new Node(board, Depth + 1, this));
            }
        }

        private void MoveDown(int row, int column)
        {
            if (row < 2)
            {
                Cell[,] board = new Cell[3, 3];
                Array.Copy(Board, board, Board.Length);
                Swap(ref board,
                    row: row,
                    column: column,
                    secondRow: row + 1,
                    secondColumn: column);
                Childs.Add(new Node(board, Depth + 1, this));
            }
        }

        private void MoveLeft(int row, int column)
        {
            if (column > 0)
            {
                Cell[,] board = new Cell[3, 3];
                Array.Copy(Board, board, Board.Length);
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
            SetEmpty();
        }

        private void SetEmpty()
        {
            foreach (var cell in Board)
            {
                if (cell.Value == 0)
                {
                    cell.IsEmpty = true;
                }
                else
                {
                    cell.IsEmpty = false;
                }
            }
        }
    }
}