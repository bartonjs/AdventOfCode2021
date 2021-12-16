using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace AdventOfCode2021
{
    internal static class Utils
    {
        internal static long ParseBinary(string input)
        {
            long value = 0;

            foreach (char c in input)
            {
                value <<= 1;

                if (c == '1')
                {
                    value |= 1;
                }
            }

            return value;
        }

        [Conditional("SAMPLE")]
        internal static void TraceForSample(string message)
        {
            Console.WriteLine(message);
        }

        internal static long Product<T>(this IEnumerable<T> source, Func<T, long> selector)
        {
            long product = 1;

            foreach (long val in source.Select(selector))
            {
                checked
                {
                    product *= val;
                }
            }

            return product;
        }
    }
}
