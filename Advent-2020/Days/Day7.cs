using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq;

namespace Advent_2020.Days
{
    public class Day7 : Day
    {
        protected override void Run()
        {
            Dictionary<string, List<Bag>> bagsDict = PrepareBagsDictionary();

            
            int countValidBagsPart1 = 0;
            foreach (string color in bagsDict.Keys)
            {
                // Part 1 
                if (CarriesShinyGold(bagsDict, color))
                {
                    countValidBagsPart1++;
                    continue;
                }
            }
            AnswerPart1 = countValidBagsPart1.ToString();

            // Part 2

        }


        private bool CarriesShinyGold(Dictionary<string, List<Bag>> bagsDict, string currentColor)
        {
            if (bagsDict[currentColor] == null) return false;
            if (bagsDict[currentColor].Any(b => b.Color == "shiny gold")) return true;

            return bagsDict[currentColor].Select(b => b.Color).Any(color => CarriesShinyGold(bagsDict, color));
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
