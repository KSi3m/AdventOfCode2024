using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Channels;
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

                var results = new List<long>();
                var numbers = new List<List<int>>();
            
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
                    count = Part1_Rec(result, numbers.ElementAt(i), 1, numbers.ElementAt(i).Count - 1, numbers.ElementAt(i)[0]);
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

        public long Part1_Rec(long result, List<int> numbers, int i, int maxI, long count1 ) 
        { 
  
            if(i> maxI)
            {
                return count1;
            }
            if (count1 > result) return 0;
            if (result == Part1_Rec(result, numbers, i + 1, maxI, count1 + numbers[i]) ||
                result == Part1_Rec(result, numbers, i + 1, maxI, count1 * numbers[i]))
            {
                return result;
            }
            return 0;
        }

       
        public long Part2_Version1()
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
                for (int i = 0; i < results.Count; i++)
                {
                    long result = results.ElementAt(i);
                    long count = Part2_Rec(result, numbers.ElementAt(i), 1, numbers.ElementAt(i).Count - 1, numbers.ElementAt(i)[0]);
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

        public long Part2_Rec(long result, List<int> numbers, int i, int maxI, long count1)
        {
            if (i > maxI)
            {
                return count1;
            }
            if (count1 > result) return 0;
            if (result == Part2_Rec(result, numbers, i + 1, maxI, count1 + numbers[i]) ||
                result == Part2_Rec(result, numbers, i + 1, maxI, count1 * numbers[i]) ||
                result == Part2_Rec(result, numbers, i + 1, maxI, long.Parse($"{count1}{numbers[i]}")))
            {
                return result;
            }
            return 0;
        }

        public long Part2_Version2()
        {
            string filePath = "..\\..\\..\\input_day7_1.txt";
            //  string filePath = "..\\..\\..\\text.txt";

            try
            {
                var lines = File.ReadAllLines(filePath);

                var results = new List<long>();
                var numbers = new List<List<int>>();

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

                for (int i = 0; i < results.Count; i++)
                {
                    var newList = new List<string>();
                    long result = results.ElementAt(i);
                    Part2_Version2_RecStrings(numbers.ElementAt(i), 1, numbers.ElementAt(i).Count - 1, $"{numbers.ElementAt(i)[0]}", newList);
                    var newListOfResults = new HashSet<long>();
                    foreach (var eq in newList)
                    {
                        string temp = "";
                        char lastOperator = '+';
                        long currentValue = 0;
                        for (int j = 0; j < eq.Length; j++)
                        {
                            if (char.IsDigit(eq[j]))
                            {
                                temp += eq[j];
                            }
                            if (!char.IsDigit(eq[j]) || j == eq.Length - 1)
                            {
                                if (lastOperator == '+')
                                {
                                    currentValue += long.Parse(temp);
                                }
                                if (lastOperator == '*')
                                {
                                    currentValue *= long.Parse(temp);
                                }
                                if (lastOperator == '|')
                                {
                                    currentValue = long.Parse($"{currentValue}{temp}");
                                }
                                lastOperator = eq[j];
                                temp = "";
                            }

                        }
                        if (results[i] == currentValue) newListOfResults.Add(currentValue);
                    }

                    if (newListOfResults.Contains(results[i]))
                    {
                        total += results[i];
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

        public string Part2_Version2_RecStrings(List<int> numbers, int i, int maxI, string count1, List<string> equations)
        {

            if (i > maxI)
            {
                return count1;
            }
            equations.Add(Part2_Version2_RecStrings(numbers, i + 1, maxI, $"{count1}+{numbers[i]}", equations));
            equations.Add(Part2_Version2_RecStrings(numbers, i + 1, maxI, $"{count1}*{numbers[i]}", equations));
            equations.Add(Part2_Version2_RecStrings(numbers, i + 1, maxI, $"{count1}|{numbers[i]}", equations));

            return "";
        }
    }
}
