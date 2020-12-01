using System;
using System.Collections.Generic;

namespace AdventOfCode2020
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(Day01_1());
            Console.WriteLine(Day01_2());
            Console.ReadLine();
        }

        private static int Day01_1()
        {
            var input = InputParser.GetInputLines("day01.txt");
            var intList = new List<int>();
            input.ForEach(inputItem => intList.Add(int.Parse(inputItem)));

            for (int i = 0; i < intList.Count; i++)
            {
                var item1 = intList[i];
                for (int y = 0; y < intList.Count; y++)
                {
                    if (i == y)
                    {
                        continue;
                    }
                    var item2 = intList[y];
                    if (item1 + item2 == 2020)
                    {
                        return item1 * item2;
                    }
                }
            }

            throw new Exception("No Solution");
        }

        private static int Day01_2()
        {
            var input = InputParser.GetInputLines("day01.txt");
            var intList = new List<int>();
            input.ForEach(inputItem => intList.Add(int.Parse(inputItem)));

            for (int i = 0; i < intList.Count; i++)
            {
                var item1 = intList[i];
                for (int y = 0; y < intList.Count; y++)
                {
                    if (i == y)
                    {
                        continue;
                    }
                    var item2 = intList[y];
                    for (int z = 0; z < intList.Count; z++)
                    {
                        if (i == z || y == z)
                        {
                            continue;
                        }
                        var item3 = intList[z];
                        if (item1 + item2 + item3 == 2020)
                        {
                            return item1 * item2 * item3;
                        }
                    }
                }
            }

            throw new Exception("No Solution");
        }
    }
}
