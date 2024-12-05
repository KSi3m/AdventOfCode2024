using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Principal;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Day4
    {
        public int Part1()
        {
            string filePath = "..\\..\\..\\input_day4_1.txt";
            //string filePath = "..\\..\\..\\text.txt";

            try
            {
                var lines = File.ReadAllLines(filePath);
                var text = string.Join("\n", lines);
                StringBuilder stringBuilder = new StringBuilder();

               
                int rows = lines[0].Length;
                int cols = lines.Length;

                int count = 0;

                //Horizontally
                foreach (var line in lines)
                {
                    count += CountOccurences("SAMX", line) + CountOccurences("XMAS", line);
                }

                //Vertically
                for(int i = 0; i < rows;i++)
                {
                    for(int j=0;j<cols;j++)
                    {
                        stringBuilder.Append(lines[j][i]);
                    }
                    var result = stringBuilder.ToString();
                    stringBuilder.Clear();

                    count += CountOccurences("SAMX", result) + CountOccurences("XMAS", result);
                }

                //Diagonally from left to right
                for (int start = 0; start < rows + cols - 1; start++)
                {
           
                    for (int i = 0; i < rows; i++)
                    {
                        int j = start - i;
                        if (j < 0) break;
                        if (j < cols)
                        {
                            stringBuilder.Append(lines[i][j]);
                        }
                    }
                    var result = stringBuilder.ToString();
                    stringBuilder.Clear();

                    if (result.Length < 4) continue;
                    count += CountOccurences("SAMX", result) + CountOccurences("XMAS", result);
                    
                }

                //Diagonally from right to left
                for (int start = rows + cols - 2; start >=0; start--)
                {

                    for (int i = 0; i < rows; i++)
                    {
                        int j =  start + i - (cols - 1);
                        if (j >= cols) break;
                        if (j >= 0) { 
                            stringBuilder.Append(lines[i][j]);
                        }
                    }
                    var result = stringBuilder.ToString();
        
                    stringBuilder.Clear();
                    if (result.Length < 4) continue;
               
                    count += CountOccurences("SAMX", result) + CountOccurences("XMAS", result);
                    
                }

                return count;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd: {ex.Message}");
                return 0;
            }

        }


        private int CountOccurences(string searched, string line)
        {
            int index = line.IndexOf(searched);
            int count = 0;
            while (index != -1)
            {
                count++;
                index = line.IndexOf(searched, index + 1);
            }
            return count;
        }

        public int Part2()
        {
            string filePath = "..\\..\\..\\input_day4_1.txt";

            try
            {
                var lines = File.ReadAllLines(filePath);

                int rows = lines[0].Length;
                int cols = lines.Length;

                int count = 0;
                for(int j=0;j<cols-2;j++)
                {
                    for (int i = 0; i < rows-2; i++)
                    {
                        var xmasGrid = new char[3, 3];
                        for (int k = 0; k < 3; k++)
                        {
                            for(int h = 0; h < 3; h++)
                            {
                                xmasGrid[k,h] = lines[i + k][j + h];
                            }
                        }
                        if (CheckIfBoxIsXMAS(xmasGrid)) count++;
                    }
                }
                

                return count;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd: {ex.Message}");
                return 0;
            }

        }
        private bool CheckIfBoxIsXMAS(char[,] arr)
        {
            var mainDiagonal = string.Join("",Enumerable.Range(0, 3)  
                             .Select(i => arr[i, i]));

            var secondaryDiagonal = string.Join("", Enumerable.Range(0, 3)
                                   .Select(i => arr[i, 2 - i]));


            string[] searched = { "MAS", "SAM" };
            if (searched.Any(word => mainDiagonal.Contains(word)) 
                && searched.Any(word => secondaryDiagonal.Contains(word)))
            {
                return true;
            }

            return false; 
        }

    }
}
