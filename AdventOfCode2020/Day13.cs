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
    public static class Day13
    {
        public static int One()
        {
            var inputs = InputParser.GetInputLines("day13.txt");
            var departureEstimate = int.Parse(inputs[0]);
            var busIdStrings = inputs[1].Split(",").ToList();
            busIdStrings.RemoveAll(b => b == "x");
            var busIds = busIdStrings.Select(t => int.Parse(t)).ToList();
            var busTimes = new List<Tuple<int, int>>();

            foreach (var busId in busIds)
            {
                var count = 0;
                while (count < departureEstimate)
                {
                    count += busId;
                }
                busTimes.Add(new Tuple<int, int>(busId, count - departureEstimate));
            }

            var winner = busTimes.OrderBy(b => b.Item2).First();
            return winner.Item1 * winner.Item2;
        }

        public static long Two()
        {
            var inputs = InputParser.GetInputLines("day13.txt");
            var busIds = inputs[1].Split(",").ToList();

            var firstBusTime= long.Parse(busIds[0]);

            var counter = firstBusTime;
            for (int i = 1; i < busIds.Count; i++)
            {
                if (busIds[i] == "x")
                {
                    continue;
                }

                var nextBusTime = long.Parse(busIds[i]);
                var modValue = nextBusTime - (i % nextBusTime);
                while (firstBusTime % nextBusTime != modValue)
                {
                    firstBusTime += counter;
                }
                counter = MathHelpers.LowestCommonMultiple(counter, nextBusTime);
            }

            return firstBusTime;
        }
    }
}
