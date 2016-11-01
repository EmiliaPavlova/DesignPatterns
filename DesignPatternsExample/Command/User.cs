using CommandPattern.Commands;
using System;
using System.Collections.Generic;

namespace CommandPattern
{
    /// <summary>
    /// The 'Invoker' class
    /// </summary>
    public class User
    {
        private Calculator calculator = new Calculator();
        private List<Command> commands = new List<Command>();
        private int currentCommand = 0;

        public void Redo(int levels)
        {
            Console.WriteLine("\n---- Redo {0} levels ", levels);

            // Perform redo operations
            for (int i = 0; i < levels; i++)
            {
                if (currentCommand < commands.Count - 1)
                {
                    Command command = commands[currentCommand++];
                    command.Execute();
                }
            }
        }

        public void Undo(int levels)
        {
            Console.WriteLine("\n---- Undo {0} levels ", levels);

            // Perform undo operations
            for (int i = 0; i < levels; i++)
            {
                if (currentCommand > 0)
                {
                    Command command = commands[--currentCommand] as Command;
                    command.UnExecute();
                }
            }
        }

        public void Compute(char @operator, int operand)
        {
            // Create command operation and execute it
            Command command = new CalculatorCommand(calculator, @operator, operand);
            command.Execute();

            // Add command to undo list
            commands.Add(command);
            currentCommand++;
        }
    }
}
