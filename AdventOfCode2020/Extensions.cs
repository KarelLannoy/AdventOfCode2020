using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2020
{
    public static class Extensions
    {
        public static IEnumerable<IEnumerable<T>> Split<T>(this IEnumerable<T> collection, T splitOn)
        {
            var current = new List<T>();
            foreach (var item in collection)
            {
                if (item.Equals(splitOn))
                {
                    yield return current;
                    current = new List<T>();
                }
                else
                {
                    current.Add(item);
                }
            }

            yield return current;
        }
    }
}
