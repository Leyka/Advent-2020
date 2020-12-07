using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq;

namespace Advent_2020.Days
{
    public class Day7 : Day
    {
        const string SHINY_GOLD = "shiny gold";

        private Dictionary<string, List<Bag>> Bags { get; set; }

        protected override void Run()
        {
            Bags = PrepareBagsDictionary();

            // Part 1 
            int countPart1 = Bags.Keys.Sum(k => CarriesShinyGold(k) ? 1 : 0);
            AnswerPart1 = countPart1.ToString();

            // Part 2
            int countPart2 = SumBagQuantity(SHINY_GOLD);
            AnswerPart2 = countPart2.ToString();
        }


        private bool CarriesShinyGold(string currentColor)
        {
            if (Bags[currentColor] == null) return false;
            if (Bags[currentColor].Any(b => b.Color == SHINY_GOLD)) return true;

            return Bags[currentColor].Any(b => CarriesShinyGold(b.Color));
        }

        private int SumBagQuantity(string currentColor)
        {
            if (Bags[currentColor] == null) return 0;

            return Bags[currentColor].Sum(x => x.Quantity + x.Quantity * SumBagQuantity(x.Color));
        }

        private Dictionary<string, List<Bag>> PrepareBagsDictionary()
        {
            var bagsDictionary = new Dictionary<string, List<Bag>>();

            Regex r = new Regex(@"^([a-z\s]+) bags contain (.+)$");

            foreach (string input in InputLines)
            {
                GroupCollection g = r.Match(input).Groups;
                string color = g[1].Value;
                string content = g[2].Value;

                // No bags
                if (content.Contains("no other bags"))
                {
                    bagsDictionary.Add(color, null);
                    continue;
                }
                 
                // 1+ bags
                List<Bag> bags = content
                                    .Split(',')
                                    .Select(x => new Bag(x.Trim()))
                                    .ToList();
                bagsDictionary.Add(color, bags);
            }

            return bagsDictionary;
        }
    }

    class Bag
    {
        public string Color { get; set; }
        public int Quantity { get; set; }

        public Bag(string rawBagInput)
        {
            GroupCollection g = new Regex(@"(\d+) ([\sa-z]+) bags?").Match(rawBagInput).Groups;
            Quantity = int.Parse(g[1].Value);
            Color = g[2].Value;
        }
    }
}
