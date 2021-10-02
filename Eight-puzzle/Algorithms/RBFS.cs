using Eight_puzzle.ConsoleManager;
using Eight_puzzle.Core;
using Eight_puzzle.Enums;
using System;
using System.Linq;

namespace Eight_puzzle.Algorithms
{
    internal class RBFS
    {
        private int _iterations;
        private int _nodesGenerated;
        private readonly Puzzle _puzzle;

        public RBFS(Puzzle puzzle) 
        { 
            _puzzle = puzzle; 
        }

        public void RecursiveBestFirstSearch()
        {
            Result result = RecursiveBFS(new Node(_puzzle.Board), Int32.MaxValue);
            if (result.State == State.Result)
            {
                Output.PrintStats(_iterations, _nodesGenerated, result.Node.Depth);
                foreach (var item in result.Node.PathToSolution())
                {
                    Output.PrintBoard(item.Board);
                }

                return;
            }
        }

        private Result RecursiveBFS(Node node, int fLimit)
        {
            _iterations++;
            if (Node.GoalTest(node.Board))
            {
                return new(node, State.Result);
            }

            node.Expand();
            if (node.Childs.Count == 0)
            {
                return new(node, State.Failure);
            }

            _nodesGenerated += node.Childs.Count;

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
                    return new(node, State.Failure, best.PathCost);
                }

                int alternative;
                if (node.Childs.Count == 1)
                {
                    alternative = best.PathCost;
                }
                else
                {
                    alternative = childs[1].PathCost;
                }

                Result result = RecursiveBFS(best, Math.Min(alternative, fLimit));

                best.PathCost = result.PathCost;

                if (result.State == State.Result)
                {
                    return result;
                }
            }
        }
    }
}