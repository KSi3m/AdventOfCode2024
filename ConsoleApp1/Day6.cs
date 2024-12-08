using System;
using System.Collections.Generic;
using System.Data;
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

                while (iForGuard < rows && jForGuard < cols)
                {
                    if (direction == 0)
                    {

                        if (iForGuard - 1 < 0)
                        {
                            emptyGrid[iForGuard, jForGuard] = 'X';
                            break;
                        }
                        if (emptyGrid[iForGuard - 1, jForGuard] == '#')
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
                        if (emptyGrid[iForGuard, jForGuard + 1] == '#')
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
                        if (emptyGrid[iForGuard + 1, jForGuard] == '#')
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

                        if (emptyGrid[iForGuard, jForGuard - 1] == '#')
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

            if (i == 0) return -1; //up
            if (i == 90) return 0; //right
            if (i == 180) return 1; //down
            if (i == 270) return 0; //left
            return 0;

        }
        public int helperJ(int j)
        {
            if (j == 0) return 0;  //up
            if (j == 90) return 1; //right
            if (j == 180) return 0; //down
            if (j == 270) return -1; //left
            return 0;
        }

       /* public bool XDD2(int i, int j, int direction, ref char[,] arr, ref List<(int,int)> path)
        {

            if (i >= arr.GetLength(0) || j >= arr.GetLength(1) || i < 0 || j < 0)
                return false;
            if (arr[i, j] == '#')
            {
                return XDD2(i - helperI(direction), j - helperJ(direction), (direction + 90) % 360, ref arr,ref path);
            }
            arr[i, j] = 'X';
            AddToList(path, (i, j));

            return XDD2(i + helperI(direction), j + helperJ(direction), direction, ref arr,ref path);
        }
        public bool XDD3(int i, int j, int direction, ref char[,] arr)
        {

            if (i >= arr.GetLength(0) || j >= arr.GetLength(1) || i < 0 || j < 0)
                return false;
            if (arr[i, j] == '#')
            {
                return XDD3(i - helperI(direction), j - helperJ(direction), (direction + 90) % 360, ref arr);
            }
            arr[i, j] = 'X';


            return XDD3(i + helperI(direction), j + helperJ(direction), direction, ref arr);
        }

        public int Part2Rec()
        {
            //string filePath = "..\\..\\..\\input_day6_1.txt";
            string filePath = "..\\..\\..\\text.txt";

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
                List<(int, int)> path = new List<(int, int)>();
                XDD2(iForGuard + helperI(direction), jForGuard + helperJ(direction), direction, ref emptyGrid, ref path);
                foreach(var x in path.Skip(1))
                {
                    emptyGrid = GetInputArray(lines, out iForGuard, out jForGuard);
                    emptyGrid[x.Item1, x.Item2] = 'O';
                    //if xdd3 count++;

                }
                int count = emptyGrid.Cast<char>().Count(x => x.Equals('X'));

                Console.WriteLine(path.Count);
                foreach (var item in path)
                {
                    Console.WriteLine(item);
                }
                return count;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd: {ex.Message}");
                return 0;
            }

        }


        */
        public int Part2()
        {
            string filePath = "..\\..\\..\\input_day6_1.txt";
            //string filePath = "..\\..\\..\\text.txt";

            try
            {
                var lines = File.ReadAllLines(filePath);

                int iPositionOfGuard = -1;
                int jPositionOfGuard = -1;

                char[,] emptyGrid = GetInputArray(lines, out iPositionOfGuard, out jPositionOfGuard);

                int rows = emptyGrid.GetLength(0);
                int cols = emptyGrid.GetLength(1);
                int direction = 0;

                List<(int, int)> pathOfVisitedPostions = new List<(int, int)>();

                while (iPositionOfGuard < rows && jPositionOfGuard < cols)
                {
                    if (direction == 0)
                    {
                        if (iPositionOfGuard - 1 < 0)
                        {
                            emptyGrid[iPositionOfGuard, jPositionOfGuard] = 'X';
                            AddToList(pathOfVisitedPostions, (iPositionOfGuard,jPositionOfGuard));
                            break;
                        }
                        if (emptyGrid[iPositionOfGuard - 1, jPositionOfGuard] == '#')
                        {
                            direction = (direction + 90) % 360;
                            continue;
                        }
                        AddToList(pathOfVisitedPostions, (iPositionOfGuard, jPositionOfGuard));
                        emptyGrid[iPositionOfGuard, jPositionOfGuard] = 'X';
                        iPositionOfGuard--;
                    }

                    else if (direction == 90)
                    {

                        if (jPositionOfGuard + 1 >= cols)
                        {
                            emptyGrid[iPositionOfGuard, jPositionOfGuard] = 'X';
                            AddToList(pathOfVisitedPostions, (iPositionOfGuard, jPositionOfGuard));
                            break;
                        }
                        if (emptyGrid[iPositionOfGuard, jPositionOfGuard + 1] == '#')
                        {
                            direction = (direction + 90) % 360;
                            continue;
                        }
                        AddToList(pathOfVisitedPostions, (iPositionOfGuard, jPositionOfGuard));
                        emptyGrid[iPositionOfGuard, jPositionOfGuard] = 'X';
                        jPositionOfGuard++;

                    }
                    else if (direction == 180)
                    {

                        if (iPositionOfGuard + 1 >= rows)
                        {
                            emptyGrid[iPositionOfGuard, jPositionOfGuard] = 'X';
                            AddToList(pathOfVisitedPostions, (iPositionOfGuard, jPositionOfGuard));
                            break;
                        }
                        if (emptyGrid[iPositionOfGuard + 1, jPositionOfGuard] == '#')
                        {
                            direction = (direction + 90) % 360;
                            continue;
                        }
                        AddToList(pathOfVisitedPostions, (iPositionOfGuard, jPositionOfGuard));
                        emptyGrid[iPositionOfGuard, jPositionOfGuard] = 'X';
                        iPositionOfGuard++;
                    }
                    else if (direction == 270)
                    {
                        if (jPositionOfGuard - 1 < 0)
                        {
                            emptyGrid[iPositionOfGuard, jPositionOfGuard] = 'X';
                            AddToList(pathOfVisitedPostions, (iPositionOfGuard, jPositionOfGuard));
                            break;
                        }

                        if (emptyGrid[iPositionOfGuard, jPositionOfGuard - 1] == '#')
                        {
                            direction = (direction + 90) % 360;
                            continue;
                        }
                        AddToList(pathOfVisitedPostions, (iPositionOfGuard, jPositionOfGuard));
                        emptyGrid[iPositionOfGuard, jPositionOfGuard] = 'X';
                        jPositionOfGuard--;
                    }
                }

                int count = emptyGrid.Cast<char>().Count(x => x.Equals('X'));

                int countOfCycles = 0;
                int breaker = pathOfVisitedPostions.Count * 5;
                Console.WriteLine("GO");
                foreach (var x in pathOfVisitedPostions.Skip(1))
                {
                    direction = 0;
                    emptyGrid  = GetInputArray(lines, out iPositionOfGuard, out jPositionOfGuard);
                    emptyGrid[x.Item1, x.Item2] = 'O';


                    int countStepsInPath = 0;
                    int cycleDetector = 0; //if 2 break;
                    int directionAtFirstOccurenceOfO = -1;
                    while (iPositionOfGuard < rows && jPositionOfGuard < cols)
                    {
                        countStepsInPath++;
                        if (countStepsInPath > breaker)
                        {
                            countOfCycles++;
                            break;
                        }
                        if (iPositionOfGuard + helperI(direction) < 0 
                            || iPositionOfGuard + helperI(direction) >= rows
                            || jPositionOfGuard + helperJ(direction) < 0
                            || jPositionOfGuard + helperJ(direction) >= cols)
                        {
                            emptyGrid[iPositionOfGuard, jPositionOfGuard] = 'X';
                            break;
                        }
                        if (emptyGrid[iPositionOfGuard + helperI(direction), jPositionOfGuard + helperJ(direction)] == '#')
                        {
                            direction = (direction + 90) % 360;
                            continue;
                        }
                        if (emptyGrid[iPositionOfGuard + helperI(direction), jPositionOfGuard + helperJ(direction)] == 'O')
                        {
                            if (directionAtFirstOccurenceOfO == -1) directionAtFirstOccurenceOfO = direction;

                            if (direction == directionAtFirstOccurenceOfO) cycleDetector++;
                            if (cycleDetector == 2)
                            {
                                countOfCycles++;
                                break;
                            }
                            direction = (direction + 90) % 360;
                            continue;
                        }
                        emptyGrid[iPositionOfGuard, jPositionOfGuard] = 'X';
                        iPositionOfGuard += helperI(direction);
                        jPositionOfGuard += helperJ(direction);

                    }

                }


                return countOfCycles;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd: {ex.Message}");
                return 0;
            }

        }
        public void print(char[,] kek)
        {
            Console.WriteLine("  0123456789");
            for (int i = 0; i < kek.GetLength(0); i++)
            {
                Console.Write(i + " ");
                for (int j = 0; j < kek.GetLength(1); j++)
                {
                    Console.Write(kek[i, j]);
                }
                Console.WriteLine("");
            }
        }

        public char[,] GetInputArray(string[] lines, out int iForGuard, out int jForGuard)
        {
            iForGuard = -1;
            jForGuard = -1;
            int rows = lines.Length;
            int cols = lines[0].Length;
            var emptyGrid = new char[rows,cols];
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
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
            return emptyGrid;
        }
        public void AddToList(List<(int,int)> list, (int iForGuard, int jForGuard) tuple)
        {
            if (!list.Contains(tuple))
                list.Add(tuple);
        }
    }
}
