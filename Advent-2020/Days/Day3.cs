using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Advent_2020.Days
{
    public class Day3 : Day
    {
        public int GridWidth { get; set; }
        public int GridHeight { get; set; }

        protected override void Run()
        {
            GridWidth = InputLines[0].Length;
            GridHeight = InputLines.Length;

            // Part 1
            long countTrees = TraverseAndCountTrees(3, 1);
            AnswerPart1 = countTrees.ToString();

            // Part2
            var slopes = new List<Tuple<int, int>>
            {
                Tuple.Create(1, 1),
                Tuple.Create(5, 1),
                Tuple.Create(7, 1),
                Tuple.Create(1, 2),
            };

            slopes.ForEach(slope =>
            {
                var (dx, dy) = slope;
                countTrees *= TraverseAndCountTrees(dx, dy);
            });

            AnswerPart2 = countTrees.ToString();
        }

        private int TraverseAndCountTrees(int dx, int dy)
        {
            int countTrees = 0;
            int x = 0, y = 0;

            while(true)
            {
                x = (x + dx) % GridWidth;
                y += dy;

                // Check if we reached bottom
                if (y >= GridHeight)
                {
                    return countTrees;
                }

                char element = InputLines[y][x]; // [Row, Column]
                if (element == '#') // Tree
                {
                    countTrees++;
                }
            }
        }
    }
}
