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
    public static class Day19
    {
        public static long One()
        {
            var input = InputParser.GetInputText("day19.txt").Split(Environment.NewLine + Environment.NewLine);
            var rules = new Dictionary<string, string>();
            input[0].Split(Environment.NewLine).ToList().ForEach(x => ParseRule(x, rules));

            var regex = $"^{BuildRegex_1(rules)}$";
            return input[1].Split(Environment.NewLine).Where(x => Regex.IsMatch(x, regex)).Count();
        }

        public static long Two()
        {
            var input = InputParser.GetInputText("day19.txt").Split(Environment.NewLine + Environment.NewLine);
            var rules = new Dictionary<string, string>();
            input[0].Split(Environment.NewLine).ToList().ForEach(x => ParseRule(x, rules));

            rules["8"] = "( 42 | 42 8 )";
            rules["11"] = "( 42 31 | 42 11 31 )";

            var regex = $"^{BuildRegex_2(rules)}$";
            return input[1].Split(Environment.NewLine).Where(x => Regex.IsMatch(x, regex)).Count();
        }


        private static string BuildRegex_1(Dictionary<string, string> rules)
        {
            var current = rules["0"].Split(" ").ToList();
            while (current.Any(x => x.Any(y => char.IsDigit(y))))
            {
                current = current.Select(x => rules.ContainsKey(x) ? rules[x] : x).SelectMany(x => x.Split(" ")).ToList();
            }
            return string.Join("", current);
        }

        private static string BuildRegex_2(Dictionary<string, string> rules)
        {
            var current = rules["0"].Split(" ").ToList();
            while (current.Any(x => x.Any(y => char.IsDigit(y))) && current.Count() < 50000)
            {
                current = current.Select(x => rules.ContainsKey(x) ? rules[x] : x).SelectMany(x => x.Split(" ")).ToList();
            }
            return string.Join("", current);
        }

        private static void ParseRule(string line, Dictionary<string, string> rules)
        {
            var ruleParts = line.Split(":");
            var key = ruleParts[0];
            var value = ruleParts[1].Replace("\"", "").Trim();
            if (value.Contains("|"))
            {
                var parts = value.Split("|");
                value = $"( {parts[0]} | {parts[1]} )";
            }
            rules.Add(key, value);
        }
    }
}
