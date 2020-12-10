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

            var memory = new Dictionary<int, long>();

            memory[adapters.Count - 1] = 0; //your device is the endpoint
            memory[adapters.Count - 2] = 1; //your device is 3 away from the final step, so only 1 possible combination

            FindValidCombination(0, adapters, memory);
            return memory[0];
        }

        private static long FindValidCombination(int index, List<int> adapters, Dictionary<int, long> memory)
        {
            // if possible steps to en for adapter in chain is already calculated, get it from memory
            if (memory.ContainsKey(index)) return memory[index];
            long count = 0;
            // next potential adapter can only be three places away 
            // calculate number of potential steps to the end for those three places if the step is possible (<= 3)
            for (int i = 1; i <= 3; i++)
            {
                if ((index + i < adapters.Count) && (adapters[index + i] - adapters[index] <= 3))
                {
                    count += FindValidCombination(index + i, adapters, memory);
                }
            }
            // Add calculated adapter possible steps to end to memory 
            memory[index] = count;
            return count;
        }
    }
}
