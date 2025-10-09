using Calculator.Operations;

namespace Calculator;

public class Program
{
    public static void Main(string[] args)
    {
        var operations = new List<IOperation>
        {
            new Addition(),
            new Subtraction(),
            new Multiplication(),
            new Division()
        };

        string validOperators = string.Join("", operations.Select(o => o.Operator));

        var calculator = new Calculator(operations);
        
        var consoleUI = new ConsoleUI(calculator, validOperators);
        
        consoleUI.Start();

        Console.WriteLine("\nРабота калькулятора завершена.");
    }
}