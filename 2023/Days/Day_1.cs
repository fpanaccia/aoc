using System.Collections;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Text.RegularExpressions;
using static System.Net.Mime.MediaTypeNames;

namespace _2023.Days
{
    internal class Day_1 : IDay
    {
        public List<string> FirstExample = new List<string>()
        {
            "1abc2",
            "pqr3stu8vwx",
            "a1b2c3d4e5f",
            "treb7uchet",
        };

        public List<string> SecondExample = new List<string>()
        {
            "two1nine",
            "eightwothree",
            "abcone2threexyz",
            "xtwone3four",
            "4nineeightseven2",
            "zoneight234",
            "7pqrstsixteen",
        };

        public async Task Run()
        {
            var firstExample = await RunFirstExample();
            var secondExample = await RunSecondExample();
            var first = await RunFirstPart();
            var second = await RunSecondPart();

            Console.WriteLine($"First example {firstExample}");
            Console.WriteLine($"Second example {secondExample}");
            Console.WriteLine($"First {first}");
            Console.WriteLine($"Second {second}");
        }

        public async Task<string> RunFirstExample()
        {
            return await CalculateFirst(FirstExample);
        }

        public async Task<string> RunSecondExample()
        {
            return await CalculateSecond(SecondExample);
        }

        public async Task<string> RunFirstPart()
        {
            var currLocation = AppDomain.CurrentDomain.BaseDirectory;
            var lines = await File.ReadAllLinesAsync($"{currLocation}/Inputs/Day1.txt");
            return await CalculateFirst(lines.ToList());
        }

        public async Task<string> RunSecondPart()
        {
            var currLocation = AppDomain.CurrentDomain.BaseDirectory;
            var lines = await File.ReadAllLinesAsync($"{currLocation}/Inputs/Day1.txt");
            return await CalculateSecond(lines.ToList());
        }

        private async Task<string> CalculateFirst(List<string> calibrationDocument)
        {
            var total = 0;
            foreach (var calibrationLine in calibrationDocument)
            {
                var numberOnly = Regex.Replace(calibrationLine, "[^0-9.]", "");
                var numberArray = numberOnly.Where(c => Char.IsDigit(c)).Select(c => c.ToString()).ToArray();
                var finalNumber = "0";
                if (numberArray.Length > 1)
                {
                    var first = numberArray.First();
                    var last = numberArray.Last();
                    finalNumber = first + last;
                }
                else if (numberArray.Length > 0)
                {
                    var first = numberArray.First();
                    finalNumber = first + first;
                }
                total += int.Parse(finalNumber);

            }
            return total.ToString();
        }

        private async Task<string> CalculateSecond(List<string> calibrationDocument)
        {
            var numbersAsString = new List<Pair> { new Pair("1", "one"), new Pair("2", "two"), new Pair("3", "three"), new Pair("4", "four"), new Pair("5", "five"), new Pair("6", "six"), new Pair("7", "seven"), new Pair("8", "eight"), new Pair("9", "nine") };
            var total = 0;
            foreach (var calibrationLine in calibrationDocument)
            {
                var numberPosition = new Dictionary<int, string>();
                if (!string.IsNullOrWhiteSpace(calibrationLine))
                {
                    foreach (var numberAsString in numbersAsString)
                    {
                        if (calibrationLine.Contains(numberAsString.text))
                        {
                            var indexes = calibrationLine.AllIndexesOf(numberAsString.text).ToList();
                            foreach (var index in indexes)
                            { 
                                numberPosition.Add(index, numberAsString.value); 
                            }
                        }
                    }

                    var numberOnly = Regex.Replace(calibrationLine, "[^0-9.]", "");
                    var numberArray = numberOnly.Where(c => Char.IsDigit(c)).Select(c => c.ToString()).Distinct().ToArray();

                    foreach (var number in numberArray)
                    {
                        var indexes = calibrationLine.AllIndexesOf(number).ToList();
                        foreach (var index in indexes)
                        {
                            numberPosition.Add(index, number);
                        }
                    }

                    var numberPositionWithOrder = numberPosition.OrderBy(x => x.Key);
                    var first = numberPositionWithOrder.First();
                    var last = numberPositionWithOrder.Last();
                    var finalNumber = first.Value + last.Value;
                    total += int.Parse(finalNumber);

                }

            }
            return total.ToString();
        }

        class Pair
        {
            public Pair(string value, string text)
            {
                this.text = text;
                this.value = value;
            }
            public string text { get; set; }
            public string value { get; set; }
        }
    }
}
