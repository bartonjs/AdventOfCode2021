using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Solutions
{
    internal class Day3
    {
        internal static void Problem1()
        {
            int[] ctrs = null;
            int entries = 0;

            foreach (string line in Data.Enumerate())
            {
                entries++;
                ctrs ??= new int[line.Length];

                int pos = 0;

                foreach (char c in line)
                {
                    if (c == '1')
                    {
                        ctrs[pos]++;
                    }

                    pos++;
                }
            }

            long epsilon = 0;
            long gamma = 0;
            long half = entries / 2;
            int exponent = 0;

            for (int i = ctrs.Length - 1; i >= 0; i--)
            {
                long val = (long)Math.Pow(2, exponent);
                exponent++;

                if (ctrs[i] > half)
                {
                    gamma += val;
                }
                else
                {
                    epsilon += val;
                }
            }

            Console.WriteLine($"gamma={gamma},epsilon={epsilon},pwr={gamma*epsilon}");
        }

        internal static void Problem2()
        {
            List<string> oxy = Data.Enumerate().ToList();
            List<string> co2 = new List<string>(oxy);

            int len = oxy[0].Length;

            for (int i = 0; i < len && oxy.Count > 1; i++)
            {
                int set = oxy.Count(v => v[i] == '1');
                int rem = oxy.Count - set;

#if SAMPLE
                Console.WriteLine($"{set}/{oxy.Count} bits set.");
#endif

                if (set < rem)
                {
#if SAMPLE
                    Console.WriteLine("Removing the 1s.");
#endif
                    RemoveIf(oxy, i, '1');
                }
                else
                {
#if SAMPLE
                    Console.WriteLine("Removing the 0s.");
#endif
                    RemoveIf(oxy, i, '0');
                }
            }

            for (int i = 0; i < len && co2.Count > 1; i++)
            {
                int set = co2.Count(v => v[i] == '1');
                int rem = co2.Count - set;

#if SAMPLE
                Console.WriteLine($"{set}/{co2.Count} bits set.");
#endif

                if (set >= rem)
                {
#if SAMPLE
                    Console.WriteLine("Removing the 1s.");
#endif
                    RemoveIf(co2, i, '1');
                }
                else
                {
#if SAMPLE
                    Console.WriteLine("Removing the 0s.");
#endif
                    RemoveIf(co2, i, '0');
                }
            }

            long oxyVal = ParseBinary(oxy.Single());
            long co2Val = ParseBinary(co2.Single());

            Console.WriteLine($"oxy={oxy.Single()}=>{oxyVal},co2={co2.Single()}=>{co2Val},rating={oxyVal*co2Val}");
        }

        private static void RemoveIf(List<string> list, int bit, char match)
        {
            for (int j = list.Count - 1; j >= 0; j--)
            {
                if (list[j][bit] == match)
                {
                    list.RemoveAt(j);
                }
            }
        }

        private static long ParseBinary(string input)
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
    }
}
