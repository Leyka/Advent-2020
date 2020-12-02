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
            for (int i = 0; i < Inputs.Length; i++)
            {
                int first = int.Parse(Inputs[i]);
                int desiredNumber = 2020 - first;

                bool found = Inputs.Any(x => x == desiredNumber.ToString());
                if (found)
                {
                    AnswerPart1 = (first * desiredNumber).ToString();
                    break;
                }
            }

            // Part 2
            for (int i = 0; i < Inputs.Length; i++)
            {
                int first = int.Parse(Inputs[i]);

                for (int j = i+1; j < Inputs.Length; j++)
                {
                    int second = int.Parse(Inputs[j]);
                    int desiredNumber = 2020 - first - second;

                    bool found = Inputs.Any(x => x == desiredNumber.ToString());
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
