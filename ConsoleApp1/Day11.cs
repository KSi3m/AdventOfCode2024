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
    }
}
