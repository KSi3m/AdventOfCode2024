using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Day14
    {
        public int Part1()
        {
             string filePath = "..\\..\\..\\input_day14_1.txt";
            //string filePath = "..\\..\\..\\text.txt";

            try
            {
                var lines = File.ReadAllLines(filePath);
                List<(int PX, int PY, int VX, int VY)> points = new List<(int PX, int PY, int VX, int VY)>();

                foreach(var line in lines)
                {
                    var parts = line.Split(" ");
                    var pPart = parts[0].Substring(2).Split(",");
                    var vPart = parts[1].Substring(2).Split(",");


                    points.Add((int.Parse(pPart[1]), int.Parse(pPart[0]), int.Parse(vPart[1]), int.Parse(vPart[0])));

                }

                var rows = 103;
                //var rows = 7;
                //var cols = 11;
                var cols = 101;

                var middleOnXAxis = rows / 2;
                var middleOnYAxis = cols / 2;

             

                int[,] grid = new int[rows, cols];

                int total = 0;
                int[] quadrants = new int[4]; //topLeft - topRight - bottomRight - bottomLeft
                foreach (var x in points)
                {
                    var positionX = x.PX;
                    var positionY = x.PY;
                    for(int i =0;i<100;i++)
                    {
                        positionX = positionX + x.VX;
                        positionX = ((positionX % rows) + rows) % rows;
                        positionY  = positionY + x.VY;
                        positionY = ((positionY % cols) + cols) % cols;
                    }
   
                    grid[positionX, positionY]++;

                    if (positionX < middleOnXAxis && positionY < middleOnYAxis)
                    {
                        quadrants[0]++;
                        continue;
                    }
                    if (positionX < middleOnXAxis && positionY > middleOnYAxis)
                    {
                        quadrants[1]++;
                        continue;
                    }
                    if (positionX > middleOnXAxis && positionY > middleOnYAxis)
                    {
                        quadrants[2]++;
                        continue;
                    }
                    if (positionX > middleOnXAxis && positionY < middleOnYAxis) 
                    {
                        quadrants[3]++;
                        continue;
                    }
                }
                total = quadrants[0] * quadrants[1] * quadrants[2] * quadrants[3];
                return total;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd: {ex.Message}");
                return 0;
            }

        }



        public void Part2()
        {
            string filePath = "..\\..\\..\\input_day14_1.txt";
            //string filePath = "..\\..\\..\\text.txt";

            try
            {
                var lines = File.ReadAllLines(filePath);
                List<(int PX, int PY, int VX, int VY)> points = new List<(int PX, int PY, int VX, int VY)>();

                foreach (var line in lines)
                {
                    var parts = line.Split(" ");
                    var pPart = parts[0].Substring(2).Split(",");
                    var vPart = parts[1].Substring(2).Split(",");


                    points.Add((int.Parse(pPart[1]), int.Parse(pPart[0]), int.Parse(vPart[1]), int.Parse(vPart[0])));

                }

                var rows = 103;
                var cols = 101;

                int[,] grid = new int[rows, cols];
                List<int[,]> listOfGrids = new List<int[,]>();

                foreach (var x in points)
                {
                    var positionX = x.PX;
                    var positionY = x.PY;
                    for (int i = 0; i < 10000; i++)
                    {
                     
                        positionX = positionX + x.VX;
                        positionX = ((positionX % rows) + rows) % rows;
                        positionY = positionY + x.VY;
                        positionY = ((positionY % cols) + cols) % cols;
                        if (listOfGrids.Count == i)
                        {
                            var xd = CopyArray(grid,rows,cols);
                            xd[positionX, positionY]++;
                            listOfGrids.Add(xd);
                        }
                        else
                        {
                            listOfGrids.ElementAt(i)[positionX, positionY]++;
                        }
                    }
    
                }

                //wyszukanie w listach gridów poszczególnych iteracji, gridów z największą ilością
                // jedynek w pojedynczych liniach 
                //trochę zadziałało bo faktycznie poprawny wynik jednym z 4 o największej ilości jedynek
                List<(int, int)> secondWithAmountOfOnes = new List<(int, int)>(); 
                for(int i =0;i<listOfGrids.Count;i++)
                {
                    secondWithAmountOfOnes.Add((i, GetMaxAmountOfOnesInLineFromGrid(listOfGrids[i], rows, cols)));
                }

                secondWithAmountOfOnes.Sort((x,y) => y.Item2.CompareTo(x.Item2));

                foreach(var x in secondWithAmountOfOnes.Take(5))
                {
                    Console.WriteLine(x);
                }


                //padło na grid o numerze 7686 - w odpowiedzi należało dopisać jedynkę bo tam sekundy 
                //liczono od 1, a nie jak iteracje od 0; 
                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < cols; j++)
                    {
                        if(listOfGrids.ElementAt(7686)[i, j] == 0) Console.Write(".");
                        else Console.Write(listOfGrids.ElementAt(7686)[i, j]);
                    }
                    Console.WriteLine("");
                }


            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd: {ex.Message}");
              
            }

        }

        public int[,] CopyArray(int[,] grid, int rows, int cols)
        {
            int[,] deepCopy = new int[rows, cols];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    deepCopy[i, j] = grid[i, j];
                }
            }
            return deepCopy;
        }

        public int GetMaxAmountOfOnesInLineFromGrid(int[,] grid, int rows, int cols)
        {
            int count = 0;
            int max = -1;
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    if (grid[i, j] == 1) count++;
                }
                if(count > max ) max = count;
                count = 0;
            }
            return max;
        }
    }
}
