using System;
using Eight_puzzle.ConsoleManager;
using Eight_puzzle.Core;
using Eight_puzzle.Enums;

namespace Eight_puzzle.Algorithms
{
    internal sealed class IDS
    {
        private int _iterations;
        private int _nodesGenerated;
        private readonly Puzzle _puzzle;

        public IDS(Puzzle puzzle) => _puzzle = puzzle;

        public Cell[,] IterativeDeepeningSearch()
        {
            for (int depth = 0; depth < Int32.MaxValue; depth++)
            {
                (Node node, State result) = DepthLimitedSearch(depth);
                if (node is not null && result == State.Result)
                {
                    Output.PrintStats(_iterations, _nodesGenerated, node.Depth);
                    Output.PrintBoard(node.Board);
                    return node.Board;
                }
            }

            return default;
        }

        private (Node, State) DepthLimitedSearch(int depth) => RecursiveDepthLimitedSearch(new Node(_puzzle.Board), _puzzle.Board, depth);

        private (Node, State) RecursiveDepthLimitedSearch(Node node, Cell[,] problem, int limit)
        {
            _iterations++;
            bool cutoffOccured = false;
            if (node.GoalTest())
            {
                foreach (var item in node.PathToSolution())
                {
                    Output.PrintBoard(item.Board);
                }

                return (node, State.Result);
            }
            else if (node.Depth >= limit)
            {
                return (null, State.Cutoff);
            }
            else
            {
                node.Expand();
                foreach (var successor in node.Childs)
                {
                    _nodesGenerated++;
                    (Node newNode, State result) = RecursiveDepthLimitedSearch(successor, problem, limit);
                    if (result == State.Cutoff)
                    {
                        cutoffOccured = true;
                    }
                    else if (newNode is not null)
                    {
                        return (newNode, State.Result);
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
    }
}