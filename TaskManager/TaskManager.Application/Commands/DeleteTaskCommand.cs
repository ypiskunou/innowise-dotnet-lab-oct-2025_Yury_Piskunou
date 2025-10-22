using ConsoleApp.Constants;
using TaskManager.Application.Services;

namespace TaskManager.Application.Commands;

public class DeleteTaskCommand : IAsyncCommand
{
    private readonly TaskApplicationService _taskService;
    
    public int MenuOption => MenuOptions.DeleteTask;
    public string Description => "Удалить задачу";

    public DeleteTaskCommand(TaskApplicationService taskService)
    {
        _taskService = taskService;
    }

    public async Task ExecuteAsync()
    {
        Console.Write("Введите ID задачи, которую хотите удалить: ");
        if (int.TryParse(Console.ReadLine()?.Trim(), out int id))
        {
            await _taskService.DeleteTaskAsync(id);
        }
        else
        {
            Console.WriteLine("Неверный формат ID. Введите число.");
        }
    }
}