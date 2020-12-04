using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Text.RegularExpressions;

namespace Advent_2020.Days
{
    public class Day4 : Day
    {
        protected override void Run()
        {
            string[] passports = RawInput.Split("\r\n\r\n");

            int countValidPassportsPart1 = 0, countValidPassportsPart2 = 0;
            foreach (string passport in passports)
            {
                string[] rawFields = passport.Replace("\r\n", " ").Split(" ");
                List<Tuple<string, string>> fields = rawFields
                    .Select(rawField =>
                    {
                        string[] keyValue = rawField.Split(':');
                        return Tuple.Create(keyValue[0], keyValue[1]);
                    }).ToList();

                
                if (fields.Count == 8 || (fields.Count == 7 && !fields.Select(f => f.Item1).Contains("cid")))
                {
                    // Part 1
                    countValidPassportsPart1++;

                    // Part2
                    if (IsValidPart2(fields))
                    {
                        countValidPassportsPart2++;
                    }
                }
            }

            AnswerPart1 = countValidPassportsPart1.ToString();
            AnswerPart2 = countValidPassportsPart2.ToString();
        }

        private bool IsValidPart2(List<Tuple<string, string>> fields)
        {
            bool isValid = true;

            foreach (var field in fields)
            {
                var (key, value) = field;
                switch (key)
                {
                    case "byr":
                        int birthday = int.Parse(value);
                        isValid &= birthday >= 1920 && birthday <= 2002;
                        break;
                    case "iyr":
                        int issueYear = int.Parse(value);
                        isValid &= issueYear >= 2010 && issueYear <= 2020;
                        break;
                    case "eyr":
                        int expYear = int.Parse(value);
                        isValid &= expYear >= 2020 && expYear <= 2030;
                        break;
                    case "hgt":
                        var groups = new Regex(@"(\d+)(\w+)").Match(value).Groups;
                        int height = int.Parse(groups[1].Value);
                        string unit = groups[2].Value;
                        isValid &= unit == "cm"
                            ? height >= 150 && height <= 193
                            : height >= 59 && height <= 76;
                        break;
                    case "hcl":
                        isValid &= new Regex(@"^#[a-f0-9]{6}$").IsMatch(value);
                        break;
                    case "ecl":
                        isValid &= new Regex(@"^(amb|blu|brn|gry|grn|hzl|oth)$").IsMatch(value);
                        break;
                    case "pid":
                        isValid &= new Regex(@"^\d{9}$").IsMatch(value);
                        break;
                    default:
                        break;
                }

                // No point to continue if it's not valid anymore
                if (!isValid)
                {
                    return false;
                }
            }

            return isValid;
        }
    }
}
