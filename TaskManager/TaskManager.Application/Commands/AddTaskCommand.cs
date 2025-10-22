using ConsoleApp.Constants;
using TaskManager.Application.Services;

namespace TaskManager.Application.Commands;

public class AddTaskCommand : IAsyncCommand
{
    private readonly TaskApplicationService _taskService;

    public int MenuOption => MenuOptions.AddNewTask;
    public string Description => "Добавить новую задачу";

    public AddTaskCommand(TaskApplicationService taskService)
    {
        _taskService = taskService;
    }

    public async Task ExecuteAsync()
    {
        Console.Write("Введите название задачи: ");
        var title = Console.ReadLine()?.Trim();
        
        if (string.IsNullOrWhiteSpace(title))
        {
            Console.WriteLine("Ошибка: Название задачи не может быть пустым. Операция отменена.");
            return;
        }

        Console.Write("Введите описание (или оставьте пустым): ");
        var description = Console.ReadLine();
        
        try
        {
            await _taskService.AddNewTaskAsync(title, description);
            Console.WriteLine("Задача успешно добавлена!");
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
    }
}