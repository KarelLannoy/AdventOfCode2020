using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode2020
{
    public static class Day09
    {
        public static long One()
        {
            List<long> inputs = InputParser.GetInputLines<long>("day09.txt");
            int preamble = 25;
            for (int i = preamble; i < inputs.Count; i++)
            {
                if (!CheckValid(inputs[i], inputs.GetRange(i - preamble, preamble)))
                    return inputs[i];
            }

            throw new Exception("Solution is wrong");
        }

        private static bool CheckValid(long value, List<long> preambleList)
        {
            preambleList.RemoveAll(l => l >= value);
            preambleList.Sort();
            for (int i = 0; i < preambleList.Count; i++)
            {
                for (int y = 0; y < preambleList.Count; y++)
                {
                    if (i == y)
                        continue;
                    if (preambleList[i] + preambleList[y] == value)
                        return true;
                }
            }
            return false;
        }

        public static long Two()
        {
            var weaknessNumber = One();
            List<long> inputs = InputParser.GetInputLines<long>("day09.txt");
            for (int i = 0; i < inputs.Count; i++)
            {
                int indexer = 0;
                List<long> sumNumbers = new List<long>();
                while (sumNumbers.Sum() < weaknessNumber)
                {
                    sumNumbers.Add(inputs[i + indexer]);
                    indexer++;
                }
                if (sumNumbers.Count > 1 && sumNumbers.Sum() == weaknessNumber)
                {
                    return sumNumbers.Min() + sumNumbers.Max();
                }
            }
            throw new Exception("Solution is wrong");
        }
    }
}
