using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Day6
    {
        public int Part1()
        {
            string filePath = "..\\..\\..\\input_day6_1.txt";
           // string filePath = "..\\..\\..\\text.txt";

            try
            {
                var lines = File.ReadAllLines(filePath);

                int iForGuard = -1;
                int jForGuard = -1;
   
                char[,] emptyGrid = new char[lines.Length, lines[0].Length];
                for(int i=0;i<lines.Length;i++)
                {
                    for(int j = 0; j < lines[i].Length;j++)
                    {
  
                        if (lines[i][j] == '^')
                        {
                            iForGuard = i;
                            jForGuard = j;
                            emptyGrid[i, j] = 'X';
                            continue;
                        }
                        emptyGrid[i,j] = lines[i][j];

                    }
                }


                int rows = emptyGrid.GetLength(0);
                int cols = emptyGrid.GetLength(1);
                int direction = 0;

                 while (iForGuard < rows && jForGuard < cols)
                 {
                     if (direction == 0)
                     {

                         if (iForGuard - 1 < 0)
                         {
                             emptyGrid[iForGuard, jForGuard] = 'X';
                             break;
                         }
                         if (emptyGrid[iForGuard -1, jForGuard] == '#')
                         {
                             direction = (direction + 90) % 360;
                             continue;
                         }
                         emptyGrid[iForGuard, jForGuard] = 'X';
                         iForGuard--;
                     }

                     else if (direction == 90)
                     {

                         if (jForGuard + 1 >= cols)
                         {
                             emptyGrid[iForGuard, jForGuard] = 'X';
                             break;
                         }
                         if (emptyGrid[iForGuard, jForGuard+1] == '#')
                         {
                             direction = (direction + 90) % 360;
                             continue;
                         }
                         emptyGrid[iForGuard, jForGuard] = 'X';
                         jForGuard++;

                     }
                     else if (direction == 180)
                     {

                         if (iForGuard + 1 >= rows)
                         {
                             emptyGrid[iForGuard, jForGuard] = 'X';
                             break;
                         }
                         if (emptyGrid[iForGuard+1, jForGuard] == '#')
                         {
                             direction = (direction + 90) % 360;
                             continue;
                         }

                         emptyGrid[iForGuard, jForGuard] = 'X';
                         iForGuard++;
                     }
                     else if (direction == 270)
                     {
                         if (jForGuard - 1 < 0)
                         {
                             emptyGrid[iForGuard, jForGuard] = 'X';
                             break;
                         }

                         if (emptyGrid[iForGuard, jForGuard -1 ] == '#')
                         {
                             direction = (direction + 90) % 360;
                             continue;
                         }
                         emptyGrid[iForGuard, jForGuard] = 'X';
                         jForGuard--;
                     }
                 }

                int count = emptyGrid.Cast<char>().Count(x => x.Equals('X'));
                return count;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd: {ex.Message}");
                return 0;
            }

        }
        public int Part1Rec()
        {
            string filePath = "..\\..\\..\\input_day6_1.txt";
            // string filePath = "..\\..\\..\\text.txt";

            try
            {
                var lines = File.ReadAllLines(filePath);

                int iForGuard = -1;
                int jForGuard = -1;

                char[,] emptyGrid = new char[lines.Length, lines[0].Length];
                for (int i = 0; i < lines.Length; i++)
                {
                    for (int j = 0; j < lines[i].Length; j++)
                    {

                        if (lines[i][j] == '^')
                        {
                            iForGuard = i;
                            jForGuard = j;
                            emptyGrid[i, j] = 'X';
                            continue;
                        }
                        emptyGrid[i, j] = lines[i][j];

                    }
                }
                int rows = emptyGrid.GetLength(0);
                int cols = emptyGrid.GetLength(1);
                int direction = 0;

                XDD(iForGuard + helperI(direction), jForGuard + helperJ(direction), direction, ref emptyGrid);

                int count = emptyGrid.Cast<char>().Count(x => x.Equals('X'));
 

                return count;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd: {ex.Message}");
                return 0;
            }

        }


       

        public bool XDD(int i, int j, int direction, ref char[,] arr)
        {

            if (i >= arr.GetLength(0) || j >= arr.GetLength(1) || i < 0 || j < 0)
                return false;
            if (arr[i, j] == '#') {
                return XDD(i - helperI(direction), j - helperJ(direction), (direction + 90) % 360, ref arr);
            }
            arr[i, j] = 'X';
  
            return XDD(i + helperI(direction), j + helperJ(direction), direction, ref arr);
        }
        
        public int helperI(int i)
        {

            if (i == 0) return -1;
            if (i == 90) return 0;
            if (i == 180) return 1;
            if (i == 270) return 0;
            return 0;

        }
        public int helperJ(int j)
        {
            if (j == 0) return 0;
            if (j == 90) return 1;
            if (j == 180) return 0;
            if (j == 270) return -1;
            return 0;
        }
    }
}
