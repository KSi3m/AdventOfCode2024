using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Day1
    {
        public int Part1()
        {

            string filePath = "..\\..\\..\\input_day1_1.txt";

            try
            {
                List<int> list1 = new List<int>();
                List<int> list2 = new List<int>();

                foreach (string line in File.ReadLines(filePath))
                {

                    var first = line.Substring(0, line.IndexOf(" "));
                    var second = line.Substring(line.LastIndexOf(" ") + 1);
                    list1.Add(Int32.Parse(first));
                    list2.Add(Int32.Parse(second));
                }
                list1.Sort();
                list2.Sort();

                if (list1.Count == list2.Count)
                {
                    var total = 0;
                    for (int i = 0; i < list1.Count; i++)
                    {
                        total += Math.Abs(list1.ElementAt(i) - list2.ElementAt(i));

                    }
                    return total;
                }
                return 0;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd: {ex.Message}");
                return 0;
            }
        }


        public long Part2()
        {

            string filePath = "..\\..\\..\\input_day1_1.txt";

            try
            {
                List<int> list1 = new List<int>();
                var dict2 = new Dictionary<int, int>();
     
                foreach (string line in File.ReadLines(filePath))
                {

                    var first = line.Substring(0, line.IndexOf(" "));
                    var second = line.Substring(line.LastIndexOf(" ") + 1);
                    var numberForFirstList = Int32.Parse(first);
                    var numberForSecondList = Int32.Parse(second);
                    list1.Add(numberForFirstList);

                    if(!dict2.TryAdd(numberForSecondList, 1))
                    {
                        dict2[numberForSecondList]++;
                    }
                }
        
                dict2 = dict2.OrderBy(x=>x.Value).ToDictionary();

                //if(list1.Distinct().Count() == list1.Count)
                long total = 0;
                foreach (var item in list1)
                {
                    if (dict2.TryGetValue(item, out int value))
                        total += (long)item * value;
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
