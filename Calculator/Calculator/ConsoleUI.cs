using System.Globalization;

namespace Calculator;

public class ConsoleUI
{
    private readonly ICalculator _calculator;
    private readonly string _validOperators;
    private readonly IConsole _console;

    public ConsoleUI(ICalculator calculator, string validOperators, IConsole console)
    {
        _calculator = calculator;
        _validOperators = validOperators;
        _console = console;
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
                _console.WriteLine($"Результат: {result}");

                _console.Write("\nВыполнить еще одну операцию? (y/n): ");
                string input = _console.ReadLine()
                               ?? throw new OperationCanceledException(
                                   "Ввод отменен пользователем. Программа завершает работу."
                               );
                if (input.Trim().ToLower() == "n")
                {
                    break;
                }

                _console.WriteLine();
            }
            catch (InvalidOperationException ex)
            {
                _console.WriteLine($"\n{ex.Message} Программа завершает работу.");
            }
            catch (Exception ex)
            {
                _console.WriteLine($"Произошла ошибка: {ex.Message}");
            }
        }
    }

    internal double ReadDouble(string prompt)
    {
        double number;
        _console.Write(prompt);
        string input = _console.ReadLine() ?? throw new InvalidOperationException("Поток ввода был закрыт.");

        while (!double.TryParse(input.Trim().Replace(',', '.'),
                   NumberStyles.Any, CultureInfo.InvariantCulture, out number))
        {
            _console.WriteLine("Ошибка: введено некорректное число. Попробуйте снова.");
            _console.Write(prompt);
            input = _console.ReadLine() ?? throw new InvalidOperationException("Поток ввода был закрыт.");
        }

        return number;
    }

    private char ReadOperation(string prompt)
    {
        char operation;
        _console.Write(prompt);
        string input = _console.ReadLine() ?? throw new InvalidOperationException("Поток ввода был закрыт.");

        while (!TryGetOperation(input.Trim(), out operation))
        {
            _console.WriteLine("Ошибка: введена некорректная операция. Доступные: +, -, *, /");
            _console.Write(prompt);
            input = _console.ReadLine() ?? throw new InvalidOperationException("Поток ввода был закрыт.");
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