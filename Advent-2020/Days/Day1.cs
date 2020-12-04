using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Advent_2020.Days
{
    public class Day1 : Day
    {
        protected override void Run()
        {
            // Part 1
            for (int i = 0; i < InputLines.Length; i++)
            {
                int first = int.Parse(InputLines[i]);
                int desiredNumber = 2020 - first;

                bool found = InputLines.Any(x => x == desiredNumber.ToString());
                if (found)
                {
                    AnswerPart1 = (first * desiredNumber).ToString();
                    break;
                }
            }

            // Part 2
            for (int i = 0; i < InputLines.Length; i++)
            {
                int first = int.Parse(InputLines[i]);

                for (int j = i+1; j < InputLines.Length; j++)
                {
                    int second = int.Parse(InputLines[j]);
                    int desiredNumber = 2020 - first - second;

                    bool found = InputLines.Any(x => x == desiredNumber.ToString());
                    if (found)
                    {
                        AnswerPart2 = (first * second * desiredNumber).ToString();
                        return;
                    }
                }
            }
        }
    }
}
