using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode2020
{
    public static class Day06
    {
        public static string One()
        {
            var inputString = InputParser.GetInputText("day06.txt");
            var inputs = inputString.Split(Environment.NewLine + Environment.NewLine);
            int count = 0;
            foreach (var input in inputs)
            {
                count += input.Replace(Environment.NewLine, "").Distinct().Count();
            }
            return count.ToString();
        }

        public static string Two()
        {
            var inputString = InputParser.GetInputText("day06.txt");
            var inputs = inputString.Split(Environment.NewLine + Environment.NewLine);
            int count = 0;
            foreach (var input in inputs)
            {
                var countAnswerForPerson = 0;
                var nrOfPersons = input.Count(c => c == Environment.NewLine[0]) + 1;
                var allUniqueAnswers = input.Replace(Environment.NewLine, "").Distinct();
                foreach (var answer in allUniqueAnswers)
                {
                    if (input.Count(c => c == answer) == nrOfPersons)
                        countAnswerForPerson++;
                }
                count += countAnswerForPerson;
            }
            return count.ToString();
        }
    }
}
