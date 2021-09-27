using System;
using Eight_puzzle.ClientSide;
using Eight_puzzle.Core;
using Eight_puzzle.Enums;

namespace Eight_puzzle.Algorithms
{
    internal sealed class IDS
    {
        private readonly Puzzle _puzzle;

        public IDS(Puzzle puzzle) => _puzzle = puzzle;

        public Cell[,] IterativeDeepeningSearch()
        {
            Cell[,] board;
            State result;
            for (int depth = 0; depth < Int32.MaxValue; depth++)
            {
                (board, result) = DepthLimitedSearch(depth);
                if (board is not null && result == State.Result)
                {
                    Console.WriteLine($"Depth: {depth}");
                    Output.PrintBoard(board);
                    return board;
                }
            }

            return default;
        }

        private (Cell[,], State) DepthLimitedSearch(int depth) => RecursiveDepthLimitedSearch(new Node(_puzzle.Board), _puzzle.Board, depth);

        private (Cell[,], State) RecursiveDepthLimitedSearch(Node node, Cell[,] problem, int limit)
        {
            bool cutoffOccured = false;
            if (GoalTest(problem, node.Board))
            {
                foreach(Node item in node.PathToSolution())
                {
                    Output.PrintBoard(item.Board);
                }

                return (node.Board, State.Result);
            }
            else if (node.Depth == limit)
            {
                return (null, State.Cutoff);
            }
            else
            {
                node.Expand();
                foreach (var successor in node.Childs)
                {
                    (Cell[,] board, State result) = RecursiveDepthLimitedSearch(successor, problem, limit);
                    if (result == State.Cutoff)
                    {
                        cutoffOccured = true;
                    }
                    else if (result != State.Failure)
                    {
                        return (board, State.Result);
                    }
                }
            }
            
            if (cutoffOccured)
            {
                return (null, State.Cutoff);
            }
            else
            {
                return (null, State.Failure);
            }
        }

        private bool GoalTest(Cell[,] problem, Cell[,] board)
        {
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    if (problem[i, j].Value == board[i, j].Value)
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}