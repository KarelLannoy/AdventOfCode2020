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
            return GetNumberSpokenAt(2020);
        }

        public static long Two()
        {
            return GetNumberSpokenAt(30000000);
        }

        private static long GetNumberSpokenAt(int number)
        {
            var input = InputParser.GetInputCommaSeperated<int>("day15.txt");
            var startIndex = input.Count;
            Dictionary<int, int> lastKnownIndexOfTable = new Dictionary<int, int>();
            for (int i = 0; i < input.Count - 1; i++)
            {
                lastKnownIndexOfTable.Add(input[i], i);
            }
            var previousNumber = input.Last();
            for (int i = startIndex; i < number; i++)
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
