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
            List<Instruction> instructions = InputLines.Select((inp) => new Instruction(inp)).ToList();

            // Part 1
            int accumPart1 = RunInstructions(instructions).Item2;
            AnswerPart1 = accumPart1.ToString();

            // Part 2
            List<Instruction> fixedInstructions = GetFixedInfiniteLoop(instructions);
            int accumPart2 = RunInstructions(fixedInstructions).Item2;
            AnswerPart2 = accumPart2.ToString();
        }

        
        private List<Instruction> GetFixedInfiniteLoop(List<Instruction> instructions)
        {
            var allNopJmpInstructions = instructions.Where(x => x.Action == "nop" || x.Action == "jmp").ToList();

            foreach (Instruction instruction in allNopJmpInstructions)
            {
                int instructionIndex = instructions.IndexOf(instruction);

                // Toggle action between nop -> jmp and jmp -> nop
                instruction.Action = instruction.Action == "nop" ? "jmp" : "nop";
                instructions[instructionIndex] = instruction;

                bool ranSuccesfully = RunInstructions(instructions).Item1;
                instructions.ForEach(x => x.IsExecuted = false);

                if (ranSuccesfully)
                {
                    return instructions;
                }
                else
                {
                    instruction.Action = instruction.Action == "nop" ? "jmp" : "nop";
                    instructions[instructionIndex] = instruction;
                }
            }

            return null;
        }

        private Tuple<bool, int> RunInstructions(List<Instruction> instructions)
        {
            int accum = 0;

            for (int i = 0; i < instructions.Count; i++)
            {
                Instruction instruction = instructions[i];

                if (instruction.IsExecuted)
                {
                    // Infinite loop
                    return Tuple.Create(false, accum);
                }

                if (instruction.Action == "acc") accum += instruction.Value;
                else if (instruction.Action == "jmp") i += instruction.Value - 1;

                instruction.IsExecuted = true;
            }

            // Has run successfully without infinite loop
            return Tuple.Create(true, accum);
        }

    }

    class Instruction
    {
        public string Action { get; set; }
        public int Value { get; set; }
        public bool IsExecuted { get; set; }
        public Instruction(string input)
        {
            var g = new Regex(@"([a-z]{3}) ([+-])(\d+)").Match(input).Groups;
            Action = g[1].Value;
            int sign = g[2].Value == "-" ? -1 : 1;
            Value = int.Parse(g[3].Value) * sign;
        }
    }
}
