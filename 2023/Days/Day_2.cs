using System.Collections;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Text.RegularExpressions;
using static System.Net.Mime.MediaTypeNames;

namespace _2023.Days
{
    internal class Day_2 : IDay
    {
        public List<string> Example = new List<string>()
        {
            "Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green",
            "Game 2: 1 blue, 2 green; 3 green, 4 blue, 1 red; 1 green, 1 blue",
            "Game 3: 8 green, 6 blue, 20 red; 5 blue, 4 red, 13 green; 5 green, 1 red",
            "Game 4: 1 green, 3 red, 6 blue; 3 green, 6 red; 3 green, 15 blue, 14 red",
            "Game 5: 6 red, 1 blue, 3 green; 2 blue, 1 red, 2 green",
        };

        public Dictionary<string, int> FirstTotalCubes = new Dictionary<string, int>
        {
            ["red"] = 12,
            ["green"] = 13,
            ["blue"] = 14
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
            return await CalculateFirst(Example);
        }

        public async Task<string> RunSecondExample()
        {
            return await CalculateSecond(Example);
        }

        public async Task<string> RunFirstPart()
        {
            var currLocation = AppDomain.CurrentDomain.BaseDirectory;
            var lines = await File.ReadAllLinesAsync($"{currLocation}/Inputs/Day2.txt");
            return await CalculateFirst(lines.ToList());
        }

        public async Task<string> RunSecondPart()
        {
            var currLocation = AppDomain.CurrentDomain.BaseDirectory;
            var lines = await File.ReadAllLinesAsync($"{currLocation}/Inputs/Day2.txt");
            return await CalculateSecond(lines.ToList());
        }

        private async Task<string> CalculateFirst(List<string> games)
        {
            var total = 0;
            foreach (var game in games)
            {
                var splitedGame = game.Split(":");
                var gameNumber = int.Parse(splitedGame[0].Replace("Game ", ""));
                var gameRounds = splitedGame[1].Split(";");
                var mappedRounds = new List<Dictionary<string, int>>();

                foreach (var round in gameRounds)
                {
                    var newRound = GetEmptyRound();
                    var cubeResults = round.Split(",");
                    foreach (var cubeResult in cubeResults)
                    {
                        var cube = cubeResult.Trim().Split(" ");
                        newRound[cube[1]] = int.Parse(cube[0]);
                    }

                    mappedRounds.Add(newRound);
                }

                var filteredmappedRounds = mappedRounds.Where(mappedRound => FirstTargetHasAllPositive2(mappedRound));
                if (filteredmappedRounds.Count() == gameRounds.Length)
                {
                    total += gameNumber;
                }
            }

            return total.ToString();
        }

        private async Task<string> CalculateSecond(List<string> games)
        {
            var total = 0;
            foreach (var game in games)
            {
                var splitedGame = game.Split(":");
                var gameNumber = int.Parse(splitedGame[0].Replace("Game ", ""));
                var gameRounds = splitedGame[1].Split(";");
                var minimumPower = GetMinimumPowerGame();

                foreach (var round in gameRounds)
                {
                    var cubeResults = round.Split(",");
                    foreach (var cubeResult in cubeResults)
                    {
                        var cube = cubeResult.Trim().Split(" ");
                        var value = int.Parse(cube[0]);
                        if (value > minimumPower[cube[1]]) minimumPower[cube[1]] = value;
                    }
                }
                total += minimumPower["red"] * minimumPower["green"] * minimumPower["blue"];
            }

            return total.ToString();
        }

        private Dictionary<string, int> GetEmptyRound()
        {
            return new Dictionary<string, int>
            {
                ["red"] = 0,
                ["green"] = 0,
                ["blue"] = 0
            };
        }

        private Dictionary<string, int> GetMinimumPowerGame()
        {
            return new Dictionary<string, int>
            {
                ["red"] = 1,
                ["green"] = 1,
                ["blue"] = 1
            };
        }

        private bool FirstTargetHasAllPositive2(Dictionary<string, int> target)
        {
            return FirstTotalCubes["red"] >= target["red"] && FirstTotalCubes["green"] >= target["green"] && FirstTotalCubes["blue"] >= target["blue"];
        }
    }
}
