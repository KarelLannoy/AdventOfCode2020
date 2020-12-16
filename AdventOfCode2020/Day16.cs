using System;
using System.Collections.Generic;
using System.Drawing;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace AdventOfCode2020
{
    public static class Day16
    {
        public static int One()
        {
            var inputText = InputParser.GetInputText("day16.txt");
            var input = inputText.Split(Environment.NewLine + Environment.NewLine);
            var rules = ParseRules(input[0]);
            var tickets = ParseTickets(input[2]);
            int errorRate = 0;
            foreach (var ticket in tickets)
            {
                errorRate += GetTicketErrorRate(ticket, rules);
            }
            return errorRate;
        }

        private static int GetTicketErrorRate(List<int> ticket, List<Rule> rules)
        {
            var errorRate = 0;
            foreach (var ticketItem in ticket)
            {
                var isValid = false;
                foreach (var rule in rules)
                {
                    if (rule.IsInRange(ticketItem))
                    {
                        isValid = true; 
                    }
                }
                if (!isValid)
                {
                    errorRate += ticketItem;
                }
            }
            return errorRate;
        }
        private static bool IsTicketValid(List<int> ticket, List<Rule> rules)
        {
            foreach (var ticketItem in ticket)
            {
                var isTicketItemValid = false;
                foreach (var rule in rules)
                {
                    if (rule.IsInRange(ticketItem))
                    {
                        isTicketItemValid = true;
                    }
                }
                if (!isTicketItemValid)
                {
                    return false;
                }
            }
            return true;
        }

        private static List<List<int>> ParseTickets(string text)
        {
            var tickets = new List<List<int>>();
            var lines = text.Split(Environment.NewLine).ToList();
            lines.RemoveAt(0);
            foreach (var line in lines)
            {
                tickets.Add(line.Split(",").Select(i => int.Parse(i)).ToList());
            }
            return tickets;
        }

        private static List<Rule> ParseRules(string text)
        {
            var rules = new List<Rule>();
            var lines = text.Split(Environment.NewLine);
            foreach (var line in lines)
            {
                Rule rule = new Rule();
                var lineParts = line.Split(":");
                rule.Name = lineParts[0];
                var rulesetParts = lineParts[1].Trim().Split(" or ");

                foreach (var part in rulesetParts)
                {
                    var parts = part.Split("-");
                    var ruleset = new RuleSet();
                    ruleset.Min = int.Parse(parts[0]);
                    ruleset.Max = int.Parse(parts[1]);
                    rule.RuleSets.Add(ruleset);
                }

                rules.Add(rule);
            }
            return rules;
        }

        public static long Two()
        {
            var inputText = InputParser.GetInputText("day16.txt");
            var input = inputText.Split(Environment.NewLine + Environment.NewLine);
            var rules = ParseRules(input[0]);
            var myTicket = ParseTickets(input[1])[0];
            var tickets = ParseTickets(input[2]);

            var ticketsToRemove = new List<List<int>>();
            foreach (var ticket in tickets)
            {
                if (!IsTicketValid(ticket, rules))
                {
                    ticketsToRemove.Add(ticket);
                }
            }
            ticketsToRemove.ForEach(t => tickets.Remove(t));
            MapTicketsToRules(tickets, rules);

            var importantRules = rules.Where(r => r.Name.StartsWith("departure")).ToList();
            long result = 1;
            foreach (var importantRule in importantRules)
            {
                //Console.WriteLine($"{importantRule.Name} : {importantRule.PossibleLocation.First()} : {myTicket[importantRule.PossibleLocation.First()]}");
                result *= Convert.ToInt64(myTicket[importantRule.PossibleLocation.First()]);
            }
            return result;
        }

        private static void MapTicketsToRules(List<List<int>> tickets, List<Rule> rules)
        {
            foreach (var ticket in tickets)
            {
                for (int i = 0; i < ticket.Count; i++)
                {
                    foreach (var rule in rules)
                    {
                        if (rule.IsInRange(ticket[i]))
                        {
                            if (!rule.CertainlyNotLocation.Contains(i) && !rule.PossibleLocation.Contains(i))
                            {
                                rule.PossibleLocation.Add(i);
                            }
                        }
                        else
                        {
                            if (rule.PossibleLocation.Contains(i))
                            {
                                rule.PossibleLocation.Remove(i);
                            }
                            if (!rule.CertainlyNotLocation.Contains(i))
                            {
                                rule.CertainlyNotLocation.Add(i);
                            }
                        }
                    }
                }
            }
            while (rules.Any(r=>r.PossibleLocation.Count > 1))
            {
                var singleLocations = rules.Where(r => r.PossibleLocation.Count == 1).Select(r => r.PossibleLocation.First()).ToList();
                foreach (var rule in rules)
                {
                    if (rule.PossibleLocation.Count > 1)
                    {
                        rule.PossibleLocation.RemoveAll(i => singleLocations.Contains(i));
                    }
                }
            }
        }
    }
    public class Rule
    {
        public Rule()
        {
            RuleSets = new List<RuleSet>();
            PossibleLocation = new List<int>();
            CertainlyNotLocation = new List<int>();
        }
        public List<int> PossibleLocation { get; set; }
        public List<int> CertainlyNotLocation { get; set; }
        public string Name { get; set; }
        public List<RuleSet> RuleSets { get; set; }
        public bool IsInRange(int value)
        {
            return RuleSets.Any(rs => rs.IsInRange(value));
        }
    }
    public class RuleSet
    {
        public int Min { get; set; }
        public int Max { get; set; }

        public bool IsInRange(int value)
        {
            return Min <= value && Max >= value;
        }
    }
}
