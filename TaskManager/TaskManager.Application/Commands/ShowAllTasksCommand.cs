using ConsoleApp.Constants;
using TaskManager.Application.Services;

namespace TaskManager.Application.Commands;

public class ShowAllTasksCommand : IAsyncCommand
{
    private readonly TaskApplicationService _taskService;

    public int MenuOption => MenuOptions.ShowAllTasks;
    public string Description => "Показать все задачи";

    public ShowAllTasksCommand(TaskApplicationService taskService)
    {
        _taskService = taskService;
    }

    public async Task ExecuteAsync()
    {
        var tasks = await _taskService.GetAllTasksAsync();
        Console.WriteLine("\n--- Список Задач ---");
        if (!tasks.Any())
        {
            Console.WriteLine("Задач пока нет.");
        }
        else
        {
            foreach (var task in tasks)
            {
                var status = task.IsCompleted ? "[✓] Выполнено" : "[ ] В работе";
                Console.WriteLine($"{task.Id}. {task.Title} {status}");
            }
        }
        Console.WriteLine("--------------------");
    }
}