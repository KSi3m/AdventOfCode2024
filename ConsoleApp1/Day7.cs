using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Day7
    {
        public long Part1()
        {
            string filePath = "..\\..\\..\\input_day7_1.txt";
            //string filePath = "..\\..\\..\\text.txt";

            try
            {
                var lines = File.ReadAllLines(filePath);

                var text = string.Join("", File.ReadAllLines(filePath));
                var results = new List<long>();
                var numbers = new List<List<int>>();
                /*foreach (var item in lines)
                {
                    Console.WriteLine(item);
                }*/
                foreach (var line in lines)
                {
          
                    string[] parts = line.Split(':');
                    long leftNumber = long.Parse(parts[0].Trim());
                    results.Add(leftNumber);

            
                    string[] rightNumbers = parts[1].Trim().Split(' ');
                    var newList = new List<int>();
                    foreach (var number in rightNumbers)
                    {
                        newList.Add(int.Parse(number));
                    }
                    numbers.Add(newList);
                }             
                long total = 0;
                for (int i = 0; i<results.Count;i++)
                {
                    long result = results.ElementAt(i);
                    long count = -1;
                    count = PartX(result, numbers.ElementAt(i), 1, numbers.ElementAt(i).Count - 1, numbers.ElementAt(i)[0]);
                    total += count;
                }
                return total;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd: {ex.Message}");
                return 0;
            }

        }

        public long PartX(long result, List<int> numbers, int i, int maxI, long count1) 
        { 
  
            if(i> maxI)
            {
                return count1;
            }
            if (count1 > result) return 0;
            if (result == PartX(result, numbers, i + 1, maxI, count1 + numbers[i]) ||
                result == PartX(result, numbers, i + 1, maxI, count1 * numbers[i]))
            {
                return result;
            }
            return 0;
        }

        
    }
}
