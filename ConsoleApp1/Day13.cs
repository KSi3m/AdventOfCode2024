using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ConsoleApp1
{
    internal class Day13
    {
        public double Part1()
        {
            string filePath = "..\\..\\..\\input_day13_1.txt";
            //string filePath = "..\\..\\..\\text.txt";

            try
            {
                var text = string.Join("", File.ReadAllLines(filePath));

                string pattern = @"X[+=](\d+),\s*Y[+=](\d+)";
                Regex regex = new Regex(pattern);


                List<(double X, double Y)> tuples = new List<(double X, double Y)>();
                int count = 0;
                List<List<(double X, double Y)>> full = new List<List<(double X, double Y)>>();
                foreach (Match match in regex.Matches(text))
                {
                    double x = double.Parse(match.Groups[1].Value);
                    double y = double.Parse(match.Groups[2].Value);
                    tuples.Add((x, y));
                    count++;
                    if(count == 3) {
                        full.Add(new List<(double X, double Y)>(tuples));
                        count = 0;
                        tuples.Clear();
                    }
                }

                double total = 0;

                foreach (var tuple in full)
                {
 
                    double denominator = ((tuple[0].X * tuple[1].Y) - (tuple[1].X * tuple[0].Y));
                    if (denominator == 0)
                    {
                        continue;
                    }

                    double A = ((tuple[2].X * tuple[1].Y) - (tuple[1].X * tuple[2].Y))
                        / denominator;
                    double B = (tuple[2].Y - (A * tuple[0].Y)) / tuple[1].Y;
                    if (A == (int)A && B == (int)B)
                    {
                        total += (A * 3) + B;
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

        public double Part2()
        {
            string filePath = "..\\..\\..\\input_day13_1.txt";
           //string filePath = "..\\..\\..\\text.txt";

            try
            {
                var text = string.Join("", File.ReadAllLines(filePath));

                string pattern = @"X[+=](\d+),\s*Y[+=](\d+)";
                Regex regex = new Regex(pattern);


                List<(double X, double Y)> tuples = new List<(double X, double Y)>();
                int count = 0;
                List<List<(double X, double Y)>> full = new List<List<(double X, double Y)>>();
                foreach (Match match in regex.Matches(text))
                {
                    double x,y;

                    if (count == 2){
                         x = double.Parse(match.Groups[1].Value);
                        x += 10000000000000;
                         y = double.Parse(match.Groups[2].Value);
                        y += 10000000000000;
                    }
                    else
                    {
                         x = double.Parse(match.Groups[1].Value);
                         y = double.Parse(match.Groups[2].Value);
                    }
                    tuples.Add((x, y));
                    count++;
                    
                    if (count == 3)
                    {
                        full.Add(new List<(double X, double Y)>(tuples));
                        count = 0;
                        tuples.Clear();
                    }
                }

                double total = 0;

                foreach (var tuple in full)
                {
                    
                    double denominator = ((tuple[0].X * tuple[1].Y) - (tuple[1].X * tuple[0].Y));
                    if (denominator == 0)
                    {
                        continue;
                    }


                    double A = ((tuple[2].X * tuple[1].Y) - (tuple[1].X * tuple[2].Y))
                        / denominator;
                    double B = (tuple[2].Y - (A * tuple[0].Y)) / tuple[1].Y;
                    if (A == (long)A && B == (long)B)
                    {
                        total += (A * 3) + B;
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
