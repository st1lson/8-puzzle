using System;
using Eight_puzzle.ConsoleManager;
using Eight_puzzle.Enums;

namespace Eight_puzzle.Algorithms
{
    internal class Ids
    {
        private int _iterations;
        private int _nodesGenerated;

        public void IterativeDeepeningSearch(int[,] problem)
        {
            for (int depth = 0; depth < Int32.MaxValue; depth++)
            {
                Result result = DepthLimitedSearch(problem, depth);
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
        }

        private Result DepthLimitedSearch(int[,] problem, int depth) => RecursiveDepthLimitedSearch(new Node(problem), depth);

        private Result RecursiveDepthLimitedSearch(Node node, int limit)
        {
            _iterations++;
            if (Node.GoalTest(node.Board))
            {
                return new(node, State.Result);
            }
            else if (node.Depth >= limit)
            {
                return new(node, State.Cutoff);
            }
            else
            {
                node.Expand();
                foreach (Node child in node.Childs)
                {
                    _nodesGenerated++;
                    Result result = RecursiveDepthLimitedSearch(child, limit);
                    if (result.State == State.Result)
                    {
                        return result;
                    }
                }
            }

            return new(node, State.Failure);
        }
    }
}