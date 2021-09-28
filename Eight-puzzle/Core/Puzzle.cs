using System;

namespace Eight_puzzle.Core
{
    internal class Puzzle
    {
        public Cell[,] Board { get; }
        private readonly Random _random;

        public Puzzle()
        {
            Board = new Cell[3, 3];
           _random = new Random();
        }

        public Puzzle(Cell[,] board)
        {
            Board = board;
        }

        public void Initialize()
        {
            int value = 1;
            bool isEmpty = false;
            for (int i = 0; i < Board.GetLength(0); i++)
            {
                for (int j = 0; j < Board.GetLength(1); j++)
                {
                    if (i == 2 && j == 2)
                    {
                        value = 0;
                        isEmpty = true;
                    }
                    
                    Board[i, j] = new Cell
                    {
                        Value = value++,
                        IsRight = true,
                        IsEmpty = isEmpty
                    };
                }
            }
        }

        private int GetInvCount()
        {
            int invCount = 0;
            for (int i = 0; i < 3 - 1; i++)
            {
                for (int j = i + 1; j < 3; j++)
                {
                    if (Board[j, i].Value > 0 && Board[j, i].Value > Board[i, j].Value)
                    {
                        invCount++;
                    }
                }
            }

            return invCount;
        }

        private bool IsSolvable()
        {
            int invCount = GetInvCount();

            return (invCount % 2 == 0);
        }

        public void Shuffle()
        {
            for (int i = 0; i < Board.GetLength(0); i++)
            {
                for (int j = 0; j < Board.GetLength(1); j++)
                {   
                    int row = _random.Next(3);
                    int column = _random.Next(3);
                    Cell temp = Board[i, j];
                    Board[i, j] = Board[row, column];
                    Board[row, column] = temp;
                }
            }

            if (!IsSolvable())
            {
                Shuffle();
            }
        }
    }
}