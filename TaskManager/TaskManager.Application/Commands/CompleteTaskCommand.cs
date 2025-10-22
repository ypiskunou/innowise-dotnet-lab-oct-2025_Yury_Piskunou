using ConsoleApp.Constants;
using TaskManager.Application.Services;

namespace TaskManager.Application.Commands;

public class CompleteTaskCommand : IAsyncCommand
{
    private readonly TaskApplicationService _taskService;
    
    public int MenuOption => MenuOptions.CompleteTask;
    public string Description => "Завершить задачу";

    public CompleteTaskCommand(TaskApplicationService taskService)
    {
        _taskService = taskService;
    }

    public async Task ExecuteAsync()
    {
        Console.Write("Введите ID задачи, которую хотите завершить: ");
        if (int.TryParse(Console.ReadLine()?.Trim(), out int id))
        {
            await _taskService.CompleteTaskAsync(id);
        }
        else
        {
            Console.WriteLine("Неверный формат ID. Введите число.");
        }
    }
}