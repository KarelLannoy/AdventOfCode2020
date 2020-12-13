using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2020
{
    public static class MathHelpers
    {
        public static long GreatestCommonDevisor(long item1, long item2)
        {
            while (item2 != 0)
            {
                item2 = item1 % (item1 = item2);
            }
            return item1;
        }

        public static long LowestCommonMultiple(long item1, long item2)
        {
            return item1 * item2 / GreatestCommonDevisor(item1, item2); 
        }
    }
}
