using _2022;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace _2022.Day_1
{
    internal class Day_1 : IDay
    {
        public string Run()
        {
            string[] strArray = File.ReadAllLines("Day_1/Day1.txt");
            int num1 = 0;
            List<int> source = new List<int>();
            foreach (string s in strArray)
            {
                if (string.IsNullOrEmpty(s))
                {
                    source.Add(num1);
                    num1 = 0;
                }
                else
                    num1 += int.Parse(s);
            }
            int num2 = source.OrderByDescending<int, int>((Func<int, int>)(x => x)).Take<int>(3).Sum();
            DefaultInterpolatedStringHandler interpolatedStringHandler = new DefaultInterpolatedStringHandler(3, 2);
            interpolatedStringHandler.AppendFormatted<int>(source.Max());
            interpolatedStringHandler.AppendLiteral(" - ");
            interpolatedStringHandler.AppendFormatted<int>(num2);
            return interpolatedStringHandler.ToStringAndClear();
        }
    }
}
