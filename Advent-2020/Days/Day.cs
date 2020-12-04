using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace Advent_2020.Days
{
    public abstract class Day
    {
        protected string RawInput { get; set; }
        protected string[] InputLines { get; set; }
        protected string AnswerPart1 { get; set; } = "Not implemented yet";
        protected string AnswerPart2 { get; set; } = "Not implemented yet";

        protected abstract void Run();

        private void ReadInputFromTxtFile()
        {
            // Get full path of "Inputs" folder using reflection
            string workingDirPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string inputPath = Path.Combine(workingDirPath, "Inputs"); 
            // Get text file name based on Class. Ex. Day1 => Day1.txt
            string concreteClassName =  GetType().Name;
            string fullInputPath = Path.Combine(inputPath, $"{concreteClassName}.txt");

            RawInput = File.ReadAllText(fullInputPath);
            InputLines = File.ReadAllLines(fullInputPath);
        }

        public void RunAndPrintAnswers()
        {
            ReadInputFromTxtFile();
            Run();

            Console.WriteLine($"Part 1: {AnswerPart1}");
            Console.WriteLine($"Part 2: {AnswerPart2}");
        }
    }
}
