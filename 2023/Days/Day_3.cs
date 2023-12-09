using System.Collections;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Text.RegularExpressions;
using static System.Net.Mime.MediaTypeNames;

namespace _2023.Days
{
    internal class Day_3 : IDay
    {
        public List<string> Example = new List<string>()
        {
            "467..114..",
            "...*......",
            "..35..633.",
            "......#...",
            "617*......",
            ".....+.58.",
            "..592.....",
            "......755.",
            "...$.*....",
            ".664.598..",
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
            var lines = await File.ReadAllLinesAsync($"{currLocation}/Inputs/Day3.txt");
            return await CalculateFirst(lines.ToList());
        }

        public async Task<string> RunSecondPart()
        {
            var currLocation = AppDomain.CurrentDomain.BaseDirectory;
            var lines = await File.ReadAllLinesAsync($"{currLocation}/Inputs/Day3.txt");
            return await CalculateSecond(lines.ToList());
        }

        private async Task<string> CalculateFirst(List<string> engineSchematic)
        {
            var total = 0;
            var columnLength = engineSchematic.First().Length;
            var rowLength = engineSchematic.Count();
            var totalRowLength = rowLength + 2;
            var totalColumnLength = columnLength + 2;
            var matrix = BuildMatrix(engineSchematic, totalRowLength, totalColumnLength);
            var numbersByRow = new Dictionary<int, List<int>>();

            for (int i = 1; i < totalRowLength - 1; i++)
            {
                var numbers = new List<int>();
                for (int j = 1; j < totalColumnLength - 1; j++)
                {
                    var charInPosition = matrix[i, j];
                    if(char.IsDigit(charInPosition) && HasSymbol(matrix, i, j))
                    {
                        var numberAndPosition = GetNumber(matrix, i, j);
                        j = numberAndPosition.Item2;
                        numbers.Add(numberAndPosition.Item1);
                        total += numberAndPosition.Item1;
                    }
                }
                if(numbers.Any()) numbersByRow.Add(i, numbers);
            }

            return total.ToString();
        }

        private async Task<string> CalculateSecond(List<string> engineSchematic)
        {
            var total = 0;
            var columnLength = engineSchematic.First().Length;
            var rowLength = engineSchematic.Count();
            var totalRowLength = rowLength + 2;
            var totalColumnLength = columnLength + 2;
            var matrix = BuildMatrix(engineSchematic, totalRowLength, totalColumnLength);
            var numbersByRow = new Dictionary<int, List<int>>();

            for (int i = 1; i < totalRowLength - 1; i++)
            {
                for (int j = 1; j < totalColumnLength - 1; j++)
                {
                    var charInPosition = matrix[i, j];
                    if (matrix[i, j] == '*')
                    {
                        var ratio = GetRatio(matrix, i, j);
                        if (ratio > 0)
                        {
                            total += ratio;
                        }
                    }
                }
            }

            return total.ToString();
        }

        private char[,] BuildMatrix(List<string> engineSchematic, int totalRowLength, int totalColumnLength)
        {
            var matrix = new char[totalRowLength, totalColumnLength];
            AddDottedLine(matrix, 0, totalColumnLength);
            AddDottedLine(matrix, totalRowLength - 1, totalColumnLength);

            for (var i = 0; i < engineSchematic.Count; i++)
            {
                var row = engineSchematic[i];
                matrix[i + 1, 0] = '.';
                for (var j = 0; j < row.Length; j++)
                {
                    matrix[i + 1, j + 1] = row[j];
                }
                matrix[i + 1, totalColumnLength - 1] = '.';
            }

            return matrix;
        }

