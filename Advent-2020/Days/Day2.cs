using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq;

namespace Advent_2020.Days
{
    public class Day2 : Day
    {
        protected override void Run()
        {
            var passwordPolicies = InputLines.Select(inp => new PasswordPolicy(inp)).ToList();

            // Part 1
            int countValidPasswords = 0;
            foreach (var policy in passwordPolicies)
            {
                if (IsValidPasswordPart1(policy))
                {
                    countValidPasswords++;
                }
            }
            AnswerPart1 = countValidPasswords.ToString();

            // Part 2
            countValidPasswords = 0;
            foreach (var policy in passwordPolicies)
            {
                if (IsValidPasswordPart2(policy))
                {
                    countValidPasswords++;
                }
            }
            AnswerPart2 = countValidPasswords.ToString();
        }

        private bool IsValidPasswordPart1(PasswordPolicy policy)
        {
            var (minOccurence, maxOccurence) = policy.Numbers;

            int countLetter = policy.Password.Count(letter => letter == policy.Letter);
            return countLetter >= minOccurence && countLetter <= maxOccurence;
        }


        private bool IsValidPasswordPart2(PasswordPolicy policy)
        {
            var (firstPos, lastPos) = policy.Numbers;

            // XOR operation 
            return policy.Password[firstPos - 1] == policy.Letter ^ policy.Password[lastPos - 1] == policy.Letter;
        }

    }

    struct PasswordPolicy
    {
        public Tuple<int, int> Numbers { get; set; }
        public char Letter { get; set; }
        public string Password { get; set; }

        public PasswordPolicy(string rawInput)
        {
            Regex r = new Regex(@"(\d+)-(\d+)\s{1}(\w{1}):\s(\w+)");
            var groups = r.Match(rawInput).Groups;

            Numbers = Tuple.Create(int.Parse(groups[1].Value), int.Parse(groups[2].Value));
            Letter = groups[3].Value[0];
            Password = groups[4].Value;
        }
    }
}
