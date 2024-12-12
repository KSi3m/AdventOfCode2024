using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Day10
    {
        public int Part1()
        {
            string filePath = "..\\..\\..\\input_day10_1.txt";
           // string filePath = "..\\..\\..\\text.txt";

            try
            {
                var lines = File.ReadAllLines(filePath);
                var text = string.Join("\n", lines);

                int rows = lines[0].Length;
                int cols = lines.Length;


                int[,] emptyGrid = new int[lines.Length, lines[0].Length];
                List<(int, int)> positionsOfZero = new List<(int, int)>();
                for (int i = 0; i < lines.Length; i++)
                {
                    for (int j = 0; j < lines[i].Length; j++)
                    {

                        if (lines[i][j] == '0')
                        {
                            positionsOfZero.Add((i, j));
                        }
                        if (lines[i][j] == '.')
                        {
                            emptyGrid[i, j] = -10;
                            continue;
                        }
                        emptyGrid[i, j] = lines[i][j]-'0';

                    }
                }
                List<(int, int)> idOf9 = new List<(int, int)>();
                List<(int, int)> listOfAll = new List<(int, int)>();

                foreach (var item in positionsOfZero)
                {
                    Rec(item.Item1, item.Item2, emptyGrid, -1, idOf9);  
                    listOfAll.AddRange(idOf9.Distinct());
                    idOf9.Clear();
                }

                return listOfAll.Count;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd: {ex.Message}");
                return 0;
            }

        }

        public bool Rec(int row, int col, int[,] emptyGrid, int currentNumber, List<(int,int)> idOf9)
        {
        
            if (col >= emptyGrid.GetLength(1) || row >= emptyGrid.GetLength(0)
                || row < 0 || col < 0) return false;

            if (emptyGrid[row, col] - currentNumber != 1) return false;

            if (emptyGrid[row, col] == 9)
            {
                idOf9.Add((row, col));
            }

            if(Rec(row-1,col,emptyGrid, emptyGrid[row, col], idOf9)
                || Rec(row,col+1,emptyGrid, emptyGrid[row, col], idOf9)
                || Rec(row+1, col, emptyGrid, emptyGrid[row, col], idOf9)
                || Rec(row, col - 1, emptyGrid, emptyGrid[row, col],idOf9)
                )
            {
                return true;
            }

            return false;
        }


       /* public int Part1()
        {
            string filePath = "..\\..\\..\\input_day10_1.txt";
            // string filePath = "..\\..\\..\\text.txt";

            try
            {
                var lines = File.ReadAllLines(filePath);
                var text = string.Join("\n", lines);

                int rows = lines[0].Length;
                int cols = lines.Length;


                int[,] emptyGrid = new int[lines.Length, lines[0].Length];
                List<(int, int)> positionsOfZero = new List<(int, int)>();
                for (int i = 0; i < lines.Length; i++)
                {
                    for (int j = 0; j < lines[i].Length; j++)
                    {

                        if (lines[i][j] == '0')
                        {
                            positionsOfZero.Add((i, j));
                        }
                        if (lines[i][j] == '.')
                        {
                            emptyGrid[i, j] = -10;
                            continue;
                        }
                        emptyGrid[i, j] = lines[i][j] - '0';

                    }
                }
                Console.WriteLine(emptyGrid.GetLength(0));
                Console.WriteLine(emptyGrid.GetLength(1));
                int count = 0;
                List<(int, int)> idOf9 = new List<(int, int)>();
                List<(int, int)> listOfAll = new List<(int, int)>();

                foreach (var item in positionsOfZero)
                {
                    Console.WriteLine($"\t\t{item}");
                }
                foreach (var item in positionsOfZero)
                {
                    Console.WriteLine($"\tk{item}");
                    Rec(item.Item1, item.Item2, emptyGrid, -1, idOf9);
                    listOfAll.AddRange(idOf9.Distinct());
                    idOf9.Clear();
                }


                foreach (var item in listOfAll)
                {
                    Console.WriteLine($"\t{item}");
                }
                Console.WriteLine(listOfAll.Count);
                return count;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd: {ex.Message}");
                return 0;
            }

        }*/
    }
}
