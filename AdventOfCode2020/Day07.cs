using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode2020
{
    public static class Day07
    {
        public static string One()
        {
            var inputs = InputParser.GetInputLines("day07.txt");
            var bagTree = CreateBagTree(inputs);
            var parentBags = new List<string>();
            parentBags = LoopTreeUp(bagTree, "shiny gold", parentBags);
            return parentBags.Distinct().Count().ToString();
        }

        private static List<string> LoopTreeUp(Dictionary<string, List<Tuple<int, string>>> bagTree, string bagColor, List<string> parentList)
        {
            var parentBags = bagTree.Where(bt => bt.Value.Any(b => b.Item2.Equals(bagColor))).Select(bt => bt.Key).Distinct().ToList();
            if (parentBags != null && parentBags.Count > 0)
            {
                parentList.AddRange(parentBags);
                foreach (var parentBag in parentBags)
                {
                    parentList = LoopTreeUp(bagTree, parentBag, parentList);
                }
            }
            return parentList;
        }

        public static string Two()
        {
            var inputs = InputParser.GetInputLines("day07.txt");
            var bagTree = CreateBagTree(inputs);
            var startBag = bagTree.First(kvp=>kvp.Key == "shiny gold");

            var result = LoopTreeDown(bagTree, startBag) - 1;//shiny gold bag
            return result.ToString();
        }

        private static int LoopTreeDown(Dictionary<string, List<Tuple<int, string>>> bagTree, KeyValuePair<string, List<Tuple<int, string>>> startBag)
        {
            var count = 1;
            foreach (var bag in startBag.Value.Where(t=>t.Item1 > 0))
            {
                var newParentBag = bagTree.First(kvp => kvp.Key == bag.Item2);
                count += bag.Item1 * LoopTreeDown(bagTree, newParentBag);
            }
            return count;
        }

        private static Dictionary<string, List<Tuple<int, string>>> CreateBagTree(List<string> inputs)
        {
            Dictionary<string, List<Tuple<int, string>>> rules = new Dictionary<string, List<Tuple<int, string>>>();
            foreach (var input in inputs)
            {
                var inputParts = input.Split(" bags contain ", StringSplitOptions.RemoveEmptyEntries);
                var parentBagColor = inputParts[0];
                var childBags = inputParts[1].Split(", ");
                if (childBags[0] == "no other bags.")
                    rules.Add(parentBagColor, new List<Tuple<int, string>> { new Tuple<int, string>(0, "") });
                else
                    rules.Add(parentBagColor, childBags.Select(s => new Tuple<int, string>(int.Parse(s.Substring(0, s.IndexOf(" "))), s.Substring(s.IndexOf(" ") + 1).Replace(" bags", "").Replace(" bag", "").Replace(".", ""))).ToList());
            }
            return rules;
        }
    }
}
