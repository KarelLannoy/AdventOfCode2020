using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace AdventOfCode2020
{
    public static class Day03
    {
        public static string One()
        {
            var inputs = InputParser.GetInputLines("day03.txt");

            var numberOfHorizontalStepsNeeded = (inputs.Count * 3);
            var numberOfReplications = Math.Ceiling((double)numberOfHorizontalStepsNeeded / (double)inputs[0].Count());
            var treeList = BuildTreeList(inputs, numberOfReplications);
            
            var treeCount = 0;
            var x = 0;
            var y = 0;

            for (int i = 0; i < inputs.Count; i++)
            {
                if (treeList.Contains(new Point(x, y)))
                {
                    treeCount++;
                }
                x += 3;
                y += 1;
            }

            return treeCount.ToString();
            throw new Exception("Bad solution");
        }

        public static string Two()
        {
            var inputs = InputParser.GetInputLines("day03.txt");

            List<Tuple<int, int>> slopes = new List<Tuple<int, int>> { new Tuple<int, int>(1, 1), new Tuple<int, int>(3, 1), new Tuple<int, int>(5, 1), new Tuple<int, int>(7, 1), new Tuple<int, int>(1, 2) };

            var numberOfHorizontalStepsNeeded = (inputs.Count * slopes.Max(t=>t.Item1));
            var numberOfReplications = Math.Ceiling((double)numberOfHorizontalStepsNeeded / (double)inputs[0].Count());
            var treeList = BuildTreeList(inputs, numberOfReplications);
            double totalTreeCount = 1;
            
            foreach (var slope in slopes)
            {
                var treeCount = 0;
                var x = 0;
                var y = 0;

                for (int i = 0; i < inputs.Count; i++)
                {
                    if (treeList.Contains(new Point(x, y)))
                    {
                        treeCount++;
                    }
                    x += slope.Item1;
                    y += slope.Item2;
                }
                totalTreeCount *= treeCount;
            }
            
            return totalTreeCount.ToString();
            throw new Exception("Bad solution");
        }

        private static List<Point> BuildTreeList(List<string> inputs, double numberOfReplications)
        {
            var result = new List<Point>();
            var y = 0;
            foreach (var input in inputs)
            {
                var x = 0;
                for (int i = 0; i < numberOfReplications; i++)
                {
                    for (int z = 0; z < input.Length; z++)
                    {
                        if (input[z] == '#')
                        {
                            result.Add(new Point(x, y));
                        }
                        x++;
                    }
                }
                y++;
            }
            return result;
        }
    }
}
