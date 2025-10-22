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
            bool success = await _taskService.DeleteTaskAsync(id);
            
            if (success)
            {
                Console.WriteLine($"Задача с ID {id} успешно удалена.");
            }
            else
            {
                Console.WriteLine($"Ошибка: Задача с ID {id} не найдена.");
            }
        }
        else
        {
            Console.WriteLine("Неверный формат ID. Введите число.");
        }
    }
}