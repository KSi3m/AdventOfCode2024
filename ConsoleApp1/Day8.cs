using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Day8
    {
        public int Part1()
        {
            string filePath = "..\\..\\..\\input_day8_1.txt";
             //string filePath = "..\\..\\..\\text.txt";

            try
            {
                var lines = File.ReadAllLines(filePath);
                int rows = lines.Length;
                int cols = lines[0].Length;

                Dictionary<char, List<(int, int)>> dict = new Dictionary<char, List<(int, int)>>();
                char[,] emptyGrid = new char[lines.Length, lines[0].Length];
                for (int i = 0; i < lines.Length; i++)
                {
                    for (int j = 0; j < lines[i].Length; j++)
                    {
                        emptyGrid[i,j] = lines[i][j];
                        if (lines[i][j] != '.')
                        {
                            if(dict.TryGetValue(lines[i][j], out var value))
                            {
                                value.Add((i, j));
                            }
                            else{
                                dict.TryAdd(lines[i][j], new List<(int, int)>() { (i, j) });
                            }
                        }

                    }
                }

                int count = 0;
                foreach (var (x, y) in dict)
                {
                    for(int i = 0;i<y.Count;i++ )
                    {
                        for (int j =i+1;j< y.Count ; j++)
                        {
                            var point1 = y.ElementAt(i);
                            var point2 = y.ElementAt(j);

                            var xP = Math.Abs(point2.Item1 - point1.Item1);
                            var yP = Math.Abs(point2.Item2 - point1.Item2);

  
                            double a = (double)(point2.Item2 - point1.Item2) / (double)(point2.Item1 - point1.Item1);

                            var antinode1 = (0,0);
                            var antinode2 = (0,0);
                            if (a<0)
                            {
                                antinode1 = (point2.Item1 + xP, point2.Item2 - yP);
                                antinode2 = (point1.Item1 - xP, point1.Item2 + yP);

                            }
                            else if(a==0)
                            {
                                antinode1 = (point2.Item1 + xP, point2.Item2);
                                antinode2 = (point1.Item1 - xP, point1.Item2);
                            }
                            else
                            {
                                antinode1 = (point2.Item1 + xP, point2.Item2 + yP);
                                antinode2 = (point1.Item1 - xP, point1.Item2 - yP);
                            }


                            count += UpdateGrid(antinode1, rows, cols,  emptyGrid);
                            count += UpdateGrid(antinode2, rows, cols,  emptyGrid);

                        }
                      
                    }
                }

                print(emptyGrid);
                return count;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd: {ex.Message}");
                return 0;
            }

        }

        public int UpdateGrid((int,int) antinode, int rows, int cols,  char[,] emptyGrid)
        {
            if (antinode.Item1 >= 0 && antinode.Item2 >= 0 && antinode.Item1 < rows
                               && antinode.Item2 < cols)
            {

                if (emptyGrid[antinode.Item1, antinode.Item2] != '#')
                {
                    emptyGrid[antinode.Item1, antinode.Item2] = '#';
                    return 1;
                }

            }
            return 0;
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
    }
}
