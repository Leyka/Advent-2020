using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;

namespace Advent_2020.Days
{
    public class Day8 : Day
    {
        protected override void Run()
        {
            int accumPart1 = 0, accumPart2 = 0;

            List<Instruction> instructions = InputLines.Select((inp) => new Instruction(inp)).ToList();

            // Part 1
            RunInstructions(instructions, ref accumPart1);
            AnswerPart1 = accumPart1.ToString();

            // Part 2
            List<Instruction> fixedInstructions = GetFixedInfiniteLoop(instructions);
            RunInstructions(fixedInstructions, ref accumPart2);
            AnswerPart2 = accumPart2.ToString();
        }

        
        private List<Instruction> GetFixedInfiniteLoop(List<Instruction> initialInstructions)
        {
            var allNopJmpInstructions = initialInstructions.Where(x => x.Action == "nop" || x.Action == "jmp").ToList();
            foreach (Instruction instruction in allNopJmpInstructions)
            {
                // Create a new copy
                var copyInstruction = (Instruction)instruction.Clone();
                var instructionsToTest = initialInstructions.ConvertAll(x => new Instruction(x.Action, x.Value));

                // Toggle action between nop -> jmp and jmp -> nop
                copyInstruction.Action = instruction.Action == "nop" ? "jmp" : "nop";
                int instructionIndex = initialInstructions.IndexOf(initialInstructions.FirstOrDefault(x => x.Id == instruction.Id));
                instructionsToTest[instructionIndex] = copyInstruction;

                int sum = 0;
                bool ranSuccesfully = RunInstructions(instructionsToTest, ref sum);
                if (ranSuccesfully)
                {
                    instructionsToTest.ForEach(x => x.IsExecuted = false);
                    return instructionsToTest;
                }
            }

            return null;
        }

        private bool RunInstructions(List<Instruction> instructions, ref int sum)
        {
            for (int i = 0; i < instructions.Count; i++)
            {
                Instruction instruction = instructions[i];

                if (instruction.IsExecuted)
                {
                    return false;
                }

                if (instruction.Action == "acc") sum += instruction.Value;
                else if (instruction.Action == "jmp") i += instruction.Value - 1;

                instruction.IsExecuted = true;
            }

            // Has run successfully without infinite loop
            return true;
        }

    }

    class Instruction : ICloneable
    {
        public Guid Id { get;  }
        public string Action { get; set; }
        public int Value { get; set; }
        public bool IsExecuted { get; set; }

        public Instruction(string input)
        {
            Id = Guid.NewGuid();

            var g = new Regex(@"([a-z]{3}) ([+-])(\d+)").Match(input).Groups;
            Action = g[1].Value;
            int sign = g[2].Value == "-" ? -1 : 1;
            Value = int.Parse(g[3].Value) * sign;
        }

        public Instruction(string action, int value)
        {
            Id = Guid.NewGuid();
            Action = action;
            Value = value;
        }

        public object Clone()
        {
            return new Instruction(Action, Value);
        }
    }
}
