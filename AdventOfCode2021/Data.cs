using System;
using System.Collections.Generic;

namespace AdventOfCode2021
{
    internal static class Data
    {
        private static string[] s_lines;

        internal static IEnumerable<string> Enumerate()
        {
            foreach (string line in s_lines)
            {
                yield return line;
            }
        }

        internal static List<long> ToLongList(this string commaSeparated)
        {
            ReadOnlySpan<char> span = commaSeparated;
            int comma = commaSeparated.IndexOf(',');
            List<long> list = new List<long>();

            while (comma >= 0)
            {
                list.Add(long.Parse(span.Slice(0, comma)));
                span = span.Slice(comma + 1);
                comma = span.IndexOf(',');
            }

            list.Add(long.Parse(span));
            return list;
        }
    }
}
