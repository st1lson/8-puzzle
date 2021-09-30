﻿using Eight_puzzle.ConsoleManager;
using Eight_puzzle.Core;
using Eight_puzzle.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Eight_puzzle.Algorithms
{
    internal sealed class RBFS
    {
        private int _iterations;
        private int _nodesGenerated;
        private readonly Puzzle _puzzle;
        private readonly List<Node> _visited;

        public RBFS(Puzzle puzzle) 
        { 
            _puzzle = puzzle; 
            _visited = new List<Node>();
        }

        public Cell[,] RecursiveBestFirstSearch()
        {
            (Node node, _, _) = RecursiveBFS(new Node(_puzzle.Board), Int32.MaxValue);
            if (node is not null)
            {
                Output.PrintStats(_iterations, _nodesGenerated, node.Depth);
                Output.PrintBoard(node.Board);
                return node.Board;
            }

            return default;
        }

        private (Node, State, int) RecursiveBFS(Node node, int fLimit)
        {
            _iterations++;
            if (!_visited.Contains(node))
            {
                _visited.Add(node);
            }

            if (node.GoalTest())
            {
                foreach(var item in node.PathToSolution())
                {
                    Output.PrintBoard(item.Board);
                }

                return (node, State.Result, fLimit);
            }

            node.Expand();
            if (node.Childs.Count < 1)
            {
                return (null, State.Failure, Int32.MaxValue);
            }

            foreach (var successor in node.Childs)
            {
                successor.PathCost = node.Depth + successor.Heuristic();
                _nodesGenerated++;
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

                int alternative = childs[1].PathCost;
                (Node newNode, State result, int newFLimit) = RecursiveBFS(best, Math.Min(alternative, fLimit));
                
                best.PathCost = newFLimit;

                if (newNode is not null)
                {
                    return (newNode, State.Result, newFLimit);
                }
            }
        }
    }
}