namespace TaskManager.Domain.Entities;

public record TaskItem(int Id, string Title, string? Description, bool IsCompleted, DateTime CreateAt);