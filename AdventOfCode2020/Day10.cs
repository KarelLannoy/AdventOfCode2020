using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace AdventOfCode2020
{
    public static class Day10
    {
        public static int One()
        {
            var adapters = InputParser.GetInputLines<int>("day10.txt");
            adapters.Sort();
            var oneStep = 0;
            var threeStep = 1; //count the final jump to device
            var previousJolt = 0;
            for (int i = 0; i < adapters.Count; i++)
            {
                var currentJolt = adapters[i];
                var joltDifference = currentJolt - previousJolt;
                switch (joltDifference)
                {
                    case 1:
                        oneStep++;
                        break;
                    case 3:
                        threeStep++;
                        break;
                    default:
                        throw new Exception("Invalid input");
                }
                previousJolt = currentJolt;
            }
            return oneStep * threeStep;
        }

        public static double Two()
        {
            var adapters = InputParser.GetInputLines<int>("day10.txt");
            adapters.Sort();
            
            adapters.Insert(0, 0); // insert outlet
            adapters.Add(adapters.Last() + 3); //insert your device

            var countDictionary = new Dictionary<int, long>();

            countDictionary[adapters.Count - 1] = 0; //your device is the endpoint
            countDictionary[adapters.Count - 2] = 1; //your device is 3 away from the final step, so only 1 possible combination

            FindValidCombination(0, adapters, countDictionary);
            return countDictionary[0];
        }

        private static long FindValidCombination(int index, List<int> adapters, Dictionary<int, long> countDictionary)
        {
            if (countDictionary.ContainsKey(index)) return countDictionary[index];
            long count = 0;
            for (int i = 1; i <= 3; i++)
            {
                if ((index + i < adapters.Count) && (adapters[index + i] - adapters[index] <= 3))
                {
                    count += FindValidCombination(index + i, adapters, countDictionary);
                }
            }
            countDictionary[index] = count;
            return count;
        }
    }
}
