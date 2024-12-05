using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Day3
    {

        public int Part1()
        {
            string filePath = "..\\..\\..\\input_day3_1.txt";

            try
            {
                var text = string.Join("", File.ReadAllLines(filePath));
    
                string pattern = @"mul\([0-9]+,[0-9]+\)";

                MatchCollection matches = Regex.Matches(text, pattern);
                var cleanedData = matches.Cast<Match>().Select(x => x.Value);
   
                string pattern2 = @"[0-9]+,[0-9]+";
           
                var total = 0;
                foreach(var x in cleanedData)
                {
                    var match = Regex.Matches(x, pattern2).Select(x =>
                    {
                        var parts = x.Value.Split(",");
                        return new { Left = Int32.Parse(parts[0]), Right = Int32.Parse(parts[1]) };
                    });
                    total += match.ElementAt(0).Left * match.ElementAt(0).Right;
                }
                return total;
                
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd: {ex.Message}");
                return 0;
            }

        }

        public int Part2()
        {
            string filePath = "..\\..\\..\\input_day3_1.txt";

            try
            {
                var text = string.Join("", File.ReadAllLines(filePath));

                string pattern = @"do\(\)|don\'t\(\)|mul\([0-9]+,[0-9]+\)";

                MatchCollection matches = Regex.Matches(text, pattern);
                var cleanedData = matches.Cast<Match>().Select(x => x.Value);

                string pattern2 = @"[0-9]+,[0-9]+";

                int total = 0;
                bool isDo = true;

                foreach (var x in cleanedData)
                {
                    if (x.Equals("do()"))
                    {
                        isDo = true;
                        continue;
                    }
                    if (x.Equals("don't()"))
                    {
                        isDo = false;
                        continue;
                    }
                    if (isDo)
                    {
                        var match = Regex.Matches(x, pattern2).Select(x =>
                        {
                            var parts = x.Value.Split(",");
                            return new { Left = Int32.Parse(parts[0]), Right = Int32.Parse(parts[1]) };
                        });
                        total += match.ElementAt(0).Left * match.ElementAt(0).Right;
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

    }
}
