using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace AdventOfCode2020
{
    public static class Day21
    {
        public static long One()
        {
            var (ingredientCount, potentialAllergen) = ParseInput(InputParser.GetInputLines("day21.txt"));

            var allergens = potentialAllergen.Values.SelectMany(x => x).ToHashSet();
            var num = ingredientCount
                .Where(kvp => !allergens.Contains(kvp.Key))
                .Sum(kvp => kvp.Value);

            return num;
        }

        public static string Two()
        {
            var (_, potentialAllergen) = ParseInput(InputParser.GetInputLines("day21.txt"));
            while (potentialAllergen.Values.Any(x => x.Count != 1))
            {
                var potentialAllergenKeys = potentialAllergen.Keys.ToList();
                foreach (var allergen in potentialAllergenKeys)
                {
                    var potAllergens = potentialAllergen[allergen];
                    if (potAllergens.Count != 1) continue;
                    var potentialAllergenCopy = potentialAllergen.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
                    foreach (var (key, value) in potentialAllergenCopy)
                    {
                        if (key == allergen)
                            continue;

                        var newList = value.Where(x => x != potAllergens.Single()).ToHashSet();
                        potentialAllergen[key] = newList;
                    }
                }
            }
            return string.Join(",", potentialAllergen.OrderBy(x => x.Key).Select(x => x.Value.Single()));
        }

        private static (Dictionary<string, int> allergenCount, Dictionary<string, HashSet<string>>) ParseInput(List<string> input)
        {
            Dictionary<string, int> ingredientCount = new Dictionary<string, int>();
            Dictionary<string, HashSet<string>> potentialAllergen = new Dictionary<string, HashSet<string>>();

            foreach (var row in input)
            {
                var split = row.Split(" (contains ");
                var ingredients = split[0].Split(" ");
                var allergens = split[1].Replace(")", "").Split(", ");

                foreach (var ingredient in ingredients)
                {
                    if (ingredientCount.ContainsKey(ingredient))
                        ingredientCount[ingredient] += 1;
                    else
                        ingredientCount[ingredient] = 1;
                }

                foreach (var allergen in allergens)
                {
                    if (potentialAllergen.ContainsKey(allergen))
                    {
                        potentialAllergen[allergen].IntersectWith(ingredients);
                    }
                    else
                    {
                        potentialAllergen[allergen] = new HashSet<string>(ingredients);
                    }
                }
            }

            return (ingredientCount, potentialAllergen);
        }
    }
}
