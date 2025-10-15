using TaskManager.Domain.Entities;

namespace TaskManager.Domain.Contracts;

public interface ITaskRepository
{
    Task AddAsync(TaskItem task); 
    Task<IEnumerable<TaskItem>> GetAllAsync();
    Task<bool> UpdateStatusAsync(int id, bool isCompleted);
    Task<bool> DeleteAsync(int id);
    
}