using System;

namespace Eight_puzzle.Core
{
    internal class Puzzle
    {
        public int[,] Board { get; }
        private readonly Random _random;

        public Puzzle()
        {
            Board = new int[3, 3];
           _random = new Random();
        }

        public Puzzle(int[,] board)
        {
            Board = board;
            _random = new Random();
        }

        public void Initialize()
        {
            int value = 1;
            for (int i = 0; i < Board.GetLength(0); i++)
            {
                for (int j = 0; j < Board.GetLength(1); j++)
                {
                    if (i == 2 && j == 2)
                    {
                        value = 0;
                    }

                    Board[i, j] = value++;
                }
            }
        }

        private bool IsSolvable() => GetInvCount() % 2 == 0;

        public void Shuffle()
        {
            for (int i = 0; i < Board.GetLength(0); i++)
            {
                for (int j = 0; j < Board.GetLength(1); j++)
                {   
                    int row = _random.Next(3);
                    int column = _random.Next(3);
                    int temp = Board[i, j];
                    Board[i, j] = Board[row, column];
                    Board[row, column] = temp;
                }
            }

            if (!IsSolvable())
            {
                Shuffle();
            }
        }

        private int GetInvCount()
        {
            int invCount = 0;
            for (int i = 0; i < 3 - 1; i++)
            {
                for (int j = i + 1; j < 3; j++)
                {
                    if (Board[j, i] > 0 && Board[j, i] > Board[i, j])
                    {
                        invCount++;
                    }
                }
            }

            return invCount;
        }
    }
}