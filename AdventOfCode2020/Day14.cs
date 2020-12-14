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
    public static class Day14
    {
        public static long One()
        {
            var inputs = InputParser.GetInputLines("day14.txt");
            var instructions = ParseMaskedInstructions(inputs);
            Dictionary<int, long> memory = new Dictionary<int, long>();
            foreach (var instruction in instructions)
            {
                RunInstructions_1(instruction, memory);
            }
            return memory.Where(kvp => kvp.Value > 0).Select(kvp => kvp.Value).Sum();
        }

        private static void RunInstructions_1(MaskedInstruction instruction, Dictionary<int, long> memory)
        {
            foreach (var operation in instruction.Instructions)
            {
                var instructionValue = Convert.ToString(operation.Item2, 2);
                instructionValue = instructionValue.PadLeft(instruction.Mask.Length, '0');
                StringBuilder sb = new StringBuilder(instructionValue);
                for (int i = 0; i < instruction.Mask.Length; i++)
                {
                    var resultChar = '0';
                    if (instruction.Mask[i] == 'X')
                        resultChar = instructionValue[i];
                    else
                        resultChar = instruction.Mask[i];
                    sb[i] = resultChar;
                }
                memory[operation.Item1] = Convert.ToInt64(sb.ToString(), 2);
            }
        }

        private static List<MaskedInstruction> ParseMaskedInstructions(List<string> inputs)
        {
            MaskedInstruction currenMask = null;
            var instructions = new List<MaskedInstruction>();
            foreach (var input in inputs)
            {
                var inputSplit = input.Split(" = ");
                if (inputSplit[0] == "mask")
                {
                    if (currenMask != null) instructions.Add(currenMask);
                    currenMask = new MaskedInstruction();
                    currenMask.Mask = inputSplit[1];
                }
                else
                {
                    currenMask.Instructions.Add(new Tuple<int, long>(int.Parse(inputSplit[0].Substring(4).TrimEnd(']')), long.Parse(inputSplit[1])));
                }
            }
            instructions.Add(currenMask);
            return instructions;
        }

        public static long Two()
        {
            var inputs = InputParser.GetInputLines("day14.txt");
            var instructions = ParseMaskedInstructions(inputs);
            Dictionary<long, long> memory = new Dictionary<long, long>();
            foreach (var instruction in instructions)
            {
                RunInstructions_2(instruction, memory);
            }
            return memory.Where(kvp => kvp.Value > 0).Select(kvp => kvp.Value).Sum();
        }

        private static void RunInstructions_2(MaskedInstruction instruction, Dictionary<long, long> memory)
        {
            foreach (var operation in instruction.Instructions)
            {
                var memoryValue = Convert.ToString(operation.Item1, 2);
                memoryValue = memoryValue.PadLeft(instruction.Mask.Length, '0');
                StringBuilder sb = new StringBuilder(memoryValue);
                for (int i = 0; i < instruction.Mask.Length; i++)
                {
                    var resultChar = "0";
                    if (instruction.Mask[i] == 'X')
                        resultChar = "X";
                    else
                        resultChar = Convert.ToString((Convert.ToInt32(instruction.Mask[i].ToString(), 2) | Convert.ToInt32(memoryValue[i].ToString(), 2)), 2);
                    sb[i] = resultChar[0];
                }
                var addresses = GetAllMemoryAddresses(sb.ToString());
                foreach (var address in addresses)
                {
                    memory[Convert.ToInt64(address,2)] = operation.Item2;
                }
            }
        }

        private static List<string> GetAllMemoryAddresses(string memoryAddress)
        {
            for (int i = 0; i < memoryAddress.Length; i++)
            {
                if (memoryAddress[i] == 'X')
                {
                    var addressOne = memoryAddress.Remove(i, 1).Insert(i, "1");
                    var addressTwo= memoryAddress.Remove(i, 1).Insert(i, "0");
                    List<string> returnValue = new List<string>();
                    if (addressOne.Contains('X'))
                    {
                        returnValue.AddRange(GetAllMemoryAddresses(addressOne));
                        returnValue.AddRange(GetAllMemoryAddresses(addressTwo));
                    }
                    else
                    {
                        returnValue.Add(addressOne);
                        returnValue.Add(addressTwo);
                    }
                    return returnValue;
                }
                continue;
            }
            return null;
        }
    }

    public class MaskedInstruction
    {
        public MaskedInstruction()
        {
            Instructions = new List<Tuple<int, long>>();
        }
        public string Mask { get; set; }
        public List<Tuple<int, long>> Instructions { get; set; }
    }
}
