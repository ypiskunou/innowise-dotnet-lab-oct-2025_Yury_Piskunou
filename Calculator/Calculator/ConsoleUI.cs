using System.Globalization;

namespace Calculator;

public class ConsoleUI
{
    private readonly ICalculator  _calculator;
    private readonly string _validOperators;

    public ConsoleUI(ICalculator calculator, string validOperators)
    {
        _calculator = calculator;
        _validOperators = validOperators;
    }

    public void Start()
    {
        while (true)
        {
            try
            {
                double num1 = ReadDouble("Введите первое число: ");
                char op = ReadOperation("Введите операцию (+, -, *, /): ");
                double num2 = ReadDouble("Введите второе число: ");

                double result = _calculator.Calculate(num1, op, num2);
                Console.WriteLine($"Результат: {result}");
            }
            
            catch (Exception ex)
            {
                Console.WriteLine($"Произошла ошибка: {ex.Message}");
            }

            Console.Write("\nВыполнить еще одну операцию? (y/n): ");
            if (Console.ReadLine()?.ToLower() == "n")
            {
                break;
            }
            
            Console.WriteLine();
        }
    }
    
    private double ReadDouble(string prompt)
    {
        double number;
        Console.Write(prompt);
        string input = Console.ReadLine()??throw new InvalidOperationException("Поток ввода был закрыт.");
        
        while (!double.TryParse(input.Trim().Replace(',', '.'), 
                   NumberStyles.Any, CultureInfo.InvariantCulture, out number))
        {
            Console.WriteLine("Ошибка: введено некорректное число. Попробуйте снова.");
            Console.Write(prompt);
            input = Console.ReadLine()??throw new InvalidOperationException("Поток ввода был закрыт.");
        }
        
        return number;
    }
    
    private char ReadOperation(string prompt)
    {
        char operation;
        Console.Write(prompt);
        string input = Console.ReadLine()??throw new InvalidOperationException("Поток ввода был закрыт.");
        
        while (!TryGetOperation(input.Trim(), out operation))
        {
            Console.WriteLine("Ошибка: введена некорректная операция. Доступные: +, -, *, /");
            Console.Write(prompt);
            input = Console.ReadLine()??throw new InvalidOperationException("Поток ввода был закрыт.");
        }
        
        return operation;
    }
    
    private bool TryGetOperation(string? text, out char operation)
    {
        operation = '\0';

        if (!string.IsNullOrWhiteSpace(text) && text.Length == 1 && _validOperators.Contains(text[0]))
        {
            operation = text[0];
            return true;
        }

        return false;
    }

}