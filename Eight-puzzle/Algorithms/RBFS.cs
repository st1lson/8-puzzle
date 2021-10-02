using Eight_puzzle.ConsoleManager;
using Eight_puzzle.Enums;
using System;
using System.Linq;

namespace Eight_puzzle.Algorithms
{
    internal class Rbfs
    {
        private int _iterations;
        private int _nodesGenerated;

        public void RecursiveBestFirstSearch(int[,] problem)
        {
            Result result = RecursiveBfs(new Node(problem), Int32.MaxValue);
            if (result.State == State.Result)
            {
                Output.PrintStats(_iterations, _nodesGenerated, result.Node.Depth);
                foreach (var item in result.Node.PathToSolution())
                {
                    Output.PrintBoard(item.Board);
                }
            }
        }

        private Result RecursiveBfs(Node node, int fLimit)
        {
            _iterations++;
            if (Node.GoalTest(node.Board))
            {
                return new(node, State.Result);
            }

            node.Expand();
            if (node.Childs.Count == 0)
            {
                return new Result(node, State.Failure);
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
                    node.Childs.Clear();
                    return new Result(best, State.Failure);
                }
                int alternative = Int32.MaxValue;
                if (childs.Length > 1)
                {
                    alternative = childs[1].PathCost;
                }

                Result result = RecursiveBfs(best, Math.Min(fLimit, alternative));
                best.PathCost = result.PathCost;
                if (result.State == State.Result)
                {
                    return result;
                }
            }
        }
    }
}