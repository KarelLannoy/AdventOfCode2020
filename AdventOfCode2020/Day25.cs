using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace AdventOfCode2020
{
    public static class Day25
    {
        public static void One()
        {
            var input = InputParser.GetInputLines("day25.txt");
            var cardLoopSize = GetLoopSize(input[0]);
            var doorLoopSize = GetLoopSize(input[1]);

            var cardEncryptionKey = GetEncryptionKey(cardLoopSize, long.Parse(input[1]));
            var doorEncryptionKey = GetEncryptionKey(doorLoopSize, long.Parse(input[0]));

            Console.WriteLine(cardEncryptionKey + " " + doorEncryptionKey);
        }

        private static long GetLoopSize(string input)
        {
            var subjectNumber = 7;
            long begin = 1;
            long remainder = 20201227;
            long loopsize = 0;
            while (begin != long.Parse(input))
            {
                loopsize++;
                begin = begin * subjectNumber;
                begin = begin % remainder;
            }
            return loopsize;
        }

        private static long GetEncryptionKey(long loopSize, long subjectNumber)
        {
            long begin = 1;
            long remainder = 20201227;
            for (int i = 0; i < loopSize; i++)
            {
                begin = begin * subjectNumber;
                begin = begin % remainder;
            }
            return begin;
        }
    }
}
