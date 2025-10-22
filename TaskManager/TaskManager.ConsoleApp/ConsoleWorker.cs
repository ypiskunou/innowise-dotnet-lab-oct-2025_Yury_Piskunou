using ConsoleApp.Constants;
using Microsoft.Extensions.Hosting;
using TaskManager.Application.Commands;
using TaskManager.Application.Services;

namespace ConsoleApp;

public class ConsoleWorker : IHostedService
{
    private readonly IReadOnlyDictionary<int, IAsyncCommand> _commands;
    private readonly IHostApplicationLifetime _appLifetime;
    
    public ConsoleWorker(IEnumerable<IAsyncCommand> commands, IHostApplicationLifetime appLifetime)
    {
        _appLifetime = appLifetime;
        _commands = commands.ToDictionary(c => c.MenuOption, c => c);
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        Console.WriteLine("Добро пожаловать в Менеджер Задач!");

        while (!cancellationToken.IsCancellationRequested)
        {
            PrintMenu();
            Console.Write("Выберите действие: ");
            
            if (int.TryParse(Console.ReadLine()?.Trim(), out int choice))
            {
                if (choice == MenuOptions.Exit)
                {
                    _appLifetime.StopApplication();
                    return;
                }
                
                if (_commands.TryGetValue(choice, out var command))
                {
                    try
                    {
                        await command.ExecuteAsync();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Произошла непредвиденная ошибка: {ex.Message}");
                    }
                }
                else
                {
                    Console.WriteLine("Неверный ввод. Такой опции нет в меню.");
                }
            }
            else
            {
                Console.WriteLine("Неверный ввод. Пожалуйста, введите число.");
            }
        }
    }
    
    private void PrintMenu()
    {
        Console.WriteLine("\n--- Меню ---");
        // Строим меню динамически на основе найденных команд
        foreach (var command in _commands.Values.OrderBy(c => c.MenuOption))
        {
            Console.WriteLine($"{command.MenuOption}. {command.Description}");
        }
        Console.WriteLine($"{MenuOptions.Exit}. Выйти");
        Console.WriteLine("------------");
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        Console.WriteLine("Приложение завершает работу. До свидания!");
        return Task.CompletedTask;
    }
}