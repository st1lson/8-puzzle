using System;
using System.Text;
using Eight_puzzle.Algorithms;
using Eight_puzzle.Core;
using Eight_puzzle.FileManager;

namespace Eight_puzzle.ClientSide
{
    internal static class Output
    {
        private static Puzzle _puzzle;
        private static FileOperator _fileOperator;
        private static string _menu;

        public static void Menu()
        {
            if (_menu == null)
            {
                _menu = CreateMenu();
            }

            Console.WriteLine(_menu);
            string input = Console.ReadLine();
            if (!Int32.TryParse(input, out var value))
            {
                Error("Input should be a number");
            }

            if (_puzzle is null)
            {
                _puzzle = new Puzzle();
                _puzzle.Initialize();
            }
            
            PrintBoard(_puzzle.Board);
            Console.WriteLine();
            Action(value);
            /*Console.Clear();
            Menu();*/
        }

        public static void Error(string errorMessage)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"[ERROR] {DateTime.Now} - {errorMessage}");
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void PrintBoard(Cell[,] board)
        {
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    Console.Write(board[i, j].Value + "\t");
                }

                Console.WriteLine();
            }
        }

        private static void Action(int value)
        {
            switch (value)
            {
                case 1:
                    Console.WriteLine();
                    PrintBoard(new IDS(_puzzle).IterativeDeepeningSearch());
                    break;
                case 2:
                    new RBFS(_puzzle).RecursiveBestFirstSearch();
                    break;
                case 3:
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
            stringBuilder.AppendLine("Use '3' to change source file");
            stringBuilder.AppendLine("Use '0' to exit");
            
            return stringBuilder.ToString();
        }
    }
}