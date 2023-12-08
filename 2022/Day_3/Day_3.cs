using _2022;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2022.Day_3
{
    internal class Day_3 : IDay
    {
        public string Run()
        {
            string[] strArray = File.ReadAllLines("Day_3/Day3.txt");
            List<List<string>> stringListList = new List<List<string>>();
            int num1 = 0;
            List<string> stringList1 = new List<string>();
            int num2 = 0;
            foreach (string str in strArray)
            {
                stringList1.Add(str);
                if (num1 == 2)
                {
                    num1 = 0;
                    stringListList.Add(stringList1);
                    stringList1 = new List<string>();
                }
                else
                    ++num1;
            }
            foreach (List<string> stringList2 in stringListList)
            {
                List<char> charList = new List<char>();
                foreach (char ch in stringList2[0].Distinct<char>())
                {
                    if (stringList2[1].Contains(ch))
                        charList.Add(ch);
                }
                foreach (char ch in charList)
                {
                    if (stringList2[2].Contains(ch))
                        num2 += getPrio(ch);
                }
            }
            return num2.ToString() ?? "";
        }

        private static string partOne()
        {
            string[] strArray = File.ReadAllLines("Day_3/Day3.txt");
            int num1 = 0;
            foreach (string str1 in strArray)
            {
                int num2 = str1.Length / 2;
                string source = str1.Substring(0, num2);
                string str2 = str1.Substring(num2);
                foreach (char ch in source.Distinct<char>())
                {
                    if (str2.Contains(ch))
                        num1 += getPrio(ch);
                }
            }
            return num1.ToString() ?? "";
        }

        private static int getPrio(char value)
        {
            return char.IsLower(value) ? (int)value - 96 : (int)value - 38;
        }
    }
}
