using System;
using System.Text;
using Eight_puzzle.Algorithms;
using Eight_puzzle.Core;
using Eight_puzzle.FileManager;

namespace Eight_puzzle.ConsoleManager
{
    internal static class Output
    {
        private static Puzzle _puzzle;
        private static FileOperator _fileOperator;
        private static string _menu;

        public static void Menu()
        {
            _menu ??= CreateMenu();
            Console.WriteLine(_menu);
            string input = Console.ReadLine();
            if (!int.TryParse(input, out var value))
            {
                Error("Input should be a number");
            }

            if (_puzzle is null)
            {
                /*_puzzle = new Puzzle();
                _puzzle.Initialize();
                _puzzle.Shuffle();*/
                _fileOperator = new FileOperator();
                _puzzle = _fileOperator.DeserializePuzzle();
            }
            
            if (value is 1 or 2)
            {
                Console.WriteLine("Initial board:");
                PrintBoard(_puzzle.Board);
            }
            
            Action(value);

            Console.WriteLine("Press 'Enter' to continue");
            if (Console.ReadKey().Key.Equals(ConsoleKey.Enter))
            {
                Console.Clear();
                Menu();
            }
        }

        public static void Error(string errorMessage)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"[ERROR] {DateTime.Now} - {errorMessage}");
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void PrintBoard(int[,] board)
        {
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    Console.Write(board[i, j] + "\t");
                }

                Console.WriteLine();
            }

            Console.WriteLine();
        }

        public static void PrintStats(int iterations, int nodesGenerated, int depth)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append($"Number of iterations: {iterations}\n");
            stringBuilder.Append($"Number of generated nodes: {nodesGenerated}\n");
            stringBuilder.Append($"Depth: {depth}\n");

            Console.WriteLine(stringBuilder.ToString());
        }

        private static void Action(int value)
        {
            switch (value)
            {
                case 1:
                    Console.WriteLine();
                    new Ids().IterativeDeepeningSearch(_puzzle.Board);
                    break;
                case 2:
                    Console.WriteLine();
                    new Rbfs().RecursiveBestFirstSearch(_puzzle.Board);
                    break;
                case 3:
                    _puzzle.Shuffle();
                    PrintBoard(_puzzle.Board);
                    break;
                case 4:
                    Console.WriteLine("Enter a file path:");
                    _fileOperator ??= new FileOperator();
                    _fileOperator.ChangeFile(Console.ReadLine());
                    break;
                case 0:
                    Environment.Exit(0);
                    break;
            }
        }

        private static string CreateMenu()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("Menu:");
            stringBuilder.AppendLine("Use '1' to use IDS algorithm");
            stringBuilder.AppendLine("Use '2' to use RBFS algorithm");
            stringBuilder.AppendLine("Use '3' to shuffle the board");
            stringBuilder.AppendLine("Use '4' to change source file");
            stringBuilder.AppendLine("Use '0' to exit");

            return stringBuilder.ToString();
        }
    }
}