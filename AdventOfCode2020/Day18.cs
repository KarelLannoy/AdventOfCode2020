using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode2020
{
    public static class Day18
    {
        public static long One()
        {
            var mathProblems = InputParser.GetInputLines("day18.txt");
            var results = new List<long>();
            foreach (var problem in mathProblems)
            {
                results.Add(long.Parse(SolveProblem_1(problem.Replace(" ",""))));
            }
            return results.Sum();
        }

        private static string SolveProblem_1(string problem)
        {
            while (problem.Contains('('))
            {
                var subProblem = FindFirstSubProblem(problem);
                var subProblemResult = SolveProblem_1(subProblem.Substring(1, subProblem.Length - 2));
                problem = problem.Replace(subProblem, subProblemResult);
            }
            string operatorString = null;
            string firstNumber = null;
            string secondNumber = null;
            for (int i = 0; i < problem.Length; i++)
            {
                char c = problem[i];
                if (c == '+')
                {
                    if (operatorString != null)
                    {
                        firstNumber = RunMath(firstNumber, secondNumber, operatorString);
                        secondNumber = null;
                    }
                    operatorString = "+";
                }
                else if (c == '*')
                {
                    if (operatorString != null)
                    {
                        firstNumber = RunMath(firstNumber, secondNumber, operatorString);
                        secondNumber = null;
                    }
                    operatorString = "*";
                }
                else
                {
                    if (operatorString == null)
                        firstNumber += c;
                    else
                        secondNumber += c;
                }

            }
            return RunMath(firstNumber, secondNumber, operatorString);
        }

        private static string RunMath(string firstNumber, string secondNumber, string operatorString)
        {
            switch (operatorString)
            {
                case "+":
                    return (long.Parse(firstNumber) + long.Parse(secondNumber)).ToString();
                case "*":
                    return (long.Parse(firstNumber) * long.Parse(secondNumber)).ToString();
                default:
                    throw new Exception("This should not be happening");
            }
        }

        private static string FindFirstSubProblem(string problem)
        {
            var firstOccurence = problem.IndexOf('(');
            var bracketIndex = 0;
            var subProblem = "";
            for (int i = firstOccurence; i < problem.Length; i++)
            {
                char c = problem[i];
                if (c == '(')
                    bracketIndex++;
                if (c == ')')
                    bracketIndex--;
                subProblem += c;
                if (bracketIndex == 0)
                    break;
            }
            return subProblem;
        }

        public static long Two()
        {
            var mathProblems = InputParser.GetInputLines("day18.txt");
            var results = new List<long>();
            foreach (var problem in mathProblems)
            {
                results.Add(long.Parse(SolveProblem_2(problem.Replace(" ", ""))));
            }
            return results.Sum();
        }

        private static string SolveProblem_2(string problem)
        {
            while (problem.Contains('('))
            {
                var subProblem = FindFirstSubProblem(problem);
                var subProblemResult = SolveProblem_2(subProblem.Substring(1, subProblem.Length - 2));
                problem = problem.Replace(subProblem, subProblemResult);
            }
            while (problem.Contains('+'))
            {
                var subAdditionProblem = FindFirstSubAdditionProblem(problem);
                var subAdditionProblemResult = RunMath(subAdditionProblem.Split("+")[0], subAdditionProblem.Split("+")[1], "+");
                Regex regex = new Regex(Regex.Escape(subAdditionProblem));
                problem = regex.Replace(problem, subAdditionProblemResult, 1);
            }
            if (problem.Contains('*'))
            {


                string operatorString = null;
                string firstNumber = null;
                string secondNumber = null;
                for (int i = 0; i < problem.Length; i++)
                {
                    char c = problem[i];
                    if (c == '+')
                    {
                        if (operatorString != null)
                        {
                            firstNumber = RunMath(firstNumber, secondNumber, operatorString);
                            secondNumber = null;
                        }
                        operatorString = "+";
                    }
                    else if (c == '*')
                    {
                        if (operatorString != null)
                        {
                            firstNumber = RunMath(firstNumber, secondNumber, operatorString);
                            secondNumber = null;
                        }
                        operatorString = "*";
                    }
                    else
                    {
                        if (operatorString == null)
                            firstNumber += c;
                        else
                            secondNumber += c;
                    }

                }
                return RunMath(firstNumber, secondNumber, operatorString);
            }
            else
                return problem;
        }

        private static string FindFirstSubAdditionProblem(string problem)
        {
            var firstOccurence = problem.IndexOf('+');
            string firstNumber = "";
            string secondNumber = "";
            for (int i = firstOccurence -1 ; i >= 0; i--)
            {
                if (Char.IsNumber(problem[i]))
                {
                    firstNumber = firstNumber.PadLeft(firstNumber.Length +1 , problem[i]);
                }
                else
                    break;
            }
            for (int i = firstOccurence + 1; i < problem.Length; i++)
            {
                if (Char.IsNumber(problem[i]))
                {
                    secondNumber += problem[i];
                }
                else
                    break;
            }

            return $"{firstNumber}+{secondNumber}";
        }
    }
}
