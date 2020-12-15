using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace AdventOfCode2020
{
    public static class Day15
    {
        public static long One()
        {
            var input = InputParser.GetInputCommaSeperated<int>("day15.txt");
            var startIndex = input.Count;

            for (int i = startIndex; i < 2020; i++)
            {
                var previousNumber = input[i-1];
                if (input.Count(num => num == previousNumber) > 1)
                {
                    var indexes = Enumerable.Range(0, input.Count).Where(i => input[i] == previousNumber).ToList();
                    var secondToLastIndex = indexes.OrderByDescending(o=>o).Skip(1).First();
                    input.Add((i - 1) - secondToLastIndex);
                }
                else
                    input.Add(0);
            }
            return input.Last();
        }

        public static long Two()
        {
            var input = InputParser.GetInputCommaSeperated<int>("day15.txt");
            var startIndex = input.Count;
            Dictionary<int, int> lastKnownIndexOfTable = new Dictionary<int, int>();
            for (int i = 0; i < input.Count - 1; i++)
            {
                lastKnownIndexOfTable.Add(input[i], i);
            }
            var previousNumber = input.Last();
            for (int i = startIndex; i < 30000000; i++)
            {
                int newNumber = 0;
                if (lastKnownIndexOfTable.ContainsKey(previousNumber))
                {
                    newNumber = (i - 1) - lastKnownIndexOfTable[previousNumber];
                }
                lastKnownIndexOfTable[previousNumber] = (i - 1);
                previousNumber = newNumber;

            }
            return previousNumber;
        }
    }
}
