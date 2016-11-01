using System;

namespace CommandPattern.Commands
{
    public class CalculatorCommand : Command
    {
        private char @operator;
        private int operand;
        private Calculator calculator;

        public CalculatorCommand(Calculator calculator, char @operator, int operand)
        {
            this.calculator = calculator;
            this.Operator = @operator;
            this.Operand = operand;
        }

        // Gets operator
        public char Operator
        {
            set { this.@operator = value; }
        }

        // Get operand
        public int Operand
        {
            set { this.operand = value; }
        }

        // Execute new command
        public override void Execute()
        {
            calculator.Operation(@operator, operand);
        }

        // Unexecute last command
        public override void UnExecute()
        {
            calculator.Operation(Undo(@operator), operand);
        }

        // Returns opposite operator for given operator
        private char Undo(char @operator)
        {
            switch (@operator)
            {
                case '+': return '-';
                case '-': return '+';
                case '*': return '/';
                case '/': return '*';
                default:
                    throw new ArgumentException("@operator");
            }
        }
    }
}
