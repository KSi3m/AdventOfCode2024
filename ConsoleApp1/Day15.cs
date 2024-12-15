using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Day15
    {
        public int Part1()
        {
            string filePath = "..\\..\\..\\input_day15_1.txt";
           // string filePath = "..\\..\\..\\text.txt";

            try
            {
                var lines = File.ReadAllLines(filePath);

         
                int indexOfEmptySpace = -1;
                for (int i = 0; i < lines.Length; i++)
                {
             
                    if (lines[i] == "")
                    {
                        indexOfEmptySpace = i;
                        break;
                    }
                }
                (int X, int Y) robotPosition = (-1, -1);
                int rows = indexOfEmptySpace;
                int cols = lines[0].Length;
                char[,] emptyGrid = new char[indexOfEmptySpace, lines[0].Length];
                for (int i = 0; i < indexOfEmptySpace; i++)
                {
                    for (int j = 0; j < lines[i].Length; j++)
                    {
                        if (lines[i][j] == '@')
                            robotPosition = (i, j);
                         emptyGrid[i, j] = lines[i][j];

                    }
                }
               // print(emptyGrid);
                Console.WriteLine("***");
                List<char> moves = new List<char>();
                for (int i = indexOfEmptySpace+1; i < lines.Length; i++)
                {
                    for (int j = 0; j < lines[i].Length; j++)
                    {
                        moves.Add(lines[i][j]);

                    }
                }

                PerformMovements(moves, robotPosition, emptyGrid);

                int total = 0;

                for (int i = 0; i < emptyGrid.GetLength(0); i++)
                {
                    for (int j = 0; j < emptyGrid.GetLength(1); j++)
                    {
                        if (emptyGrid[i, j] == 'O')
                            total += 100 * i + j;
                    }
                }
               
                return total;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd: {ex.Message}");
                return 0;
            }

        }

        public void print(char[,] kek)
        {
            for (int i = 0; i < kek.GetLength(0); i++)
            {
                for (int j = 0; j < kek.GetLength(1); j++)
                {
                    Console.Write(kek[i, j]);
                }
                Console.WriteLine("");
            }
        }
        public void PerformMovements(List<char> moves, (int X, int Y) robotPosition, char[,] emptyGrid)
        {
            int rows = emptyGrid.GetLength(0);
            int cols = emptyGrid.GetLength(1);
            foreach (var m in moves)
            {
                if (m == '>')
                {
                    int k;
                    for (k = robotPosition.Y + 1; k < cols; k++)
                    {
                        if (emptyGrid[robotPosition.X, k] == 'O')
                        {
                            continue;
                        }
                        else if (emptyGrid[robotPosition.X, k] == '.')
                        {
                            emptyGrid[robotPosition.X, robotPosition.Y] = '.';

                            emptyGrid[robotPosition.X, robotPosition.Y + 1] = '@';
                            for (int j = robotPosition.Y + 2; j < k + 1; j++)
                            {
                                emptyGrid[robotPosition.X, j] = 'O';
                            }
                            robotPosition = (robotPosition.X, robotPosition.Y + 1);
                            break;
                        }
                        else if (emptyGrid[robotPosition.X, k] == '#')
                        {
                            break;
                        }
                    }


                }
                else if (m == '<')
                {
                    int k;
                    for (k = robotPosition.Y - 1; k >= 0; k--)
                    {
                        if (emptyGrid[robotPosition.X, k] == 'O')
                        {
                            continue;
                        }
                        else if (emptyGrid[robotPosition.X, k] == '.')
                        {
                            emptyGrid[robotPosition.X, robotPosition.Y] = '.';

                            emptyGrid[robotPosition.X, robotPosition.Y - 1] = '@';
                            for (int j = robotPosition.Y - 2; j > k - 1; j--)
                            {
                                emptyGrid[robotPosition.X, j] = 'O';
                            }
                            robotPosition = (robotPosition.X, robotPosition.Y - 1);
                            break;
                        }
                        else if (emptyGrid[robotPosition.X, k] == '#')
                        {
                            break;
                        }
                    }
                }
                else if (m == '^')
                {
                    int k;
                    for (k = robotPosition.X - 1; k >= 0; k--)
                    {
                        if (emptyGrid[k, robotPosition.Y] == 'O')
                        {
                            continue;
                        }
                        else if (emptyGrid[k, robotPosition.Y] == '.')
                        {
                            emptyGrid[robotPosition.X, robotPosition.Y] = '.';

                            emptyGrid[robotPosition.X - 1, robotPosition.Y] = '@';
                            for (int j = robotPosition.X - 2; j > k - 1; j--)
                            {
                                emptyGrid[j, robotPosition.Y] = 'O';
                            }
                            robotPosition = (robotPosition.X - 1, robotPosition.Y);
                            break;
                        }
                        else if (emptyGrid[k, robotPosition.Y] == '#')
                        {
                            break;
                        }
                    }



                }
                else if (m == 'v')
                {
                    int k;
                    for (k = robotPosition.X + 1; k < rows; k++)
                    {
                        if (emptyGrid[k, robotPosition.Y] == 'O')
                        {
                            continue;
                        }
                        else if (emptyGrid[k, robotPosition.Y] == '.')
                        {
                            emptyGrid[robotPosition.X, robotPosition.Y] = '.';

                            emptyGrid[robotPosition.X + 1, robotPosition.Y] = '@';
                            for (int j = robotPosition.X + 2; j < k + 1; j++)
                            {
                                emptyGrid[j, robotPosition.Y] = 'O';
                            }
                            robotPosition = (robotPosition.X + 1, robotPosition.Y);
                            break;
                        }
                        else if (emptyGrid[k, robotPosition.Y] == '#')
                        {
                            break;
                        }
                    }

                    // Console.WriteLine("***");
                    //print(emptyGrid);



                }
            }
        }

    }
}
