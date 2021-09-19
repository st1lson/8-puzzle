using System;
using System.IO;
using System.Text;
using Eight_puzzle.ClientSide;
using Eight_puzzle.Core;

namespace Eight_puzzle.FileManager
{
    internal sealed class FileOperator
    {
        private string _path;

        public FileOperator() => _path = "default.txt";

        public FileOperator(string path) => _path = path;

        public void ChangeFile(string path) => _path = path;
        
        public Puzzle DeserializePuzzle()
        {
            if (!File.Exists(_path))
            {
                Output.Error("Selected file does not exist");
                return null;
            }
            
            Cell[,] board = new Cell[3, 3];
            int i = 0;
            int j = 0;
            using (StreamReader streamReader = new StreamReader(_path, Encoding.Default))
            {
                while (!streamReader.EndOfStream)
                {
                    int[] cells = Array.ConvertAll(streamReader.ReadLine()?.Split(" ", StringSplitOptions.RemoveEmptyEntries) ?? throw new Exception("Corrupted cells in file"), int.Parse);
                    foreach (var cell in cells)
                    {
                        board[i, j] = new Cell
                        {
                            Value = cell,
                            Position = new Position
                            {
                                XPos = i,
                                YPos = j
                            },
                            IsEmpty = cell == 0,
                            IsRight = cell == i * 3 + j + 1 
                        };
                        
                        j++;
                    }

                    i++;
                    j = 0;
                }
            }

            foreach (var cell in board)
            {
                Console.WriteLine(cell);
            }
            return new Puzzle(board);
        }

        public void SerializePuzzle(Puzzle puzzle)
        {
            if (!File.Exists(_path))
            {
                Output.Error("Selected file does not exist");
                return;
            }

            using (StreamWriter streamWriter = new StreamWriter(_path, false, Encoding.Default))
            {
                StringBuilder stringBuilder = new StringBuilder();
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        stringBuilder.Append(puzzle.Board[i, j].Value + " ");
                    }

                    stringBuilder.Append('\n');
                }

                streamWriter.Write(stringBuilder.ToString());
            }
        }
    }
}