using Eight_puzzle.ConsoleManager;
using Eight_puzzle.Core;
using Eight_puzzle.Enums;
using System;
using System.Linq;

namespace Eight_puzzle.Algorithms
{
    internal sealed class RBFS
    {
        private readonly Puzzle _puzzle;

        public RBFS(Puzzle puzzle) => _puzzle = puzzle;

        public Cell[,] RecursiveBestFirstSearch()
        {
            (Node node, _, _) = RecursiveBFS(new Node(_puzzle.Board), Int32.MaxValue);
            if (node.Board is not null)
            {
                Output.PrintBoard(node.Board);
                return node.Board;
            }

            return default;
        }

        private (Node, State, int) RecursiveBFS(Node node, int fLimit)
        {
            if (node.GoalTest())
            {
                return (node, State.Result, fLimit);
            }

            node.Expand();
            if (node.Childs is null)
            {
                return (null, State.Failure, Int32.MaxValue);
            }

            foreach (var successor in node.Childs)
            {
                if (successor.PathCost < node.PathCost)
                {
                    successor.PathCost = node.PathCost;
                }
            }

            while (true)
            {
                Node[] childs = node.
                    GetChilds().
                    OrderBy(n => n.PathCost).
                    Take(2).
                    ToArray();
                Node best = childs[0];
                if (best.PathCost > fLimit)
                {
                    return (null, State.Failure, best.PathCost);
                }

                int alternative = childs[^1].PathCost;
                (Node newNode, State result, int newFLimit) = RecursiveBFS(best, Math.Min(fLimit, alternative));

                best.PathCost = newFLimit;

                if (newNode is not null)
                {
                    return (node, result, 0);
                }
            }
        }
    }
}