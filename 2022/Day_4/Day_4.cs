using _2022;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2022.Day_4
{
    internal class Day_4 : IDay
    {
        public string Run()
        {
            string[] strArray1 = File.ReadAllLines("Day_4/Day4.txt");
            int num = 0;
            foreach (string str in strArray1)
            {
                string[] strArray2 = str.Split(',');
                List<int> sections1 = generateSections(strArray2[0]);
                List<int> sections2 = generateSections(strArray2[1]);
                if (sections1.Min() <= sections2.Min() && sections1.Max() >= sections2.Min())
                    ++num;
                else if (sections1.Min() >= sections2.Min() && sections1.Min() <= sections2.Max())
                    ++num;
            }
            return num.ToString() ?? "";
        }

        private string partOne()
        {
            string[] strArray1 = File.ReadAllLines("Day_4/Day4.txt");
            int num = 0;
            foreach (string str in strArray1)
            {
                string[] strArray2 = str.Split(',');
                List<int> sections1 = generateSections(strArray2[0]);
                List<int> sections2 = generateSections(strArray2[1]);
                if (sections1.Min() <= sections2.Min() && sections1.Max() >= sections2.Max())
                    ++num;
                else if (sections1.Min() >= sections2.Min() && sections1.Max() <= sections2.Max())
                    ++num;
            }
            return num.ToString() ?? "";
        }

        private static List<int> generateSections(string section)
        {
            string[] strArray = section.Split('-');
            int start = int.Parse(strArray[0]);
            int num = int.Parse(strArray[1]);
            return Enumerable.Range(start, num - start + 1).ToList<int>();
        }
    }
}
