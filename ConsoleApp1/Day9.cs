using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ConsoleApp1
{
    internal class Day9
    {
        public long Part1()
        {
            string filePath = "..\\..\\..\\input_day9_1.txt";
            // string filePath = "..\\..\\..\\text.txt";

            try
            {
                var lines = File.ReadLines(filePath);
                //var text = string.Join("\n", lines);
                foreach (var item in lines)
                {
                    Console.WriteLine(item);
                }
                Console.WriteLine(lines.ElementAt(0));
    
                string[] arr = new string[lines.ElementAt(0).Length * 6];
                for (int i = 0, currentID = i, arrID = i; i < lines.ElementAt(0).Length; i++)
                {
                    var xd = lines.ElementAt(0)[i];
                    if (i % 2 == 0)
                    {
                    
                        for(int j = 0; j < int.Parse(lines.ElementAt(0)[i].ToString()); j++)
                        {
                            arr[arrID++] = currentID.ToString();
                        }
                        currentID++;
                    }
                    else
                    {
                        for (int j = 0; j < int.Parse(lines.ElementAt(0)[i].ToString()); j++)
                        {
                            arr[arrID++] = ".";
                        }
                    }
                }

                string[] nonEmptyArray = arr.Where(item => item != "" && item != null).ToArray();
       

                foreach (var item in nonEmptyArray)
                {
                    Console.WriteLine($" {item}");
                    
                }                //Console.WriteLine(temp);,


       
                //char[] chars = temp.ToCharArray();
                int indexOfLeftMostFreeSpace = -1;
                for (int i = nonEmptyArray.Length - 1; i > 0; i--)
                {
                    //Console.WriteLine(string.Join("", chars));
                    if (nonEmptyArray[i] == ".")
                    {
                        nonEmptyArray[i] = "x";
                        continue;
                    }

                    indexOfLeftMostFreeSpace = LeftmostFreeSpaceIndex(nonEmptyArray);
                    if (indexOfLeftMostFreeSpace == -1) break;

                    nonEmptyArray[indexOfLeftMostFreeSpace] = nonEmptyArray[i];
                    nonEmptyArray[i] = "x";
                }
                long checksum = 0;
                //Console.WriteLine(string.Join("", chars));
                for (int i=0;i< nonEmptyArray.Length;i++)
                {

                    if (nonEmptyArray[i] == "x") break;
                    if (nonEmptyArray[i] == ".") continue;

                        checksum += i * int.Parse(nonEmptyArray[i]);
                    
                    //Console.WriteLine((chars[i] - '0'));
                }

                return checksum;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd: {ex.Message}");
                return 0;
            }

        }

        public int LeftmostFreeSpaceIndex(string[] text)
        {
            for (int i = 0;i<text.Length;i++)
            {
                if (text[i] == ".") { return i; }
            }
            return -1;
        }

        public long Part2()
        {
            string filePath = "..\\..\\..\\input_day9_1.txt";
            //string filePath = "..\\..\\..\\text.txt";

            try
            {
                var lines = File.ReadLines(filePath);
                //var text = string.Join("\n", lines);
                foreach (var item in lines)
                {
                    Console.WriteLine(item);
                }
                //Console.WriteLine(lines.ElementAt(0));

                string[] arr = new string[lines.ElementAt(0).Length * 6];
                for (int i = 0, currentID = i, arrID = i; i < lines.ElementAt(0).Length; i++)
                {
                    //Console.WriteLine(lines.ElementAt(0)[i]);
                    var xd = lines.ElementAt(0)[i];
                    if (i % 2 == 0)
                    {
                        //stringBuilder.Append(new string((char)(count + '0'), int.Parse(lines.ElementAt(0)[i].ToString())));
                        for (int j = 0; j < int.Parse(lines.ElementAt(0)[i].ToString()); j++)
                        {
                            arr[arrID++] = currentID.ToString();
                        }
                        currentID++;
                    }
                    else
                    {
                        for (int j = 0; j < int.Parse(lines.ElementAt(0)[i].ToString()); j++)
                        {
                            arr[arrID++] = ".";
                        }
                        // stringBuilder.Append(new string('.', int.Parse(lines.ElementAt(0)[i].ToString())));

                    }
                }

                string[] nonEmptyArray = arr.Where(item => item != "" && item != null).ToArray();


                /*foreach (var item in nonEmptyArray)
                {
                    Console.WriteLine($" {item}");

                }      */          //Console.WriteLine(temp);,


                List<int> newListOfIDIndexes = new List<int>() { nonEmptyArray.Length - 1 };
                string? currentID2 = nonEmptyArray[nonEmptyArray.Length - 1];
                List<int> indexesToIgnore = new List<int>();
                for (int i = nonEmptyArray.Length - 2; i > 0; i--)
                {
                    if (indexesToIgnore.Contains(i)) continue;

                    if (nonEmptyArray[i] != "." && currentID2 == null)
                    {
                        currentID2 = nonEmptyArray[i];
                        newListOfIDIndexes.Add(i);
                        continue;
                    }

                    if (nonEmptyArray[i] == currentID2)
                    {
                        newListOfIDIndexes.Add(i);
                        continue;
                    }

                    if (nonEmptyArray[i] == "." && !newListOfIDIndexes.Any())
                    {
                        nonEmptyArray[i] = "x";
                        continue;
                    }

                    if (nonEmptyArray[i] != currentID2)
                    {
                        var freeSpaceIndexes = LeftmostFreeSpaceIndexes(nonEmptyArray, newListOfIDIndexes.Count);
                        if (newListOfIDIndexes.Count <= freeSpaceIndexes.Count)
                        {
                            for(int x =0;x< newListOfIDIndexes.Count;x++)
                            {
                                nonEmptyArray[freeSpaceIndexes[x]] = nonEmptyArray[newListOfIDIndexes[x]];
                                nonEmptyArray[newListOfIDIndexes[x]] = "x";
                                indexesToIgnore.Add(freeSpaceIndexes[x]);
                            }

                        }
                        newListOfIDIndexes.Clear();
                        if (nonEmptyArray[i] != ".")
                        {
                            currentID2 = nonEmptyArray[i];
                            newListOfIDIndexes.Add(i);
                        }
                        else
                        {
                            currentID2 = null;
                        }

                    }
                   
                  
                }
                long checksum = 0;
                indexesToIgnore.ForEach(x=>Console.Write($"{x},"));
                Console.WriteLine("");
                foreach (var item in nonEmptyArray)
                {
                    Console.Write(item);
                }                //Console.WriteLine(string.Join("", chars));
                Console.WriteLine("");
                for (int i = 0; i < nonEmptyArray.Length; i++)
                 {

                     if (nonEmptyArray[i] == "x") continue;
                     if (nonEmptyArray[i] == ".") continue;

                     checksum += i * int.Parse(nonEmptyArray[i]);

                     //Console.WriteLine((chars[i] - '0'));
                 }

                return checksum;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd: {ex.Message}");
                return 0;
            }

        }

        public List<int> LeftmostFreeSpaceIndexes(string[] text, int spaceNeeded)
        {
            List<int> indexes = new List<int>();
            for (int i = 0; i < text.Length; i++)
            {
                if (text[i] != "." && !indexes.Any()) continue;
                if (text[i] != "." && indexes.Any())
                {
                    if(indexes.Count >= spaceNeeded)
                         return indexes;
                    indexes.Clear();
                }
                if (text[i] == "." ) {
                    indexes.Add(i); 
                }
            }
            return indexes;
        }
    }
}
