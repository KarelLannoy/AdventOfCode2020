using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode2020
{
    public static class Day05
    {
        public static string One()
        {
            var inputText = InputParser.GetInputText("day05.txt");
            inputText = inputText.Replace("F", "0").Replace("B", "1").Replace("R", "1").Replace("L","0");
            var inputLines = inputText.Split(Environment.NewLine).ToList();
            inputLines.Sort();
            var result = inputLines.Last();
            return Convert.ToInt32(result, 2).ToString();
        }

        public static string Two()
        {
            var inputText = InputParser.GetInputText("day05.txt");
            inputText = inputText.Replace("F", "0").Replace("B", "1").Replace("R", "1").Replace("L", "0");
            var inputLines = inputText.Split(Environment.NewLine).ToList();
            inputLines.Sort();

            List<int> seatIds = new List<int>();
            foreach (var seat in inputLines)
            {
                seatIds.Add(Convert.ToInt32(seat,2));
            }

            for (int i = seatIds.First(); i < seatIds.Last(); i++)
            {
                if (!seatIds.Contains(i))
                {
                    return i.ToString();
                }
            }

            throw new Exception("Bad Solution");
        }
    }
}
