﻿using System;

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
            for (int i = 0; i < Board.GetLength(0); i++)
            {
                for (int j = 0; j < Board.GetLength(1); j++)
                {
                    if (i == 2 && j == 2)
                    {
                        value = 0;
                    }
                    
                    Board[i, j] = new Cell
                    {
                        Value = value++,
                        Position = new Position
                        {
                            XPos = i,
                            YPos = j
                        },
                        IsRight = true
                    };
                }
            }
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
        }
    }
}