using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Day2
    {
        public int Part1()
        {
            string filePath = "..\\..\\..\\input_day2_1.txt";

            try
            {
                List<int> list;
                var count = 0;
                foreach (string line in File.ReadLines(filePath))
                {
                    list = line.Split(" ").Select(x=>Int32.Parse(x)).ToList();
                    if (IsSafe(list)) count++;
                }
               
                return count;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd: {ex.Message}");
                return 0;
            }

        }

        public bool IsAscending(int a, int b)
        {
            return a<b;
        }

        public bool IsDifferenceInRange(int a, int b)
        {
            var diff = Math.Abs(a - b);
            return diff <= 3 && diff >= 1;
        }


        public int Part2()
            //weeell i have a gut feeling that it is not very efficent, but it is what is is 
            //and suprisingly it gives me the correct answer soo I'll just take it
            //maybe will update it in the future if i can come up with something like more respectable lol
        {
            string filePath = "..\\..\\..\\input_day2_1.txt";

            try
            {
                List<int> list;
                var count = 0;
                foreach (string line in File.ReadLines(filePath))
                {
                    list = line.Split(" ")
                        .Select(x => Int32.Parse(x)).ToList();

                    if (IsSafe(list)) count++;
                    else
                    {
                        for (int i = 0; i < list.Count; i++)
                        {
                            var newList = new List<int>();
                            for (int j = 0; j < list.Count; j++)
                            {
                                if (j != i) 
                                {
                                    newList.Add(list[j]);
                                }
                            }

                           if(IsSafe(newList))
                           {
                                count++;
                                break;
                           } 
                        }
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

        public bool IsSafe(List<int> list)
        {

            if (list.Count < 2) return false;

            bool ascending = false;
            if (IsAscending(list.ElementAt(0), list.ElementAt(1)))
            {
                ascending = true;
            }

            for (int i = 0; i < list.Count - 1; i++)
            {
                var temp1 = list.ElementAt(i);
                var temp2 = list.ElementAt(i + 1);
                if (!IsDifferenceInRange(temp1, temp2)
                    || (ascending && !(temp1 < temp2))
                    || (!ascending && !(temp1 > temp2)))
                {
                    return false; 
                }
            }
            return true;

        }
    }
}
