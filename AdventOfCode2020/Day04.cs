using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode2020
{
    public static class Day04
    {
        private static List<string> _passportFields = new List<string>() { "byr", "iyr", "eyr", "hgt", "hcl", "ecl", "pid" };
        private static List<string> _eyeColors = new List<string>() { "amb", "blu", "brn", "gry", "grn", "hzl", "oth" };
        public static string One()
        {
            var inputs = InputParser.GetInputLines("day04.txt");
            var passports = CreatePassports(inputs);
            var counter = 0;
            foreach (var passport in passports)
            {
                if (ValidatePassport_1(passport))
                {
                    counter++;
                }
            }
            return counter.ToString();
            throw new Exception("Bad solution");
        }

        private static bool ValidatePassport_1(Dictionary<string, string> passport)
        {
            foreach (var passportfield in _passportFields)
            {
                if (!passport.ContainsKey(passportfield))
                {
                    return false;
                }
            }
            return true;
        }

        public static string Two()
        {
            var inputs = InputParser.GetInputLines("day04.txt");
            var passports = CreatePassports(inputs);
            var counter = 0;
            foreach (var passport in passports)
            {
                if (ValidatePassport_2(passport))
                {
                    counter++;
                }
            }
            return counter.ToString();
            throw new Exception("Bad solution");
        }

        private static bool ValidatePassport_2(Dictionary<string, string> passport)
        {
            foreach (var passportfield in _passportFields)
            {
                if (!passport.ContainsKey(passportfield))
                    return false;
            }
            return NumericRangeChecker(passport["byr"], 1920, 2002)
                &&
                NumericRangeChecker(passport["iyr"], 2010, 2020)
                &&
                NumericRangeChecker(passport["eyr"], 2020, 2030)
                &&
                HeightChecker(passport["hgt"])
                &&
                HairColorChecker(passport["hcl"])
                &&
                _eyeColors.Contains(passport["ecl"])
                &&
                PassportIdChecker(passport["pid"]);
        }

        private static bool HairColorChecker(string field)
        {
            return field.Length == 7 && field[0] == '#' && IsHexadecimal(field.Substring(1));
        }

        private static bool PassportIdChecker(string field)
        {
            return field.Length == 9 && IsNumeric(field);

        }

        private static bool HeightChecker(string field)
        {
            var height = field.Substring(0, field.Length - 2);
            var unit = field.Substring(field.Length - 2, 2);
            switch (unit)
            {
                case "cm":
                    return NumericRangeChecker(height, 150, 193);
                case "in":
                    return NumericRangeChecker(height, 59, 76);
                default:
                    return false; ;
            }
        }

        private static bool NumericRangeChecker(string field, int lowerBound, int upperBound)
        {
            return IsNumeric(field) && (int.Parse(field) <= upperBound && int.Parse(field) >= lowerBound);
        }

        private static bool IsNumeric(string value)
        {
            return value.All(char.IsNumber);
        }

        private static bool IsHexadecimal(string value)
        {
            return Regex.IsMatch(value, @"\A\b[0-9a-fA-F]+\b\Z");
        }

        private static List<Dictionary<string, string>> CreatePassports(List<string> inputs)
        {
            List<Dictionary<string, string>> passports = new List<Dictionary<string, string>>();
            Dictionary<string, string> passport = new Dictionary<string, string>();
            foreach (var line in inputs)
            {
                if (line == "")
                {
                    passports.Add(passport);
                    passport = new Dictionary<string, string>();
                }
                else
                {
                    var passportParts = line.Split(" ");
                    foreach (var part in passportParts)
                    {
                        var passportItem = part.Split(":");
                        passport.Add(passportItem[0], passportItem[1]);
                    }
                }
            }
            passports.Add(passport);
            return passports;
        }
    }
}
