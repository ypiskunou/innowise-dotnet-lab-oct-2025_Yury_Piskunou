using Microsoft.Extensions.Hosting;
using TaskManager.Application.Services;

namespace ConsoleApp;

public class ConsoleWorker : IHostedService
{
    private readonly TaskApplicationService _taskService;
    private readonly IHostApplicationLifetime _appLifetime;

    public ConsoleWorker(TaskApplicationService taskService, IHostApplicationLifetime appLifetime)
    {
        _taskService = taskService;
        _appLifetime = appLifetime;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        Console.WriteLine("Добро пожаловать в Менеджер Задач!");

        while (!cancellationToken.IsCancellationRequested)
        {
            PrintMenu();
            Console.Write("Выберите действие: ");
            var choice = Console.ReadLine();

            try
            {
                switch (choice)
                {
                    case "1":
                        await ShowAllTasks();
                        break;
                    case "2":
                        await AddTask();
                        break;
                    case "3":
                        await CompleteTask();
                        break;
                    case "4":
                        await DeleteTask();
                        break;
                    case "5":
                        // Завершаем приложение
                        _appLifetime.StopApplication();
                        return;
                    default:
                        Console.WriteLine("Неверный ввод. Пожалуйста, выберите от 1 до 5.");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Произошла непредвиденная ошибка: {ex.Message}");
            }
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        Console.WriteLine("Приложение завершает работу. До свидания!");
        return Task.CompletedTask;
    }

    private void PrintMenu()
    {
        Console.WriteLine("\n--- Меню ---");
        Console.WriteLine("1. Показать все задачи");
        Console.WriteLine("2. Добавить новую задачу");
        Console.WriteLine("3. Завершить задачу");
        Console.WriteLine("4. Удалить задачу");
        Console.WriteLine("5. Выйти");
        Console.WriteLine("------------");
    }
    
    private async Task ShowAllTasks()
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
                if (!string.IsNullOrEmpty(task.Description))
                {
                    Console.WriteLine($"   Описание: {task.Description}");
                }
            }
        }
        Console.WriteLine("--------------------");
    }

    private async Task AddTask()
    {
        Console.Write("Введите название задачи: ");
        var title = Console.ReadLine();

        Console.Write("Введите описание (или оставьте пустым): ");
        var description = Console.ReadLine();
        
        await _taskService.AddNewTaskAsync(title, description);
    }
    
    private async Task CompleteTask()
    {
        Console.Write("Введите ID задачи, которую хотите завершить: ");
        if (int.TryParse(Console.ReadLine(), out int id))
        {
            await _taskService.CompleteTaskAsync(id);
        }
        else
        {
            Console.WriteLine("Неверный формат ID. Введите число.");
        }
    }
    
    private async Task DeleteTask()
    {
        Console.Write("Введите ID задачи, которую хотите удалить: ");
        if (int.TryParse(Console.ReadLine(), out int id))
        {
            await _taskService.DeleteTaskAsync(id);
        }
        else
        {
            Console.WriteLine("Неверный формат ID. Введите число.");
        }
    }
}