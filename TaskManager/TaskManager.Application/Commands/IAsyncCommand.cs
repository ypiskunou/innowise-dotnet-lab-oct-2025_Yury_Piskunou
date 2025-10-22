namespace TaskManager.Application.Commands;

public interface IAsyncCommand
{
    int MenuOption { get; }
    
    string Description { get; }
    
    Task ExecuteAsync();
}