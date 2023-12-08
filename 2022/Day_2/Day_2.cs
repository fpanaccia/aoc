using _2022;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2022.Day_2
{
    internal class Day_2 : IDay
    {
        private const string rockElf = "A";
        private const string paperElf = "B";
        private const string scissorsElf = "C";
        private const string rock = "X";
        private const string paper = "Y";
        private const string scissors = "Z";
        private const string needToLose = "X";
        private const string needToDraw = "Y";
        private const string needToWin = "Z";
        private const int rockScore = 1;
        private const int paperScore = 2;
        private const int scissorsScore = 3;
        private const int winScore = 6;
        private const int loseScore = 0;
        private const int drawScore = 3;

        public string Run()
        {
            return scorePartTwo(File.ReadAllLines("Day_2/Day2.txt")).ToString();
        }

        private static int scorePartOne(string[] fileLines)
        {
            int num = 0;
            foreach (string fileLine in fileLines)
            {
                string[] strArray = fileLine.Split(' ');
                string elfHand = strArray[0];
                string myHand = strArray[1];
                num +=battleScore(elfHand, myHand) +handScore(myHand);
            }
            return num;
        }

        private static int scorePartTwo(string[] fileLines)
        {
            int num = 0;
            foreach (string fileLine in fileLines)
            {
                string[] strArray = fileLine.Split(' ');
                string elfHand = strArray[0];
                string needToDo = strArray[1];
                string neededHand =getNeededHand(elfHand, needToDo);
                num +=battleScore(elfHand, neededHand) +handScore(neededHand);
            }
            return num;
        }

        private static string getNeededHand(string elfHand, string needToDo)
        {
            switch (elfHand)
            {
                case "A":
                    switch (needToDo)
                    {
                        case "X":
                            return "Z";
                        case "Y":
                            return "X";
                        case "Z":
                            return "Y";
                    }
                    break;
                case "B":
                    switch (needToDo)
                    {
                        case "X":
                            return "X";
                        case "Y":
                            return "Y";
                        case "Z":
                            return "Z";
                    }
                    break;
                case "C":
                    switch (needToDo)
                    {
                        case "X":
                            return "Y";
                        case "Y":
                            return "Z";
                        case "Z":
                            return "X";
                    }
                    break;
            }
            return "";
        }

        private static int handScore(string myHand)
        {
            switch (myHand)
            {
                case "X":
                    return 1;
                case "Y":
                    return 2;
                case "Z":
                    return 3;
                default:
                    return 0;
            }
        }

        private static int battleScore(string elfHand, string myHand)
        {
            switch (elfHand)
            {
                case "A":
                    switch (myHand)
                    {
                        case "X":
                            return 3;
                        case "Y":
                            return 6;
                        case "Z":
                            return 0;
                    }
                    break;
                case "B":
                    switch (myHand)
                    {
                        case "X":
                            return 0;
                        case "Y":
                            return 3;
                        case "Z":
                            return 6;
                    }
                    break;
                case "C":
                    switch (myHand)
                    {
                        case "X":
                            return 6;
                        case "Y":
                            return 0;
                        case "Z":
                            return 3;
                    }
                    break;
            }
            return 0;
        }
    }
}
