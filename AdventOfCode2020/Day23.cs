using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace AdventOfCode2020
{
    public static class Day23
    {
        public static string One()
        {
            var input = InputParser.GetInputText("day23.txt").ToList().ConvertAll<int>((c) => int.Parse(c.ToString()));
            for (int i = 0; i < 100; i++)
            {
                input = ExecuteMove(input, (input.Count + i) % input.Count);
            }

            var indexOfCupOne = input.IndexOf(1);
            input = input.Rotate<int>(indexOfCupOne);
            return string.Join("",input.Skip(1).Take(input.Count - 1).ToArray());
        }

        private static List<int> ExecuteMove(List<int> input, int currentPosition)
        {
            var totalListSize = input.Count;
            var destinationValue = input[currentPosition] - 1;
            var currentValue = input[currentPosition];
            List<int> nextTheePositions = new List<int> { ((totalListSize + currentPosition + 1) % totalListSize), ((totalListSize + currentPosition + 2) % totalListSize), ((totalListSize + currentPosition + 3) % totalListSize) };
            var selectedCups = new List<int>();
            foreach (var position in nextTheePositions)
            {
                selectedCups.Add(input[position]);
            }
            for (int i = 0; i < nextTheePositions.Count; i++)
            {
                var positionToRemove = nextTheePositions[i] - i;
                input.RemoveAt(positionToRemove < 0 ? 0 : positionToRemove);
            }
            while (true)
            {
                if (input.Contains(destinationValue))
                {
                    break;
                }
                else if (destinationValue <= 0)
                {
                    destinationValue = input.Max();
                    break;
                }
                else
                {
                    destinationValue--;
                }
            }
            var destination = input.IndexOf(destinationValue);
            List<int> nextTheeNewPositions = new List<int> { ((totalListSize + destination + 1) % totalListSize), ((totalListSize + destination + 2) % totalListSize), ((totalListSize + destination + 3) % totalListSize) };
            for (int i = 0; i < nextTheeNewPositions.Count; i++)
            {
                if (input.Count <= nextTheeNewPositions[i])
                {
                    input.Add(selectedCups[i]);
                }
                else
                {
                    input.Insert(nextTheeNewPositions[i], selectedCups[i]);
                }
            }
            var shiftdifference = input.IndexOf(currentValue) - currentPosition;
            if (shiftdifference < 0)
            {
                input = input.Rotate<int>(input.Count + shiftdifference);
            }
            if (shiftdifference > 0)
            {
                input = input.Rotate<int>(shiftdifference);
            }
            return input;
        }

        public static List<T> Rotate<T>(this List<T> list, int offset)
        {
            return list.Skip(offset).Concat(list.Take(offset)).ToList();
        }

        public static long Two()
        {
            var cups = new LinkedList<long>(InputParser.GetInputText("day23.txt").Select(c => long.Parse(c.ToString())));
            for (int i = 10; i <= 1000000; i++)
            {
                cups.AddLast(i);
            }
            var cupsDict = new Dictionary<long, (bool active, LinkedListNode<long> node)>(cups.Count);
            for (var node = cups.First; !(node is null); node = node.Next)
            {
                cupsDict.Add(node.Value, (true, node));
            }

            var current = cups.First;
            var pickUp = new LinkedListNode<long>[3];
            long next = -1;

            for (int i = 0; i < 10000000; i++)
            {
                var nextPickUp = current.Next ?? cups.First;
                for (int j = 0; j < 3; j++)
                {
                    pickUp[j] = nextPickUp;
                    nextPickUp = nextPickUp.Next ?? cups.First;
                    cups.Remove(pickUp[j]);
                    cupsDict[pickUp[j].Value] = (false, pickUp[j]);
                }

                next = current.Value == 1 ? cups.Count + pickUp.Length : current.Value - 1;
                while (!cupsDict[next].active)
                {
                    next = next == 1 ? cups.Count + pickUp.Length : next - 1;
                }
                var index = cupsDict[next].node;
                for (int j = 0; j < pickUp.Length; j++)
                {
                    cups.AddAfter(index, pickUp[j]);
                    cupsDict[pickUp[j].Value] = (true, pickUp[j]);
                    index = pickUp[j];
                }
                current = current.Next ?? cups.First;
            }

            return cups.SkipWhile(l => l != 1).Skip(1).Take(2).Aggregate((a, l) => a * l);
        }
    }
}
