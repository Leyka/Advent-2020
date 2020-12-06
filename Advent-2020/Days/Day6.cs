using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Advent_2020.Days
{
    public class Day6 : Day
    {
        protected override void Run()
        {
            int sumPart1 = 0, sumPart2 = 0;

            string[] groups = RawInput.Split("\r\n\r\n");
            foreach (string group in groups)
            {
                string cleanedLetters = group.Replace("\r\n", "");

                // Part 1 
                sumPart1 += cleanedLetters.Distinct().Count();

                // Part 2 
                int declarationsCount = group.Split("\r\n").Length;
                sumPart2 += cleanedLetters.GroupBy(ch => ch).Sum(g => 
                {
                    return g.Count() == declarationsCount ? 1 : 0;
                });
            }

            AnswerPart1 = sumPart1.ToString();
            AnswerPart2 = sumPart2.ToString();
        }
    }
}
