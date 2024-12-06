using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Day5
    {
        public int Part1()
        {
            string filePath = "..\\..\\..\\input_day5_1.txt";

            try
            {
                var lines = File.ReadAllLines(filePath);

                var text = string.Join("", File.ReadAllLines(filePath));

                string pattern = @"[0-9]+\|[0-9]+";
               
                var rules = FillUpDictionaryWithRules(lines, pattern);

                PrintRules(rules);
                string pattern2 = @"(?:[0-9]+,)+[0-9]+";

                int safeUpdateCounter = 0;
                int totalMiddlePage = 0;

                foreach (var line in lines)
                {
                    var match = Regex.Match(line, pattern2);
              
                    if (!match.Success) continue;

                    List<int> numbers = line
                        .Split(',')               
                        .Select(int.Parse)        
                        .ToList();


                    bool safe = true;
                    for(int i = 0; i < numbers.Count; i++)
                    {
                        var check = true; 
                        for(int j = i+1; j<numbers.Count;j++)
                        {
                            if(rules.TryGetValue(numbers[j], out var list))
                            {
                                if (list.Contains(numbers[i]))
                                {
                                    check = false;
                                    break;
                                }
                            }
                        }
                        if (!check)
                        {
                            safe = false;
                            break;
                        }
                    }
                    if (safe)
                    {
                        safeUpdateCounter++;
                        var middle = numbers[numbers.Count / 2];
                        totalMiddlePage += middle;
                    }
                    
                }


                Console.WriteLine($"Safe updates: {safeUpdateCounter}");
                return totalMiddlePage;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd: {ex.Message}");
                return 0;
            }

        }

        private Dictionary<int, List<int>> FillUpDictionaryWithRules(string[] lines, string pattern)
        {

            Dictionary<int, List<int>> dict = new Dictionary<int, List<int>>();
            foreach (var line in lines)
            {
                var match = Regex.Match(line, pattern);
                if (!match.Success)

                    continue;

                var split = match.Value.Split("|");
                var key = Int32.Parse(split[0]);
                var value = Int32.Parse(split[1]);

                if (dict.TryGetValue(key, out var list))
                {
                    list.Add(value);
                }
                else
                {
                    var newList = new List<int>() { value };
                    dict.TryAdd(key, newList);
                }
            }
            return dict;
        }

        private void PrintRules(Dictionary<int, List<int>> dict)
        {
            foreach (var (key,value) in dict)
            {
                Console.WriteLine($"{key}: ");
                Console.Write("\t");

                foreach(var val in value)
                {
                    Console.Write($"{val}, ");
                }
                Console.WriteLine("");

            }
        }
        public int Part2()
        {
            string filePath = "..\\..\\..\\input_day5_1.txt";


            try
            {
                var lines = File.ReadAllLines(filePath);

                var text = string.Join("", File.ReadAllLines(filePath));

                string pattern = @"[0-9]+\|[0-9]+";

                var rules = FillUpDictionaryWithRules(lines, pattern);

                PrintRules(rules);
                string pattern2 = @"(?:[0-9]+,)+[0-9]+";

                int incorrectUpdatesCorrectedCounter = 0;
                int totalMiddlePage = 0;

                foreach (var line in lines)
                {
                    var match = Regex.Match(line, pattern2);

                    if (!match.Success) continue;

                    List<int> numbers = line
                        .Split(',')
                        .Select(int.Parse)
                        .ToList();


                    var incorrect = false;
                    for (int i = 0; i < numbers.Count; i++)
                    {
                        for (int j = i + 1; j < numbers.Count; j++)
                        {
                            if (rules.TryGetValue(numbers[j], out var list))
                            {
                                if (list.Contains(numbers[i]))
                                {
                                    incorrect = true;
                                    var temp  = numbers[i];
                                    numbers[i] = numbers[j];
                                    numbers[j] = temp;
                                    i--;
                                    break;
                                }
                            }
                        }
                        
                    }
                    if (incorrect)
                    {
                         incorrectUpdatesCorrectedCounter++;
                         var middle = numbers[numbers.Count / 2];
                         totalMiddlePage += middle;
                    }

                }


                Console.WriteLine($"IncorrectUpdatesCorrected: {incorrectUpdatesCorrectedCounter}");
                return totalMiddlePage;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd: {ex.Message}");
                return 0;
            }

        }

    }
}
