namespace Calculator.ConsoleApp;

using Core;
using System;

public class ConsoleUI: IApplicationHost
{
    private readonly ICalculatorSession _session;

    public ConsoleUI(ICalculatorSession session)
    {
        _session = session;
    }

    public void Run()
    {
        Console.WriteLine("--- Консольный калькулятор ---");
        Console.WriteLine("Введите выражение для вычисления.");
        Console.WriteLine("Команды: 'undo' - отменить последнюю операцию, 'exit' - выход.");
        Console.WriteLine("---------------------------------");

        while (true)
        {
            Console.Write($" > Текущее значение: {_session.CurrentValue}\nВвод: ");
            string? input = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(input)) continue;

            try
            {
                switch (input.ToLower())
                {
                    case "exit":
                        Console.WriteLine("Завершение работы.");
                        return;

                    case "undo":
                        _session.UndoLast();
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("Последняя операция отменена.");
                        Console.ResetColor();
                        break;

                    default:
                        _session.ExecuteExpression(input);
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Выражение вычислено.");
                        Console.ResetColor();
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"ОШИБКА: {ex.Message}");
                Console.ResetColor();
            }
            Console.WriteLine();
        }
    }
}