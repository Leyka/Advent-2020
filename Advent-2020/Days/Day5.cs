using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Advent_2020.Days
{
    public class Day5 : Day
    {
        protected override void Run()
        {
            // Part 1 
            List<int> seatIds = new List<int>();
            foreach (string seat in InputLines)
            {
                // Transform entry to base 2  
                int id = Convert.ToInt32(
                            seat.Replace('F', '0').Replace('B', '1').Replace('L', '0').Replace('R', '1'), 2);
                seatIds.Add(id);
            }

            int max = seatIds.Max();
            AnswerPart1 = max.ToString();

            // Part 2
            List<int> unknownSeatIds = Enumerable.Range(0, max).Where(x => !seatIds.Contains(x)).ToList();
            int mySeatId = unknownSeatIds
                            .FirstOrDefault(id => seatIds.Any(x => x == id - 1) && seatIds.Any(x => x == id + 1));
            AnswerPart2 = mySeatId.ToString();
        }
    }
}
