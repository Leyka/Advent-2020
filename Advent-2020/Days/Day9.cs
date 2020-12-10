using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Advent_2020.Days
{
    public class Day9 : Day
    {
        private const int COUNT_PREAMBLE = 25;

        public List<double> AllInputs { get; set; }
        public List<double> Sums { get; set; }

        protected override void Run()
        {
            // Parse input string -> double
            AllInputs = InputLines.Select(x => double.Parse(x)).ToList();
            Sums = AllInputs.Skip(COUNT_PREAMBLE).ToList();

            // Part 1: Check the one that is not valid
            double susNumber = Sums.FirstOrDefault((sum) => !IsValid(sum));
            AnswerPart1 = susNumber.ToString();

            // Part 2: Calculate weakness of the not valid number
            AnswerPart2 = CalculateWeakness(susNumber).ToString();
        }

        private bool IsValid(double sum)
        {
            // Keep only the last {COUNT_PREAMBLE} numbers
            int sumIndex = Sums.IndexOf(sum);
            var preambules = AllInputs.Skip(sumIndex).Take(COUNT_PREAMBLE);
            // Cross join the preambules together then validate sum
            var crossPreambules = preambules.SelectMany(x => preambules.Where(y => y != x), (x, y) => new { x, y }).ToList();
            return crossPreambules.Any((combo) => combo.x + combo.y == sum);
        }

        private double CalculateWeakness(double invalidNumber)
        {
            double weakness = 0;

            foreach (double nb in AllInputs)
            {
                if (nb >= invalidNumber) continue;
                // TODO: continue part 2
            }

            return weakness;
        }
    }
}
