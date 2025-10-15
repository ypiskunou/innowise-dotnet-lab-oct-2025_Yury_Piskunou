using TaskManager.Domain.Contracts;
using TaskManager.Domain.Entities;

namespace TaskManager.Application.Services;

public class TaskApplicationService
{
    private readonly ITaskRepository _taskRepository;
    
    public TaskApplicationService(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    public async Task<IEnumerable<TaskItem>> GetAllTasksAsync()
    {
        return await _taskRepository.GetAllAsync();
    }

    public async Task AddNewTaskAsync(string title, string? description)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            throw new ArgumentException("Название задачи не может быть пустым.", nameof(title));
        }

        var newTask = new TaskItem(0, title, description, false, DateTime.MinValue);
        await _taskRepository.AddAsync(newTask);
    }

    public async Task<bool> DeleteTaskAsync(int id)
    {
        return await _taskRepository.DeleteAsync(id);
    }
    
    public async Task<bool> CompleteTaskAsync(int id)
    {
        return await _taskRepository.UpdateStatusAsync(id, true);
    }
}