using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedProgramming
{
    // Define the delegates
    public delegate void OnResult(string result);
    public delegate string GetInput();
    public delegate void MyDelegate();

    // Original operation interface
    public interface IOperation
    {
        void Perform(OnResult onResult, GetInput getInput);
    }

    // Original operation implementations
    public class SumOperation : IOperation
    {
        public void Perform(OnResult onResult, GetInput getInput)
        {
            int firstTerm = int.Parse(getInput());
            int secondTerm = int.Parse(getInput());
            onResult((firstTerm + secondTerm).ToString());
        }
    }

    public class DifferenceOperation : IOperation
    {
        public void Perform(OnResult onResult, GetInput getInput)
        {
            int firstTerm = int.Parse(getInput());
            int secondTerm = int.Parse(getInput());
            onResult((firstTerm - secondTerm).ToString());
        }
    }

    public class UnknownOperation : IOperation
    {
        public void Perform(OnResult onResult, GetInput getInput)
        {
            onResult("Unknown command");
        }
    }

    public class ExitOperation : IOperation
    {
        private readonly Action _exitAction;

        public ExitOperation(Action exitAction)
        {
            _exitAction = exitAction;
        }

        public void Perform(OnResult onResult, GetInput getInput)
        {
            _exitAction();
            onResult("Program end");
        }
    }

    // New operation interface
    public interface Operation
    {
        string perform(int arg);
    }

    public class OperationA : Operation
    {
        public MyDelegate myDelegate;

        public OperationA(MyDelegate myDelegate)
        {
            this.myDelegate = myDelegate;
        }

        public string perform(int arg)
        {
            myDelegate?.Invoke();
            return "OperationA" + (arg + arg);
        }
    }

    public class OperationB : Operation
    {
        public string perform(int arg)
        {
            return "OperationB" + (arg + 2);
        }
    }

    // New operation for student lookup
    public class StudentLookupOperation : IOperation
    {
        private readonly Dictionary<string, int> _students;

        public StudentLookupOperation(Dictionary<string, int> students)
        {
            _students = students;
        }

        public void Perform(OnResult onResult, GetInput getInput)
        {
            string key = getInput();
            if (_students.ContainsKey(key))
            {
                onResult($"The student number for '{key}' is: {_students[key]}");
            }
            else
            {
                onResult("Key not found!");
            }
        }
    }
}
