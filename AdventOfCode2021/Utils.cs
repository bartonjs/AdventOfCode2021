using System;
using System.Diagnostics;

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
    }
}
