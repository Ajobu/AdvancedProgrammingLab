using System;
using System.Collections.Generic;

namespace AdvancedProgramming
{
    internal class Program
    {
        static bool runs = true;

        private static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            // Initialize student dictionary
            var students = new Dictionary<string, int>
            {
                { "Abunda", 3000 },
                { "Santos", 3001 },
                { "Lomeda", 3002 },
                { "Ortua", 3003 }
            };

            // Initialize all operations
            var commands = new Dictionary<string, IOperation>
            {
                { "sum", new SumOperation() },
                { "dif", new DifferenceOperation() },
                { "exit", new ExitOperation(() => runs = false) },
                { "student", new StudentLookupOperation(students) }
            };

            // Additional operations
            var operations = new Dictionary<string, Operation>
            {
                { "opA", new OperationA(() => Console.WriteLine("Delegate called in OperationA")) },
                { "opB", new OperationB() }
            };

            while (runs)
            {
                Console.WriteLine("\nAvailable commands: sum, dif, student, opA, opB, exit");
                Console.Write("Enter command: ");
                string? command = Console.ReadLine();

                // Check main commands first
                if (commands.TryGetValue(command, out IOperation consoleOperation))
                {
                    consoleOperation.Perform(Console.WriteLine, () =>
                    {
                        Console.Write("Enter required input: ");
                        return Console.ReadLine();
                    });
                }
                // Check additional operations
                else if (operations.TryGetValue(command, out Operation operation))
                {
                    Console.Write("Enter an integer argument: ");
                    if (int.TryParse(Console.ReadLine(), out int arg))
                    {
                        Console.WriteLine(operation.perform(arg));
                    }
                    else
                    {
                        Console.WriteLine("Invalid integer input");
                    }
                }
                else
                {
                    new UnknownOperation().Perform(Console.WriteLine, () => string.Empty);
                }
            }
        }
    }
}