        private void AddDottedLine(char[,] matrix, int rowPosition, int length)
        {

            var line = new string('.', length);
            for (var i = 0; i < line.Length; i++)
            {
                try
                {
                    matrix[rowPosition, i] = line[i];
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        private bool HasSymbol(char[,] matrix, int rowPosition, int columnPosition)
        {
            var currRow = IsSymbol(matrix[rowPosition, columnPosition]) || IsSymbol(matrix[rowPosition, columnPosition - 1]) || IsSymbol(matrix[rowPosition, columnPosition + 1]);
            var prevRow = IsSymbol(matrix[rowPosition - 1, columnPosition]) || IsSymbol(matrix[rowPosition - 1, columnPosition - 1]) || IsSymbol(matrix[rowPosition - 1, columnPosition + 1]);
            var nextRow = IsSymbol(matrix[rowPosition + 1, columnPosition]) || IsSymbol(matrix[rowPosition + 1, columnPosition - 1]) || IsSymbol(matrix[rowPosition + 1, columnPosition + 1]);

            return currRow || prevRow || nextRow;
        }

        private bool IsSymbol(char character)
        {
            return !char.IsDigit(character) && character != '.';
        }

        private (int, int) GetNumber(char[,] matrix, int rowPosition, int columnPosition)
        {
            var initialDigit = matrix[rowPosition, columnPosition];
            var prevDigit = new List<char>();
            var nextDigit = new List<char>();

            var currChar = matrix[rowPosition, columnPosition];
            var currColPosition = columnPosition;

            do
            {
                currColPosition -= 1;
                currChar = matrix[rowPosition, currColPosition];
                if (char.IsDigit(currChar))
                {
                    prevDigit.Add(currChar);
                }
            } while (char.IsDigit(currChar));

            currChar = matrix[rowPosition, columnPosition];
            currColPosition = columnPosition;

            do
            {
                currColPosition += 1;
                currChar = matrix[rowPosition, currColPosition];
                if (char.IsDigit(currChar))
                {
                    nextDigit.Add(currChar);
                }
            } while (char.IsDigit(currChar));

            var numberAsString = new List<char>();
            var reversedPrevDigit = prevDigit.ToArray();
            Array.Reverse(reversedPrevDigit);
            numberAsString.AddRange(reversedPrevDigit);
            numberAsString.Add(initialDigit);
            numberAsString.AddRange(nextDigit);

            return (int.Parse(numberAsString.ToArray()), columnPosition + nextDigit.Count());
        }

        private int GetRatio(char[,] matrix, int rowPosition, int columnPosition)
        {
            var total = 0;
            var numbersCurrRow = new List<int>();
            var numbersPrevRow = new List<int>();
            var numbersNextRow = new List<int>();

            var coordinatesCurrRow = new List<(int, int)>()
            {
                (rowPosition, columnPosition - 1),
                (rowPosition, columnPosition + 1),
            };

            var coordinatesPrevRow = new List<(int, int)>()
            {
                (rowPosition - 1, columnPosition),
                (rowPosition - 1, columnPosition - 1),
                (rowPosition - 1, columnPosition + 1),
            };

            var coordinatesNextRow = new List<(int, int)>()
            {
                (rowPosition + 1, columnPosition),
                (rowPosition + 1, columnPosition - 1),
                (rowPosition + 1, columnPosition + 1),
            };

            foreach (var coordinate in coordinatesCurrRow)
            {
                if(char.IsDigit(matrix[coordinate.Item1, coordinate.Item2]))
                {
                    var number = GetNumber(matrix, coordinate.Item1, coordinate.Item2);
                    numbersCurrRow.Add(number.Item1);
                }
            }

            foreach (var coordinate in coordinatesPrevRow)
            {
                if (char.IsDigit(matrix[coordinate.Item1, coordinate.Item2]))
                {
                    var number = GetNumber(matrix, coordinate.Item1, coordinate.Item2);
                    numbersPrevRow.Add(number.Item1);
                }
            }

            foreach (var coordinate in coordinatesNextRow)
            {
                if (char.IsDigit(matrix[coordinate.Item1, coordinate.Item2]))
                {
                    var number = GetNumber(matrix, coordinate.Item1, coordinate.Item2);
                    numbersNextRow.Add(number.Item1);
                }
            }

            numbersCurrRow = numbersCurrRow.Distinct().ToList();
            numbersPrevRow = numbersPrevRow.Distinct().ToList();
            numbersNextRow = numbersNextRow.Distinct().ToList();

            var numbersTotal = numbersCurrRow.Count() + numbersPrevRow.Count() + numbersNextRow.Count();

            if(numbersTotal == 2)
            {
                total = 1;
                var allNumbers = new List<int>();
                allNumbers.AddRange(numbersPrevRow);
                allNumbers.AddRange(numbersCurrRow);
                allNumbers.AddRange(numbersNextRow);

                foreach (var number in allNumbers)
                {
                    total *= number;
                }
            }

            return total;
        }
    }
}
