using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode2020
{
    public static class Day02
    {
        public static string One()
        {
            var inputs = InputParser.GetInputLines("day02.txt");
            int count = 0;
            foreach (var passwordLine in inputs)
            {
                if (PasswordValidatorOne(passwordLine))
                    count++;
            }

            return count.ToString();
            throw new Exception("Bad solution");
        }

        public static bool PasswordValidatorOne(string passwordLine)
        {
            var passwordLineParts = passwordLine.Split(": ", StringSplitOptions.RemoveEmptyEntries);
            var passwordRule = passwordLineParts[0];
            var password = passwordLineParts[1];

            var ruleParts = passwordRule.Split(" ", StringSplitOptions.RemoveEmptyEntries);
            var ruleRange = ruleParts[0];
            var ruleValue = ruleParts[1];
            var ruleRangeParts = ruleRange.Split("-", StringSplitOptions.RemoveEmptyEntries);
            var ruleLowerBound = int.Parse(ruleRangeParts[0]);
            var ruleUpperBound = int.Parse(ruleRangeParts[1]);
            var occurences = password.Count(c => c == char.Parse(ruleValue));
            return occurences >= ruleLowerBound && occurences <= ruleUpperBound;
        }

        public static string Two()
        {
            var inputs = InputParser.GetInputLines("day02.txt");
            int count = 0;
            foreach (var passwordLine in inputs)
            {
                if (PasswordValidatorTwo(passwordLine))
                    count++;
            }

            return count.ToString();
            throw new Exception("Bad solution");
        }

        public static bool PasswordValidatorTwo(string passwordLine)
        {
            var passwordLineParts = passwordLine.Split(": ", StringSplitOptions.RemoveEmptyEntries);
            var passwordRule = passwordLineParts[0];
            var password = passwordLineParts[1];

            var ruleParts = passwordRule.Split(" ", StringSplitOptions.RemoveEmptyEntries);
            var ruleRange = ruleParts[0];
            var ruleValue = ruleParts[1];
            var ruleRangeParts = ruleRange.Split("-", StringSplitOptions.RemoveEmptyEntries);
            var rulePositionOne = int.Parse(ruleRangeParts[0]) - 1;
            var rulePositionTwo = int.Parse(ruleRangeParts[1]) - 1;


            var letterOnPositionOne = password[rulePositionOne].ToString();
            var letterOnPositionTwo = password[rulePositionTwo].ToString();

            return letterOnPositionOne == ruleValue ^ letterOnPositionTwo == ruleValue;
        }
    }
}
