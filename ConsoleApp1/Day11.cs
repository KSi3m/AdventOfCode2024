using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Day11
    {
        public int Part1()
        {
            string filePath = "..\\..\\..\\input_day11_1.txt";
            //string filePath = "..\\..\\..\\text.txt";

            try
            {
          
                var lines = File.ReadAllLines(filePath);

                var xd = string.Join("",lines).Split(" ").ToList();
            
                List<string> newList = new List<string>();
                for(int i =0;i<25;i++)
                {
                    for(int j = 0; j<xd.Count;j++)
                    {
                        var number = long.Parse(xd[j]);
                        if (number == 0)
                        {
                            newList.Add("1");
                            continue;
                        }
                       else if (xd[j].Length % 2 == 0)
                        {
                            newList.Add(xd[j].Substring(0, xd[j].Length/2));
                            var second = long.Parse(xd[j].Substring(xd[j].Length / 2));
                            newList.Add($"{second}");
                        }
                        else
                        {
                            newList.Add($"{2024*number}");
                            continue;
                        }
                    }
                    xd = new List<string>(newList);
                    newList.Clear();
                }

                return xd.Count;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd: {ex.Message}");
                return 0;
            }

        }

        public long Part2()
        {
            string filePath = "..\\..\\..\\input_day11_1.txt";
            //string filePath = "..\\..\\..\\text.txt";

            try
            {
                var lines = File.ReadAllLines(filePath);

                var xd = string.Join("", lines).Split(" ").ToList();
                Dictionary<(string, int), long> memo = new Dictionary<(string, int), long>();
                Dictionary<string, string[]> splitMemo = new Dictionary<string, string[]>();
                List<string> newList = new List<string>();
                long count = 0;
                for (int j = 0;  j < xd.Count; j++)
                {
                    count += Rec(xd[j], 0,74 , memo, splitMemo);
                }
                   ;
                

                return count;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd: {ex.Message}");
                return 0;
            }

        }
        public long Rec(string number, int i, int maxI, Dictionary<(string, int), long> memo, Dictionary<string, string[]> splitMemo)
        {
            if (i > maxI) return 1; 

          
            if (memo.TryGetValue((number, i), out long cachedCount))
                return cachedCount;

            long count = 0;

            if (splitMemo.TryGetValue(number, out string[] splits))
            {
                if (splits.Length == 1)
                {
                    count += Rec(splits[0], i + 1, maxI, memo, splitMemo);
                }
                else
                {
                    count += Rec(splits[0], i + 1, maxI, memo, splitMemo);
                    count += Rec(splits[1], i + 1, maxI, memo, splitMemo);
                }
            }
            else
            {
                if (number == "0")
                {
                    splitMemo[number] = new string[] { "1" };
                    count += Rec("1", i + 1, maxI, memo, splitMemo);
                }
                else if (number.Length % 2 == 0)
                {
                    var first = number.Substring(0, number.Length / 2);
                    var second = long.Parse(number.Substring(number.Length / 2)).ToString();
                    splitMemo[number] = new string[] { first, second };
                    count += Rec(first, i + 1, maxI, memo, splitMemo);
                    count += Rec(second, i + 1, maxI, memo, splitMemo);
                }
                else
                {
                    var result = $"{2024 * long.Parse(number)}";
                    splitMemo[number] = new string[] { result };
                    count += Rec(result, i + 1, maxI, memo, splitMemo);
                }
            }

            memo[(number,i)] = count;
            return count;
        }
    }
}
