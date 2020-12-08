using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode2020
{
    public static class Day08
    {
        public static int One()
        {
            var bootCode = InputParser.GetInputLines("day08.txt");
            return RunCode(bootCode).Item2;
        }

        private static (bool, int) RunCode(List<string> bootCode)
        {
            HashSet<int> instructionHistory = new HashSet<int>();

            var InfinitLoopError = false;
            var pointer = 0;
            var accumulator = 0;
            while (pointer < bootCode.Count)
            {
                if(instructionHistory.Contains(pointer))
                {
                    InfinitLoopError = true;
                    break;
                }
                instructionHistory.Add(pointer);
                var operationCode = bootCode[pointer];
                var operation = operationCode.Split(" ");
                switch (operation[0])
                {
                    case "nop":
                        pointer++;
                        break;
                    case "acc":
                        accumulator += int.Parse(operation[1]);
                        pointer++;
                        break;
                    case "jmp":
                        pointer += int.Parse(operation[1]);
                        break;
                    default:
                        throw new Exception("Instruction not found");
                }
            }
            return (InfinitLoopError, accumulator);
        }

        public static int Two()
        {
            var bootCode = InputParser.GetInputLines("day08.txt");
            for (int i = 0; i < bootCode.Count; i++)
            {
                var bootCodeCopy = bootCode.ConvertAll(s => s);
                if (bootCodeCopy[i].StartsWith("jmp"))
                {
                    bootCodeCopy[i] = bootCodeCopy[i].Replace("jmp", "nop");
                    var result = RunCode(bootCodeCopy);
                    if (!result.Item1)
                        return result.Item2;
                }
                else if (bootCodeCopy[i].StartsWith("nop"))
                {
                    bootCodeCopy[i] = bootCodeCopy[i].Replace("nop", "jmp");
                    var result = RunCode(bootCodeCopy);
                    if (!result.Item1)
                        return result.Item2;
                }
                
            }
            throw new Exception("No solution found");
        }
    }
}
